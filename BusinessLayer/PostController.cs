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
	public class PostController
	{
		private AppDbContext AppDbContext { get; set; }
		private UnitOfWork UnitOfWork { get; set; }

		public PostController()
		{
			AppDbContext = new AppDbContext();
			UnitOfWork = new UnitOfWork(AppDbContext);
		}

		public ObservableCollection<Post> GetWholePost(int id)
		{
			return UnitOfWork.Posts.GetWholePostsByUser(id);
		}

		public ObservableCollection<Post> GetWholePostsBySearchTerm(int id, string searchTerm)
		{
			return UnitOfWork.Posts.GetWholePostsBySearchTerm(id, searchTerm);
		}

		public ObservableCollection<Post> GetPosts()
		{
			return new ObservableCollection<Post>(UnitOfWork.Posts.GetAll());
		}
		public ObservableCollection<Post> SearchPostByKeyword(string search)
		{
			return new ObservableCollection<Post>(UnitOfWork.Posts.Find(s => s.Text.Contains(search)));
		}
		public Post GetPost(int id)
		{
			return UnitOfWork.Posts.Get(id);
		}
		public void RemovePost(int id)
		{
			Post removePost = UnitOfWork.Posts.Get(id);
			UnitOfWork.Posts.Remove(removePost);
			UnitOfWork.complete();
		}

		//William
		public void UpvotePost(int id)
		{
			var post = UnitOfWork.Posts.Get(id);
			post.ReactionCount++;
			UnitOfWork.complete();
		}
		public void DownvotePost(int id)
		{
			var post = UnitOfWork.Posts.Get(id);
			post.ReactionCount--;
			UnitOfWork.complete();
		}

		//William
		public ObservableCollection<Comment> GetComments(int id)
		{
			return new ObservableCollection<Comment>(UnitOfWork.Posts.Get(id).Comments);
		}
		//William
		public void DeleteComment(int id)
		{
			UnitOfWork.Comments.Remove(UnitOfWork.Comments.Get(id));
			UnitOfWork.complete();
		}


	}
}
