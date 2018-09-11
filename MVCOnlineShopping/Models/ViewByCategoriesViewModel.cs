using MvcPaging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MVConlineshopping.Models
{
    public class ViewByCategoriesViewModel
    {
        public PagedList<Article> Articles { get; set; }
        //public string[] AvailableCategories { get; set; }
        public List<Category> Categories { get; set; }
    }
}