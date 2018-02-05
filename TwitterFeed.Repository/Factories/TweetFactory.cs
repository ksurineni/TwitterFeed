using System.Collections.Generic;
using System.Linq;
using TwitterFeed.DTO;

namespace TwitterFeed.Repository.Factories
{
	public class TweetFactory
	{
		/// <summary>
		/// Creates Tweet DTO/ ViewModel object from Twitter Json object model.
		/// </summary>
		/// <param name="tweet"></param>
		/// <returns></returns>
		public Tweet CreateTweetDTO(TwitterFeed.Repository.Entities.Tweet tweet)
		{
			// This can be extended further as required for data shaping or filtering need at application level.
			return new Tweet()
			{
				UserName = tweet.User.Name,
				ScreenName = tweet.User.Screen_Name,
				ProfileImage = tweet.User.Profile_Image_Url,
				TweetContent = tweet.Full_Text,
				RetweetCount = tweet.Retweet_Count,
				TweetDate = tweet.Created_At,
				MediaList = tweet.Extended_Entities == null ? new List<TweetMedia>() : tweet.Extended_Entities.Media.Select(i => new TweetMedia() { URL = i.URL, Expanded_URL = i.Expanded_URL, Media_URL = i.Media_URL, Type = i.Type }).ToList()
			};
		}

	
	}
}
