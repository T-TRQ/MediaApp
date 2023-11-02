using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Imaging;
using BusinessLayer;
using Entities;
using HBook.Commands;
using HBook.Models;

namespace HBook.ViewModels
{
	public class FeedViewModel : BaseViewModel
	{
		private bool orderByAscending = true;

		private Posts _selectedPost;
		public Posts SelectedPost
		{
			get { return _selectedPost; }
			set { _selectedPost = value; OnPropertyChanged(); LoadComments(); UpdateSelectedComment(); }
		}
		private string _text;
		public string Text
		{
			get { return _text; }
			set { _text = value; OnPropertyChanged(); }
		}
		private ObservableCollection<User> friends = new ObservableCollection<User>();
		public ObservableCollection<User> Friends
		{
			get { return friends; }
			set { friends = value; }
		}

		private ObservableCollection<Posts> posts = new ObservableCollection<Posts>();
		public ObservableCollection<Posts> Posts
		{
			get { return posts; }
			set { posts = value; OnPropertyChanged(); }
		}
		private ObservableCollection<Comment> comments = new ObservableCollection<Comment>();
		public ObservableCollection<Comment> Comments
		{
			get { return comments; }
			set { comments = value; OnPropertyChanged(); }
		}


		private Comment selectedComment;
		public Comment SelectedComment
		{
			get { return selectedComment; }
			set { selectedComment = value; OnPropertyChanged(); UpdateSelectedComment(); }
		}
		private Visibility removeCommentVisibility;

		public Visibility RemoveCommentVisibility
		{
			get { return removeCommentVisibility; }
			set { removeCommentVisibility = value; OnPropertyChanged(); }
		}

		public RelayCommand LoadFriendPostsCommand { get; set; }
		public RelayCommand UpVoteCommand { get; set; }
		public RelayCommand DownVoteCommand { get; set; }
		public RelayCommand CommentCommand { get; set; }
		public RelayCommand DeleteCommentCommand { get; set; }
		public RelayCommand SortRecentCommand { get; set; }
		public RelayCommand SortOldestCommand { get; set; }

		public FeedViewModel()
		{
			LoadFriendPostsCommand = new RelayCommand(LoadFriends, null);
			UpVoteCommand = new RelayCommand(Upvote, null);
			DownVoteCommand = new RelayCommand(DownVote, null);
			CommentCommand = new RelayCommand(CreateNewComment, CanCreateNewComment);
			DeleteCommentCommand = new RelayCommand(DeleteComment, null);
			SortRecentCommand = new RelayCommand(SortRecent, null);
			SortOldestCommand = new RelayCommand(SortOldest, null);
		}
		public bool CanCreateNewComment(object parameter)
		{
			if (Text != null && SelectedPost != null)
			{
				return true;
			}
			else
			{
				return false;
			}
		}

		private void UpdateSelectedComment()
		{
			if (SelectedComment != null && Comments.Count > 0 && SelectedComment.User.UserId == Singleton.GetLoggedIn()._loggedInUser.UserId)
				RemoveCommentVisibility = Visibility.Visible;
			else
				RemoveCommentVisibility = Visibility.Collapsed;
		}

		private void DeleteComment(object parameter)
		{
			if (SelectedComment != null) 
			{
				PostController pc = new PostController();
				pc.DeleteComment(SelectedComment.CommentId);
				LoadComments();
			}
		}

		public void CreateNewComment(object parameter)
		{
			UserController uc = new UserController();
			PostController pc = new PostController();
			CommentController cc = new CommentController();

			cc.AddCommentToPost(uc.GetUser(Singleton.GetLoggedIn()._loggedInUser.UserId), Text, SelectedPost.id);
			Text = null; //sätter textboxten tom igen 
			LoadComments();
		}

		private void Upvote(object parameter)
		{
			if (SelectedPost != null)
			{
				PostController pc = new PostController();
				pc.UpvotePost(SelectedPost.id);
				// tas bort det som finns i feeden för att få de nya väderna
				Friends.Clear();
				Posts.Clear();
				LoadFriends(parameter);
			}
		}
		private void DownVote(object parameter)
		{
			if (SelectedPost != null)
			{
				PostController pc = new PostController();
				pc.DownvotePost(SelectedPost.id);
				Friends.Clear();
				Posts.Clear();
				LoadFriends(parameter);
			}
		}
		private void LoadComments()
		{
			Comments.Clear();
			PostController pc = new PostController();
			if (SelectedPost != null)
			{
				Comments = pc.GetComments(SelectedPost.id);
			}
		}

		private void LoadFriendPosts()
		{
			foreach (var item in Friends)
			{
				foreach (var posts in item.Posts)
				{
					Posts friendPost = new Posts(posts.id, posts.User, posts);

					if (posts.Image != null)
					{
						using (var ms = new MemoryStream(posts.Image))
						{
							BitmapImage bmi = new BitmapImage();
							bmi.BeginInit();
							bmi.CacheOption = BitmapCacheOption.OnLoad;
							bmi.StreamSource = ms;
							bmi.EndInit();
							friendPost.PostImage = new Image();
							friendPost.PostImage.Source = bmi;
						}
					}
					Posts.Add(friendPost);
				}
			}
			Posts = SortPosts(orderByAscending);
		}
		private void LoadFriends(object parameter)
		{
			UserController uc = new UserController();
			FriendController fc = new FriendController();
			ObservableCollection<Friend> tempFriend = fc.GetFriendsByUser(Singleton.GetLoggedIn()._loggedInUser);

			if (tempFriend != null)
			{
				foreach (var item in tempFriend)
					if (item.To.UserId != Singleton.GetLoggedIn()._loggedInUser.UserId)
						Friends.Add(uc.GetUsersWithPosts(item.To));
					else
						Friends.Add(uc.GetUsersWithPosts(item.From));
			}
			LoadFriendPosts();
		}
		private void SortRecent(object parameter)
		{
			Posts = SortPosts(orderByAscending = true);
		}
		private void SortOldest(object parameter)
		{
			Posts = SortPosts(orderByAscending = false);
		}
		private ObservableCollection<Posts> SortPosts(bool ascending)
		{
			if (ascending)
				return new ObservableCollection<Posts>(Posts.OrderBy(p => p.Post.PublishDate));
			else
				return new ObservableCollection<Posts>(Posts.OrderByDescending(p => p.Post.PublishDate));
		}
	}
}
