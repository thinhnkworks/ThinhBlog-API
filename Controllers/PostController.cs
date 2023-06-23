using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ThinhBlogAPI.Controllers
{
	[Route("[controller]")]
	[ApiController]
	public class PostController : ControllerBase
	{
		[HttpGet(Name = "GetPosts")]
		public string Get()
		{
			return "these are so many posts";
		}
	}
}
