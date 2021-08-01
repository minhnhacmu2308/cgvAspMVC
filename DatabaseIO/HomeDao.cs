using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DatabaseIO
{
    public class HomeDao
    {
        MyDB mydb = new MyDB();

        public List<film> getFilmComingSoon()
        {
            return mydb.films.ToList();
        }
        public List<film> getFilmNowShowing()
        {
            return mydb.films.ToList();
        }
        public List<post> getPromotion()
        {
            return mydb.posts.Where(p => p.id_cpost == 1).ToList();
        }
    }
}
