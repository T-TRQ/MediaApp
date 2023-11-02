using Entities;
using HBook.Commands;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessLayer;

namespace HBook.ViewModels
{
	class SearchUserViewModel : BaseViewModel
	{
		#region Prop
		private string _searchusertext="";
		public string SearchUserText
		{
			get { return _searchusertext; }
			set { _searchusertext = value; }
		}

		private ObservableCollection<User> _users;

		public ObservableCollection<User> Users
		{
			get { return _users; }
			set { _users = value; OnPropertyChanged(); }
		}

		private User _selecteduser;

		public User SelectedUser
		{
			get { return _selecteduser; }
			set { _selecteduser = value; }
		}
		#endregion Prop
		#region Commands
		public RelayCommand SendFriendRequestCommand { get; set; }
		public RelayCommand SearchUserCommand { get; set; }
		#endregion Commands

		public SearchUserViewModel()
		{
			SearchUserCommand = new RelayCommand(SearchUser, null);
			SendFriendRequestCommand = new RelayCommand(CreateFriendRequest, IsFriend);
		}

		private void SearchUser(object parameter)
		{
			UserController uc = new UserController();
			Users = uc.SearchUserByName(SearchUserText);
		}

		private void CreateFriendRequest(object paramater)
		{
			UserController uc = new UserController();
			if(SelectedUser != null)
			{
				FriendRequestController frc = new FriendRequestController();
				uc.CreateFriendRequest(uc.GetUser(Singleton.GetLoggedIn()._loggedInUser.UserId), uc.GetUser(SelectedUser.UserId));
				SelectedUser = null;
			}
			else
				Console.WriteLine("No user selected!");
		}

		private bool IsFriend(object parameter)
		{
			FriendController fc = new FriendController();
			if (SelectedUser != null)
			{
				if (fc.IsFriend(Singleton.GetLoggedIn()._loggedInUser).Any(x => x.UserId == SelectedUser.UserId)||(Singleton.GetLoggedIn()._loggedInUser.UserId==SelectedUser.UserId))
				{
					return false;
				}
				else
				{
					return true;
				}
			}
			else
			{
				return false;
			}
		}
	}
}
