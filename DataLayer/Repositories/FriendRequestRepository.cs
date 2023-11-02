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
	public class FriendRequestRepository : Repository<FriendRequest>, IFriendRequestRepository
	{
		public FriendRequestRepository(AppDbContext context) : base(context)
		{

		}
		public AppDbContext AppDbContext
		{
			get { return Context as AppDbContext; }
		}

		public ObservableCollection<FriendRequest> GetWholeFriendRequests(User user)
		{
			return new ObservableCollection<FriendRequest>(AppDbContext.FriendRequest.Include("FromUser").Include("ToUser").Where(x => x.ToUser.UserId == user.UserId));
		}
	}
}
