using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DatabaseIO
{
   
    public class CategoryFilmDao
    {
        MyDB mydb = new MyDB();
        public List<category_film> getAllCategoryFilm()
        {
            return mydb.category_film.ToList();
        }
    }
}
