using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TwitterFeed.DTO
{
    public class Tweet
    {
		public string UserName { get; set; }
		public string ScreenName { get; set; }
		public string ProfileImage { get; set; }
		public string TweetContent { get; set; }
		public string RetweetCount { get; set; }

		public string TweetDate { get; set; }

		public List<TweetMedia> MediaList { get; set; }
	}
}
