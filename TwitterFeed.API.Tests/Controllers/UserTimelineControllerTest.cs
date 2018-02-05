using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Newtonsoft.Json;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web.Http.Results;
using TwitterFeed.API.Controllers;
using TwitterFeed.Repository;
using TwitterFeed.DTO;


namespace TwitterFeed.API.Tests.Controllers
{
	[TestClass]
	public class UserTimelineControllerTest
	{
		UserTimelineController _ctlr;

		/// <summary>
		/// Test UserTimeline Get API with twitter API
		/// </summary>
		/// <returns></returns>
		[TestMethod]
		public async Task GetUserTimelineWithService()
		{
			_ctlr = new UserTimelineController();
			var results = await _ctlr.Get("salesforce") as OkNegotiatedContentResult<IQueryable<Tweet>>;
			//check if API returned 200 response with some valid data
			Assert.IsNotNull(results);
			Assert.IsTrue(results.Content.Count()>0);			

		}


		/// <summary>
		/// Test UserTimeline Get API with mock data from json file.
		/// </summary>
		/// <returns></returns>
		[TestMethod]
		[DeploymentItem("UserTimeline.json")]
		public async Task GetUserTimelineWithMockData()
		{
			Mock<ITweetRepository> mockTweetRepository = new Mock<ITweetRepository>();

			//Load mock up data from json file
			string testData = System.IO.File.ReadAllText("UserTimeline.json");
			var tweets = JsonConvert.DeserializeObject<List<Repository.Entities.Tweet>>(testData);

			//Mock repository response
			//mockTweetRepository.Setup(m => m.GetUserTimeline(It.IsAny<string>(), It.IsAny<int>(), It.IsAny<string>())).ReturnsAsync(tweets.AsQueryable());
			mockTweetRepository.Setup(m => m.GetUserTimeline(It.IsAny<string>(), It.IsAny<int>(), It.IsAny<string>())).ReturnsAsync(new RepositoryResponse<IQueryable<Repository.Entities.Tweet>>(tweets.AsQueryable(),HttpStatusCode.OK));

			//Call API by injecting mock repository
			_ctlr = new UserTimelineController(mockTweetRepository.Object);
			var results = await _ctlr.Get("salesforce") as OkNegotiatedContentResult<IQueryable<Tweet>>;

			//check for 200 response and valid data
			Assert.IsNotNull(results);			
			var resulttweets = results.Content as IQueryable<Tweet>;
			Assert.AreEqual(tweets.Count, resulttweets.Count());
			Assert.AreEqual(tweets[0].Full_Text, resulttweets.First().TweetContent);
			
		}

		[TestMethod]
		public async Task InvalidUserTest()
		{
			_ctlr = new UserTimelineController();
			//check if API returns 404 not found if an invalid user timeline is requested
			var results = await _ctlr.Get("sdjhdsja382djjd") as NotFoundResult;			
			Assert.IsNotNull(results);			

		}
	}
}
