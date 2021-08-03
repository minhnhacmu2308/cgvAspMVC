using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DatabaseIO
{
    public class FilmDao
    {
        MyDB mydb = new MyDB();

        public film getDetailFilm(string id)
        {
            int filmId = Int32.Parse(id);
            return mydb.films.Where(f => f.id == filmId).FirstOrDefault();
        }
        public IEnumerable<film> searchFilm(string keySearch)
        {
            return mydb.films.Where(f => f.film_name.Contains(keySearch) || f.actor.Contains(keySearch) || f.director.Contains(keySearch));
        }
        public List<film> getAll()
        {
            return mydb.films.ToList();
        }
    }
}
