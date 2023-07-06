using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ThinhBlogAPI.Data;
using ThinhBlogAPI.Models;

namespace ThinhBlogAPI.Controllers
{
	[Route("[controller]")]
	[ApiController]
	public class AuthorController : ControllerBase
	{
		private readonly DataContext _db;
		public AuthorController(DataContext db)
		{
			_db = db;
		}

		[HttpGet(Name = "GetAllAuthors")]
		public IEnumerable<Author> Get()
		{
			return _db.Authors.ToList();
		}
		[HttpGet("{id:int}", Name = "GetAuthorByID")]
		public Author Get(int id)
		{
			return _db.Authors.Where(a => a.Id == id).FirstOrDefault();
		}

		[HttpPost]
		public void Post([FromBody] Author author)
		{
			Author a = new Author();
			a.Name = author.Name;
			_db.Authors.Add(a);
			_db.SaveChanges();
		}

		[HttpPut]
		public void Update([FromBody] Author author)
		{
			_db.Authors.Update(author);
			_db.SaveChanges();
		}

		[HttpDelete("{id:int}", Name = "DeleteAuthorByID")]
		public void Delete(int id)
		{
			var query = _db.Posts.Where(x => x.Author == id);
			foreach (var item in query)
			{
				item.Author = null;
			}
			Author a = _db.Authors.Where(b => b.Id == id).FirstOrDefault();
			if (a != null)
			{
				_db.Authors.Remove(a);
			}
			_db.SaveChanges();
		}
	}
}
