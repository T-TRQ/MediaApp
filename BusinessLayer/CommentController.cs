using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataLayer;
using Entities;
using System.Collections.ObjectModel;

namespace BusinessLayer
{
    public class CommentController
    {
        private AppDbContext AppDbContext { get; set; }
        private UnitOfWork UnitOfWork { get; set; }

        public CommentController()
        {
            AppDbContext = new AppDbContext();
            UnitOfWork = new UnitOfWork(AppDbContext);
        }


        public void AddCommentToPost(User user, string text, int id)
        {
            user = UnitOfWork.Users.Get(user.UserId);
            Comment comment = new Comment();
            comment.PublishDate = DateTime.Now;
            comment.User = UnitOfWork.Users.Get(user.UserId);
            comment.Text = text;
            comment.Post = UnitOfWork.Posts.Get(id);
            UnitOfWork.Comments.Add(comment);
            UnitOfWork.complete();
        }

        public Comment GetComment(int id)
        {
            return UnitOfWork.Comments.Get(id);
        }
    }
}