using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities
{
    public class Comment
    {
        public int CommentId { get; set; }
        public string Text { get; set; }
        public DateTime PublishDate { get; set; }
        public virtual User User { get; set; }
        public Post Post { get; set; } 
         
    }
}
