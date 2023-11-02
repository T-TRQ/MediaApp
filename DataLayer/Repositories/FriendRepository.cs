using DataLayer.IRepositories;
using Entities;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer.Repositories
{
	public class FriendRepository : Repository<Friend>, IFriendRepository
	{
		public FriendRepository(AppDbContext context) : base(context)
		{

		}
		public AppDbContext AppDbContext
		{
			get { return Context as AppDbContext; }
		}

		public ObservableCollection<Friend> GetFriends(User user)
		{
			return new ObservableCollection<Friend>(AppDbContext.Friend.Include("From").Include("To").Where(x => x.From.UserId == user.UserId || x.To.UserId == user.UserId));
		}

	}
}
