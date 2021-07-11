
using DataAccessLayer.Concrete;
using EntityLayer.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Concrete
{
    public class UserMemberManager
    {
        Repository<User> repous = new Repository<User>();
        Repository<Author> repoautus = new Repository<Author>();
        public List<User> GetUserByMail(string p)
        {
            return repous.List(x => x.EmailAddress == p);
        }

        public User FindUser(int getid)
        {
            return repous.Find(x => x.id == getid);
        }

        public int EditProfile(User p)
        {
            User user = repous.Find(x => x.id == p.id);
            user.Name = p.Name;
            user.Surname = p.Surname;
            user.EmailAddress = p.EmailAddress;
            user.PhoneNumber = p.PhoneNumber;
            user.About = p.About;
            user.ShortAbout = p.ShortAbout;
            user.Password = p.Password;
            user.Image = p.Image;
            user.RoleId = p.RoleId;
            
            return repous.Update(user);
        }

        public int EditProf(User p)
        {
            User user = repous.Find(x => x.id == p.id);
            user.Name = p.Name;
            user.Surname = p.Surname;
            user.EmailAddress = p.EmailAddress;
            user.PhoneNumber = p.PhoneNumber;
            user.About = p.About;
            user.ShortAbout = p.ShortAbout;

            return repous.Update(user);
        }
        public int UserAuthorAdd(Author a)
        {
            return repoautus.Insert(a);
        }

        public int AuthorDelete(int id)
        {
            Author author = repoautus.Find(x => x.id == id);
            return repoautus.Delete(author);
        }
    }
}
