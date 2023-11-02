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
	public class FriendRequestController
	{
		private AppDbContext AppDbContext { get; set; }
		private UnitOfWork UnitOfWork { get; set; }

		public FriendRequestController()
		{
			AppDbContext = new AppDbContext();
			UnitOfWork = new UnitOfWork(AppDbContext);
		}

		public ObservableCollection<FriendRequest> GetFriendRequestByUser(User user)
		{
			return new ObservableCollection<FriendRequest>(UnitOfWork.FriendRequests.GetWholeFriendRequests(user));
		}


		public FriendRequest GetFriendRequestByUsers(FriendRequest friendRequest)
		{
			return UnitOfWork.FriendRequests.SingleOrDefault(s => s.id == friendRequest.id);
		}

		public void SetFriendRequestIsApproved(FriendRequest friendRequest)
		{
			FriendRequest fr = GetFriendRequestByUsers(friendRequest);
			fr.IsApproved = true; 
			UnitOfWork.complete();
		}

		public void DeleteFriendRequest(int id)
		{
			FriendRequest denyfriendrequest = UnitOfWork.FriendRequests.Get(id);
			UnitOfWork.FriendRequests.Remove(denyfriendrequest);
			UnitOfWork.complete();
		}
	}
}
