using EntityLayer.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BASARSOFTHABER.Models.HomeModel
{
    public class HomeModel
    {
        public List<News> News { get; set; }
        public Category Category { get; set; }
    }
}