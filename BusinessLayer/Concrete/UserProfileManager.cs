using DataAccessLayer.Concrete;
using EntityLayer.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Concrete
{
    public class UserProfileManager
    {
        Repository<Author> repouser = new Repository<Author>();
        Repository<News> repousernews = new Repository<News>();
        public List<Author> GetUserByMail(string p)
        {
            return repouser.List(x => x.User.EmailAddress == p);
        }

        public List<News> GetNewsByAuthor(int id)
        {
            return repousernews.List(x => x.AuthorId == id);
        }

        public Author FindAuthor(int getid)
        {
            return repouser.Find(x => x.id == getid);
        }

        public int EditAuthor(Author p)
        {
            Author author = repouser.Find(x => x.id == p.id);
            author.User.Name = p.User.Name;
            author.User.Surname = p.User.Surname;
            author.User.EmailAddress = p.User.EmailAddress;
            author.User.PhoneNumber = p.User.PhoneNumber;
            author.User.About = p.User.About;
            author.User.ShortAbout = p.User.ShortAbout;
            author.User.Password = p.User.Password;
            author.User.Image = p.User.Image;

            return repouser.Update(author);
        }
    }
}
