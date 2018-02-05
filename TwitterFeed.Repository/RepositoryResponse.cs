using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;

namespace TwitterFeed.Repository
{
	public class RepositoryResponse<T> where T: class
	{
		public T Entity { get; set; }
		public HttpStatusCode StatusCode { get; set; }

		public string ErrorResponse { get; set; }


		public RepositoryResponse(T entity, HttpStatusCode statusCode)
		{
			Entity = entity;
			StatusCode = statusCode;
		}

		public RepositoryResponse(T entity, HttpStatusCode statusCode, string error) : this(entity, statusCode)
        {
			ErrorResponse = error;
		}
	}
}
