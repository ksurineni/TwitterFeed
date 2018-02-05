using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TwitterFeed.Repository.Entities
{
	public class Tweet
	{
		public string Id { get; set; }
		public string Full_Text { get; set; }
		public string Retweet_Count { get; set; }
		public string Created_At { get; set; }
		public TwitterUser User { get; set; }
		public ExtendedEntities Extended_Entities { get; set; }
	}
}
