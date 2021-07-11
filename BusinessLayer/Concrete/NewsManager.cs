using DataAccessLayer.Concrete;
using EntityLayer.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Concrete
{
    public class NewsManager
    {
        Repository<News> reponews = new Repository<News>();

        public List<News> GetAll()
        {
            return reponews.List();
        }

        public List<News> GetNewsById(int id)
        {
            return reponews.List(x => x.id == id);
        }
        public List<News> GetNewsAuthorId(int id)
        {
            return reponews.List(x => x.AuthorId == id);
        }

        public List<News> GetNewsByCategory(int id)
        {
            return reponews.List(x => x.CategoryId == id);
        }

        public int NewsAddBl(News p)
        {
            if (p.NewsName == "" || p.Subtitle == "" || p.NewsName.Length <= 5 || p.NewsContent == "" || p.NewsContent.Length <= 200)
            {
                return -1;
            }
           
            return reponews.Insert(p);
        }


        public int NewsDelete(int p)
        {
            News news = reponews.Find(x => x.id == p);
            return reponews.Delete(news);
        }

        public News FindNews(int getid)
        {
            return reponews.Find(x => x.id == getid);
        }

        public int NewsUpdate(News p)
        {
            News news = reponews.Find(x => x.id == p.id);
            news.NewsName = p.NewsName;
            news.Subtitle = p.Subtitle;
            news.ModifiedDate = p.ModifiedDate;
            news.Image = p.Image;
            news.NewsContent = p.NewsContent;
            news.CategoryId = p.CategoryId;
            news.AuthorId = p.AuthorId;
            return reponews.Update(news);
        }
    }
}
