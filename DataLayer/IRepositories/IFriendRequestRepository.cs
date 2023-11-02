using Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;

namespace DataLayer.IRepositories
{
	public interface IFriendRequestRepository : IRepository<FriendRequest>
	{
		 ObservableCollection<FriendRequest> GetWholeFriendRequests(User user);
	}
}
