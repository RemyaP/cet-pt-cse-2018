using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;
using DataAccess.EntityModels;

namespace RealEstateManagement.Models
{
    public class CategoryViewModel
    {
        private static Dictionary<int, string> _categories = null;
        public static Dictionary<int, string> GetCategories()
        {
            if( null != _categories ) return _categories;
            using( RealEntities db = new RealEntities() )
            {
                _categories = db.categories.ToDictionary( c => c.category_id, c => c.type );

            }
            return _categories;
        }
    }
}