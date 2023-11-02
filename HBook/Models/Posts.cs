using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using Entities;

namespace HBook.Models
{
	public class Posts
	{
		public int id { get; set; }
		public User User { get; set; }
		public Image PostImage { get; set; }
		public Post Post { get; set; } 

		public Posts(int id, User user, Post post)
		{
			User = user;
			this.id = id;
			Post = post;
		}
	}
}
