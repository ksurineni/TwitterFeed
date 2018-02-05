using System;
using System.Linq;
using System.Net;
using System.Web.Http;
using System.Threading.Tasks;
using TwitterFeed.Repository;
using TwitterFeed.Repository.Factories;

namespace TwitterFeed.API.Controllers
{
	public class UserTimelineController : ApiController
	{
		ITweetRepository _repository;
		TweetFactory _tweetFactory = new TweetFactory();
		private static readonly log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

		const int DefaultCount = 10;		

		public UserTimelineController(ITweetRepository repository)
		{
			_repository = repository;
		}

		public UserTimelineController() : this(new TweetRepository()) { }
		



		// GET api/<controller>/5
		public async Task<IHttpActionResult> Get(string user, int count = DefaultCount)
		{
			try
			{		
				
				var response = await _repository.GetUserTimeline(user, count);
				if (response.StatusCode.Equals(HttpStatusCode.OK))
				{
					//if successful call, return OK(200) result with tweets data
					var tweets = response.Entity.Select(i => _tweetFactory.CreateTweetDTO(i));
					return Ok(tweets);
				}
				else if (response.StatusCode.Equals(HttpStatusCode.NotFound))
				{
					//if the user does not send a valid account name, send 404 not found
					log.Info(response.ErrorResponse);
					return NotFound();
					// log Twitter API response using response.ErrorResponse
				}
				else
				{
					//Everything else can be treated as internal error in this scenario as the client is only required to send valid account
					log.Error(response.ErrorResponse);
					return InternalServerError();
				}

			}
			catch(Exception ex)
			{
				log.Error(ex.Message);
				return InternalServerError();
			}
		}

		

		
	}
}