using HBook.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessLayer;
using System.Collections.ObjectModel;
using Entities;


namespace HBook.ViewModels
{
	class FriendRequestViewModel
	{
		private ObservableCollection<FriendRequest> _friendrequests = new ObservableCollection<FriendRequest>();
		public ObservableCollection<FriendRequest> FriendRequests
		{
			get { return _friendrequests; }
			set { _friendrequests = value; }
		}

		private FriendRequest _friendRequest;
		public FriendRequest SelectedFriendRequest
		{
			get { return _friendRequest; }
			set { _friendRequest = value; }
		}

		public RelayCommand AcceptFriendRequestCommand { get; set; }
		public RelayCommand LoadFriendRequestCommand { get; set; }
		public RelayCommand DeclineFriendRequestCommand { get; set; }


		public FriendRequestViewModel()
		{
			DeclineFriendRequestCommand = new RelayCommand(DenyFriendRequest, null);
			AcceptFriendRequestCommand = new RelayCommand(AcceptFriendRequest, null);
			LoadFriendRequests();
		}

		private void LoadFriendRequests()
		{

			FriendRequestController frc = new FriendRequestController();
			var temp = frc.GetFriendRequestByUser(Singleton.GetLoggedIn()._loggedInUser);

			foreach(var item in temp)
			{
				if (item.IsApproved != true)
				{
					FriendRequests.Add(item); 
				}
			}
		} 

		private void AcceptFriendRequest(object parameter)
		{
			if (SelectedFriendRequest != null)
			{
				FriendController fr = new FriendController();
				UserController uc = new UserController();
				FriendRequestController frc = new FriendRequestController();

				uc.AddFriendToUser(uc.GetUser(SelectedFriendRequest.FromUser.UserId), uc.GetUser(SelectedFriendRequest.ToUser.UserId));
				frc.SetFriendRequestIsApproved(SelectedFriendRequest);
				FriendRequests.Clear();
				LoadFriendRequests();
			}
		}

		private void DenyFriendRequest(object parameter)
		{
			FriendRequestController frc = new FriendRequestController();

			foreach (var friendRequest in FriendRequests)
				if (SelectedFriendRequest != null && friendRequest.id == SelectedFriendRequest.id)
				{
					frc.DeleteFriendRequest(friendRequest.id);
					LoadFriendRequests();
				}
		}
	}
}
