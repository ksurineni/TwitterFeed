using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TwitterFeed.Repository.Entities
{
	public class TwitterUser
	{
		public string Id { get; set; }
		public string Name { get; set; }
		public string Screen_Name { get; set; }
		public string Profile_Image_Url { get; set; }
	}
}
