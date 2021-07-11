using DataAccessLayer.Concrete;
using EntityLayer.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Concrete
{
    public class AuthorManager
    {
        Repository<Author> repoAuthor = new Repository<Author>();

        public List<Author> GetAll()
        {
            return repoAuthor.List();
        }

        public int AuthorAdd(Author a)
        {
            if (a.User.Name==""||a.User.Surname==""||a.User.ShortAbout==""||a.User.EmailAddress==""|a.User.PhoneNumber==""||a.User.Password=="")
            {
                return -1;
            }
            a.IsAuthor = true;
            return repoAuthor.Insert(a);
        }

        public Author FindAuthor(int getid)
        {
            return repoAuthor.Find(x => x.id == getid);
        }

        public int EditAuthor(Author p)
        {
            Author author = repoAuthor.Find(x => x.id == p.id);
            author.User.Name = p.User.Name;
            author.User.Surname = p.User.Surname;
            author.User.EmailAddress = p.User.EmailAddress;
            author.User.PhoneNumber = p.User.PhoneNumber;
            author.User.About = p.User.About;
            author.User.ShortAbout = p.User.ShortAbout;
            author.User.Password = p.User.Password;
            author.User.Image = p.User.Image;
            author.User.RoleId = p.User.RoleId;
            author.AuthorCategoryId= p.AuthorCategoryId;

            return repoAuthor.Update(author);
        }

        public List<Author> AuthorByStatusFalse()
        {
            return repoAuthor.List(x => x.IsAuthor == false);
        }
        public int AuthorStatusChangeToTrue(int id)
        {
            Author author = repoAuthor.Find(x => x.id == id);
            author.IsAuthor = true;
            author.User.RoleId = 1;
            return repoAuthor.Update(author);
        }

    }
}
