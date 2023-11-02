using DataLayer.IRepositories;
using Entities;
using System.Linq;
using System.Data.Entity;

namespace DataLayer.Repositories
{
	public class UserRepository : Repository<User>, IUserRepository
	{
		public UserRepository(AppDbContext context) : base(context)
		{

		}
		public AppDbContext AppDbContext
		{
			get { return Context as AppDbContext; }
		}

		public User GetUserWithFriends(int id)
		{
			return AppDbContext.User.Include("FriendList").FirstOrDefault(x => x.UserId == id);
		}

		public User GetUserWithPosts(User user)
		{
			return AppDbContext.User.Include("Posts").SingleOrDefault(x => x.UserId==user.UserId);
		}

		public User LoginUser(string email, string password)
		{
			return AppDbContext.User.Include(x => x.Posts).FirstOrDefault(x => x.Email.ToLower() == email.ToLower() && x.Password == password);
		}
	}
}
