using DataAccessLayer.Concrete;
using EntityLayer.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Concrete
{
    public class CommentManager
    {
        Repository<Comment> repocomment = new Repository<Comment>();

        public List<Comment> CommentList()
        {
            return repocomment.List();
        }

        public List<Comment> CommenByNews(int id)
        {
            return repocomment.List(x => x.NewsId == id);
        }

        public List<Comment> CommentByStatusTrue()
        {
            return repocomment.List(x => x.CommentStatus == true);
        }
        public List<Comment> CommentByStatusFalse()
        {
            return repocomment.List(x => x.CommentStatus == false);
        }
        public int CommentAdd(Comment c)
        {
            if (c.CommentContent.Length <= 4 || c.CommentContent.Length > 301 || c.FullName == "" || c.EmailAddress == "" || c.FullName.Length <= 5)
            {
                return -1;
            }
            c.CommentStatus = false;
            return repocomment.Insert(c);
        }

        public int CommentStatusChangeToFalse(int id)
        {
            Comment comment = repocomment.Find(x => x.id == id);
            comment.CommentStatus = false;
            return repocomment.Update(comment);
        }

        public int CommentStatusChangeToTrue(int id)
        {
            Comment comment = repocomment.Find(x => x.id == id);
            comment.CommentStatus = true;
            return repocomment.Update(comment);
        }
    }
}
