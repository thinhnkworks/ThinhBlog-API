using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ThinhBlogAPI.Data;
using ThinhBlogAPI.Models;

namespace ThinhBlogAPI.Controllers
{
	[Route("[controller]")]
	[ApiController]
	public class PostController : ControllerBase
	{
		private readonly DataContext _db;
		public PostController(DataContext db)
		{
			_db = db;
		}

		[HttpGet(Name = "GetPosts")]
		public IEnumerable<Post> Get()
		{
			return _db.Posts.ToList();
		}
		[HttpGet("{id:int}", Name = "GetPostByID")]
		public Post Get(int id)
		{
			return _db.Posts.Where(x => x.Id == id).FirstOrDefault();
		}

		[HttpPost]
		public void Post([FromBody] Post post)
		{
			Post p = new Post();
			p.Title = post.Title;
			p.Body = post.Body;
			p.Author = post.Author;
			_db.Posts.Add(p);
			_db.SaveChanges();
		}

		[HttpPut]
		public void Update([FromBody] Post post)
		{
			_db.Posts.Update(post);
			_db.SaveChanges();
		}

		[HttpDelete("{id:int}", Name = "DeletePostByID")]
		public void Delete([FromBody] int id)
		{
			Post p = _db.Posts.Where(x => x.Id == id).FirstOrDefault();
			if (p != null)
			{
				_db.Posts.Remove(p);
				_db.SaveChanges();
			}
		}
	}
}
