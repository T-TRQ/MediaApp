using BusinessLayer;
using HBook.Commands;
using HBook.Views;
using System.Windows;

namespace HBook.ViewModels
{
	class MainWindowViewModel : BaseViewModel
	{
		public RelayCommand CreatePostCommand { get; set; }
		public RelayCommand FriendRequestsCommand { get; set; }
		public RelayCommand SearchUserCommand { get; set; }
		public RelayCommand FeedCommand { get; set; }
		public RelayCommand CloseProgramCommand { get; set; }
		

		public MainWindowViewModel()
		{
			FeedCommand = new RelayCommand(OpenFeedWindow, null);
			CreatePostCommand = new RelayCommand(OpenCreatePostWindow, null);
			FriendRequestsCommand = new RelayCommand(OpenFriendRequestsWindow, null);
			SearchUserCommand = new RelayCommand(OpenSearchUserWindow, null);
			CloseProgramCommand = new RelayCommand(CloseProgram, null);
		}

		private void OpenFeedWindow(object parameter)
		{
			Feed window = new Feed();
			window.ShowDialog();
		}

		private void OpenCreatePostWindow(object parameter)
		{
			CreatePost window = new CreatePost();
			window.ShowDialog();
		}

		private void OpenFriendRequestsWindow(object parameter)
		{
			FriendRequests window = new FriendRequests();
			window.ShowDialog();
		}

		private void OpenSearchUserWindow(object parameter)
		{
			SearchUser window = new SearchUser();
			window.ShowDialog();
		}

		public void CloseProgram(object parameter)
		{
			Application.Current.Shutdown();
		}

	}
}
