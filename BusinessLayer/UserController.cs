using DataLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entities;
using System.Collections.ObjectModel;

namespace BusinessLayer
{
    public class UserController
    {
		private AppDbContext AppDbContext { get; set; }
		private UnitOfWork UnitOfWork { get; set; }

		public UserController()
		{
			AppDbContext = new AppDbContext();
			UnitOfWork = new UnitOfWork(AppDbContext);
		}

		public User GetUser(int id)
		{
			return UnitOfWork.Users.Get(id);
		}
		public bool CreateUser(string name, string email, string password)
		{
			if (UnitOfWork.Users.SingleOrDefault(s => s.Password == password || s.Email.ToLower() == email.ToLower()) != null)
				return false;
			else
			{
				User user = new User(email, password, name);
				UnitOfWork.Users.Add(user);
				UnitOfWork.complete();
				return true;
			}
			
		}
		public void CreateNewPost(string text, byte[] image, User user)
		{ 
			Post p = new Post(user, text, image);
			UnitOfWork.Posts.Add(p);  
			UnitOfWork.complete(); 
		}

		public ObservableCollection<User> SearchUserByName(string search)
		{
			return new ObservableCollection<User>(UnitOfWork.Users.Find(s => s.Name.Contains(search)));
		}

		public bool Login(string email, string password)
		{
			User u = UnitOfWork.Users.LoginUser(email, password);
			if (u == null)
				return false;
			else
			{
				Singleton.SetLoggedIn(u);
				return true;
			}
		}

		public User GetUserWithFriends(int id)
		{
			User u = UnitOfWork.Users.GetUserWithFriends(id);
			return u;
		}

		public void RemovePostByUser(Post p, User u)
		{
			Post post = UnitOfWork.Posts.Get(p.id);
			u.Posts.Remove(post);
			UnitOfWork.complete();
		}
		
		public void EditUserProfile(User u)
		{
			User user = UnitOfWork.Users.Get(u.UserId);
			user.Description = u.Description;
			UnitOfWork.complete();
		}


		public void AddFriendToUser(User friend, User user) 
		{
			Friend newFriend = new Friend(user, friend);
			UnitOfWork.Friends.Add(newFriend);
			UnitOfWork.complete();
		}

		public void CreateFriendRequest(User fromUser, User toUser)
		{
			UserController uc = new UserController();

			FriendRequest fr = new FriendRequest(fromUser, toUser);
			UnitOfWork.FriendRequests.Add(fr);
			UnitOfWork.complete();
		}

		public User GetUsersWithPosts(User user)
		{
			return UnitOfWork.Users.GetUserWithPosts(user);
		}
	}
}
