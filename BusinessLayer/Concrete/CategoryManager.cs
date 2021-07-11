using DataAccessLayer.Concrete;
using EntityLayer.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Concrete
{
    public class CategoryManager
    {
        Repository<Category> repocategory = new Repository<Category>();
        Repository<AuthorCategory> repoauthorcat = new Repository<AuthorCategory>();
        public List<Category> GetAll()
        {
            return repocategory.List();
        }

        public int CategoryDelete(int p)
        {
            Category category = repocategory.Find(x => x.id == p);
            return repocategory.Delete(category);
        }

        public List<AuthorCategory> ListAllAutCat()
        {
            return repoauthorcat.List();
        }

        public AuthorCategory FindAuthorCat(int getid)
        {
            return repoauthorcat.Find(x => x.id == getid);
        }

        public int AuthorCategoryAdd(AuthorCategory a)
        {            
            return repoauthorcat.Insert(a);
        }

        public int EditAuthorCategory(AuthorCategory p)
        {
            AuthorCategory authorcat = repoauthorcat.Find(x => x.id == p.id);
            authorcat.CategoryName = p.CategoryName;
            authorcat.EditDate = p.EditDate;           

            return repoauthorcat.Update(authorcat);
        }

        public int AuthorCategoryDelete(int p)
        {
            AuthorCategory acategory = repoauthorcat.Find(x => x.id == p);
            return repoauthorcat.Delete(acategory);
        }
    }
}
