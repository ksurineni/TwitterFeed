using System;
using System.Collections.Generic;
using System.Linq;
using System.Configuration;
using System.Text;
using System.Net.Http;
using System.Threading.Tasks;
using TwitterFeed.Repository.Entities;
using Newtonsoft.Json;

namespace TwitterFeed.Repository
{
	public class TweetRepository : ITweetRepository
	{
		private static string _internalAccessToken;

		/// <summary>
		/// Gets User timeline using Twitter API
		/// </summary>
		/// <param name="userName"></param>
		/// <param name="count"></param>
		/// <param name="accessToken"></param>
		/// <returns></returns>
		public async Task<RepositoryResponse<IQueryable<Tweet>>> GetUserTimeline(string userName, int count, string accessToken = null)
		{
			if (accessToken == null)
			{
				accessToken = await GetAccessToken();
			}

			var httpClient = new HttpClient();
			httpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", accessToken);
			var response = await httpClient.GetAsync(string.Format("{0}?count={1}&screen_name={2}&tweet_mode=extended", ConfigurationManager.AppSettings["TwitterUserTimelineAPI"], count, userName));

			string jsonResponse = await response.Content.ReadAsStringAsync();
			if (response.IsSuccessStatusCode)
			{
				// For success, deserialize response to tweet objects
				var tweets = JsonConvert.DeserializeObject<List<Tweet>>(jsonResponse);
				return new RepositoryResponse<IQueryable<Tweet>>(tweets.AsQueryable(),response.StatusCode);
			}
			else
			{
				// for unsuccessful call, capture status code and json response for error logging
				return new RepositoryResponse<IQueryable<Tweet>>(null, response.StatusCode, jsonResponse);
			}

		}

		/// <summary>
		/// Get application API access token using application keys.
		/// </summary>
		/// <returns></returns>
		private async Task<string> GetAccessToken()
		{
			//Since this is static token(and does not require refresh token), it can be stored in static variable and reused for subsequent request. 
			//Another approach is to expose this as a separate API and have client sent the token with every request header.
			if (string.IsNullOrEmpty(_internalAccessToken))
			{
				var httpClient = new HttpClient();
				var request = new HttpRequestMessage(HttpMethod.Post, ConfigurationManager.AppSettings["TwitterOAuthAPI"]);
				var customerInfo = Convert.ToBase64String(new UTF8Encoding().GetBytes(ConfigurationManager.AppSettings["OAuthConsumerKey"] + ":" + ConfigurationManager.AppSettings["OAuthConsumerSecret"]));
				request.Headers.Add("Authorization", "Basic " + customerInfo);
				request.Content = new StringContent("grant_type=client_credentials", Encoding.UTF8, "application/x-www-form-urlencoded");

				HttpResponseMessage response = await httpClient.SendAsync(request);

				dynamic item = JsonConvert.DeserializeObject<object>(await response.Content.ReadAsStringAsync());

				_internalAccessToken = item["access_token"];
			
			}
			return _internalAccessToken;
		}
	}
}
