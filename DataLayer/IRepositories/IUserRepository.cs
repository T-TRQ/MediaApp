using Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer.IRepositories
{
	public interface IUserRepository : IRepository<User>
	{
		User LoginUser(string email, string password);
		User GetUserWithFriends(int id);
		User GetUserWithPosts(User user);
	}
}
