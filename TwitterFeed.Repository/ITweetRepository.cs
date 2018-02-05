using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TwitterFeed.Repository
{
	public interface ITweetRepository
	{
		Task<RepositoryResponse<IQueryable<Entities.Tweet>>> GetUserTimeline(string userName, int count, string accessToken = null);
	}
}
