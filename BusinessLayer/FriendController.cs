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
	public class FriendController
	{
		private AppDbContext AppDbContext { get; set; }
		private UnitOfWork UnitOfWork { get; set; }

		public FriendController()
		{
			AppDbContext = new AppDbContext();
			UnitOfWork = new UnitOfWork(AppDbContext);
		}

		public ObservableCollection<Friend> GetFriendsByUser(User user)
		{
			return new ObservableCollection<Friend>(UnitOfWork.Friends.GetFriends(user)); 
		}

		public ObservableCollection<User> IsFriend(User userOne)
		{
			var temp = UnitOfWork.Friends.GetFriends(userOne);

			ObservableCollection<User> users = new ObservableCollection<User>();

			foreach(var item in temp)
			{
				if(item.From.UserId!=userOne.UserId)
				{
					users.Add(item.From);
				}
				else
				{
					users.Add(item.To);
				}
			}
			return users;
		}
	}
}
