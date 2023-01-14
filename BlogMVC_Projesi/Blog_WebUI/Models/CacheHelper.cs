using Blog_BusinessLayer;
using Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Helpers;

namespace Blog_WebUI.Models
{
    public class CacheHelper
    {
        public static List<Category> GetCategoriesFromCache()
        {
            var result = WebCache.Get("category");
            if (result == null)
            {
                CategoryManager categoryManager = new CategoryManager();
                result = categoryManager.List();
                WebCache.Set("category", result, 20, true);
            }
            return result;
        }
        public static void RemoveCategoriesFromCache()
        {
            WebCache.Remove("category");
        }
        public static void Remove(string key)
        {
            WebCache.Remove(key);
        }
    }
}