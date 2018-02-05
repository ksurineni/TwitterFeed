using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TwitterFeed.Repository.Entities
{
	public class TweetMedia
	{
		public string Id { get; set; }
		public string URL { get; set; }
		public string Expanded_URL { get; set; }
		public string Media_URL { get; set; }
		public string Type { get; set; }
	}
}
