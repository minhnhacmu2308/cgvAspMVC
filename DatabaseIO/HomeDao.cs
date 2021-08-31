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
            return mydb.Database.SqlQuery<film>("SELECT a.* FROM films as a WHERE FORMAT(a.premiere_date, 'dd/MM/yyyy' ) > FORMAT(getdate(), 'dd/MM/yyyy' )").ToList();
        }
        public List<film> getFilmNowShowing()
        {
            return mydb.Database.SqlQuery<film>("SELECT a.* FROM films as a WHERE FORMAT(a.premiere_date, 'dd/MM/yyyy' ) <= FORMAT(getdate(), 'dd/MM/yyyy' )").ToList();
        }
        public List<post> getPromotion()
        {
            return mydb.posts.Where(p => p.id_cpost == 1).ToList();
        }
        public int Statictis(int month)
        {
            string SQL = "Select SUM(a.amount)  FROM booking as a,schedules as b WHERE a.schedule_id = b.id GROUP BY  MONTH(b.dateschedule) HAVING MONTH(b.dateschedule) = '" + month + "'";
            int result = mydb.Database.SqlQuery<int>(SQL).FirstOrDefault();
            return result;
        }
        public int CountTicket(int month)
        {
            string SQL = "Select COUNT(a.id)  FROM booking as a,schedules as b WHERE a.schedule_id = b.id GROUP BY  MONTH(b.dateschedule) HAVING MONTH(b.dateschedule) = '" + month + "'";
            int result = mydb.Database.SqlQuery<int>(SQL).FirstOrDefault();
            return result;
        }
        public List<film> getFilmNow()
        {
            return mydb.Database.SqlQuery<film>("SELECT DISTINCT  a.* FROM films as a,schedules as b, showtimes as c WHERE b.film_id = a.id AND c.schedule_id = b.id AND FORMAT(b.dateschedule, 'dd/MM/yyyy' ) = FORMAT(getdate(), 'dd/MM/yyyy' ) and c.start_time >= convert(varchar(32),getdate(),108)").ToList();
        }
    }
}
