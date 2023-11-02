using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities
{
	public class User
	{
		public int UserId { get; set; }
		public string Password { get; set; }
		public string Email { get; set; }
		public string Name { get; set; }
		public string Age { get; set; } = "No age added";
		public string Description { get; set; } = "No description added...";
		public ICollection<Post> Posts { get; set; } = new List<Post>();

		public User(string email, string password, string name)
		{
			this.Password = password;
			this.Email = email;
			this.Name = name;
		}
		public User()
		{

		}
	}
}
