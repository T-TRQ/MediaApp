using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities
{
	public class Post
	{ 
		public int id { get; set; }
		public User User { get; set; }
		public string Text { get; set; }
		public byte[] Image { get; set; }
		public DateTime PublishDate { get; set; }
		public int ReactionCount { get; set; }
		public List<Comment> Comments { get; set; } = new List<Comment>(); 

		public Post(User user, string text, byte[] image = null, int reCount = 0)
		{
			User = user;
			Text = text;
			PublishDate = DateTime.Now;
			Image = image;
			ReactionCount = reCount;
		}

		public Post()
		{

		}
	}
}
