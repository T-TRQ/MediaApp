using Entities;
using HBook.Models;
using System.Collections.ObjectModel;
using BusinessLayer;
using System.IO;
using System.Windows.Media.Imaging;
using System.Windows.Controls;
using HBook.Commands;
using System;
using System.Linq;
using System.Windows;

namespace HBook.ViewModels
{
	public class ucHomeProfileViewModel : BaseViewModel
	{
		private bool _canEditDescriptionTextbox;
		public bool CanEditDescriptionTextbox
		{
			get { return _canEditDescriptionTextbox; }
			set { _canEditDescriptionTextbox = value; OnPropertyChanged(); }
		}

		private User _userShowing; 
		public User UserShowing
		{
			get { return _userShowing; }
			set { _userShowing = value; OnPropertyChanged(); }
		}

		private User _selectedFriend;
		public User SelectedFriend
		{
			get { return _selectedFriend; }
			set { _selectedFriend = value; }
		}


		private ObservableCollection<Posts> _posts;
		public ObservableCollection<Posts> Posts
		{
			get { return _posts; }
			set { _posts = value; OnPropertyChanged(); }

		}
		private ObservableCollection<User> friends = new ObservableCollection<User>();
		public ObservableCollection<User> Friends
		{
			get { return friends; }
			set { friends = value; }
		}

		private string _searchposttext = "";
		public string SearchPostText
		{
			get { return _searchposttext; }
			set { _searchposttext = value; }
		}

		private Posts _selectedPost;
		public Posts SelectedPost
		{
			get { return _selectedPost; }
			set { _selectedPost = value; OnPropertyChanged(); }
		}

		private Visibility removePostVisibility = Visibility.Visible;

		public Visibility RemovePostVisibility
		{
			get { return removePostVisibility; }
			set { removePostVisibility = value; OnPropertyChanged(); }
		}

		private Visibility editVisibility = Visibility.Visible;

		public Visibility EditVisibility
		{
			get { return editVisibility; }
			set { editVisibility = value; OnPropertyChanged(); }
		}
		private Visibility homeVisibility = Visibility.Collapsed;

		public Visibility HomeVisibility
		{
			get { return homeVisibility; }
			set { homeVisibility = value; OnPropertyChanged(); }
		}


		public RelayCommand SearchPostCommand { get; set; }
		public RelayCommand LoadPostsCommand { get; set; }
		public RelayCommand RemovePostCommand { get; set; }
		public RelayCommand LoadFriendsCommand { get; set; }
		public RelayCommand EditProfileCommand { get; set; }
		public RelayCommand OpenProfileCommand { get; set; }
		public RelayCommand UserProfileCommand { get; set; }
		public RelayCommand RefreshFriendsCommand { get; set; }

		public ucHomeProfileViewModel()
		{
			UserShowing = Singleton.GetLoggedIn()._loggedInUser;   
			RemovePostCommand = new RelayCommand(RemovePost, null);
			LoadPosts();
			LoadFriends();
			SearchPostCommand = new RelayCommand(SearchPost, null);
			OpenProfileCommand = new RelayCommand(OpenProfile, CanOpenProfile);
			UserProfileCommand = new RelayCommand(OpenUserProfile, null);
			EditProfileCommand = new RelayCommand(EditProfile, CanEditDescription);
			RefreshFriendsCommand = new RelayCommand(RefreshFriends, null);
		}
		private bool CanOpenProfile(object parameter)
		{
			if(SelectedFriend!=null)
			{
				return true;
			}
			else
			{
				return false;
			}
		}

		private void OpenProfile(object parameter)
		{
			UserShowing = SelectedFriend;
			Friends.Clear();
			LoadPosts();
			LoadFriends();
			RemovePostVisibility = Visibility.Hidden;
			EditVisibility = Visibility.Collapsed;
			HomeVisibility = Visibility.Visible;
		}

		private void OpenUserProfile(object parameter)
		{
			UserShowing = Singleton.GetLoggedIn()._loggedInUser;
			Friends.Clear();
			LoadPosts();
			LoadFriends();
			RemovePostVisibility = Visibility.Visible;
			EditVisibility = Visibility.Visible;
			HomeVisibility = Visibility.Collapsed;
		}
		private void LoadPosts()
		{
			PostController pc = new PostController();
			Posts = new ObservableCollection<Posts>();
			foreach (var post in pc.GetWholePost(UserShowing.UserId))
			{
				Posts newPost = new Posts(post.id, post.User, post);

				if (post.Image != null)
				{
					using (var ms = new MemoryStream(post.Image))
					{
						BitmapImage bmi = new BitmapImage();
						bmi.BeginInit();
						bmi.CacheOption = BitmapCacheOption.OnLoad;
						bmi.StreamSource = ms;
						bmi.EndInit();
						newPost.PostImage = new Image();
						newPost.PostImage.Source = bmi;
					}
				}
				Posts.Add(newPost);
			}
		}

		private void LoadFriends()
		{
			UserController uc = new UserController();
			FriendController fc = new FriendController();
			ObservableCollection<Friend> tempFriend = fc.GetFriendsByUser(UserShowing);
			User currentUser = UserShowing;
			if (tempFriend != null)
			{
				foreach (var item in tempFriend)
					if (item.To.UserId != currentUser.UserId)
						Friends.Add(item.To);
					else
						Friends.Add(item.From);
			}
		}

		private void SearchPost(object parameter)
		{
			PostController pc = new PostController();
			Posts = new ObservableCollection<Posts>();
			foreach (var post in pc.GetWholePostsBySearchTerm(UserShowing.UserId, SearchPostText))
			{
				Posts newPost = new Posts(post.id, post.User, post);
				if (post.Image != null)
				{
					using (var ms = new MemoryStream(post.Image))
					{
						BitmapImage bmi = new BitmapImage();
						bmi.BeginInit();
						bmi.CacheOption = BitmapCacheOption.OnLoad;
						bmi.StreamSource = ms;
						bmi.EndInit();
						newPost.PostImage = new Image();
						newPost.PostImage.Source = bmi;
					}
				}
				Posts.Add(newPost);
			}
		}

		private void RemovePost(object parameter)
		{
			UserController uc = new UserController();
				if (SelectedPost != null)
				{
					PostController pc = new PostController();
					pc.RemovePost(SelectedPost.id);
					UserShowing = uc.GetUsersWithPosts(UserShowing);
					LoadPosts();
				}
		}

		private void EditProfile(object parameter) /*Skickar med den inloggade användaren*/
		{
			UserController uc = new UserController();
			uc.EditUserProfile(UserShowing);
		}

		public bool CanEditDescription(object parameter) /*Villkor för om man får skriva i textbox eller klicka på knappen*/
		{
			if (UserShowing.UserId == Singleton.GetLoggedIn()._loggedInUser.UserId) 
			{
				CanEditDescriptionTextbox = true;
				return true;
			}
			else
			{
				CanEditDescriptionTextbox = false;
				return false;
			}
		}

		private void RefreshFriends(object parameter)
		{
			Friends.Clear();
			LoadFriends();
		}
	}
}
