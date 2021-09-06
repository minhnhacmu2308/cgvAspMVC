using Model;
using System.Collections.Generic;
using System.Linq;


namespace DatabaseIO
{
    public class HomeDao
    {
        MyDB mydb = new MyDB();

        /**
         * get list film comming soon from database 
         * @return
         */
        public List<film> getFilmComingSoon()
        {
            return mydb.Database.SqlQuery<film>("SELECT * FROM films WHERE CONVERT(varchar, premiere_date, 101) > CONVERT(varchar, getdate(), 101)").ToList();
        }

        /**
         * get list film now showing from database 
         * @return
         */
        public List<film> getFilmNowShowing()
        {
            return mydb.Database.SqlQuery<film>("SELECT * FROM films WHERE CONVERT(varchar, premiere_date, 101) <= CONVERT(varchar, getdate(), 101)").ToList();
        }

        /**
         * get list introduce from database 
         * @return
         */
        public List<post> getPromotion()
        {
            return mydb.posts.Where(p => p.id_cpost == 1).ToList();
        }

        /**
         * get sum amount from database 
         * @param month
         * @return
         */
        public int statictis(int month)
        {
            string SQL = "Select SUM(a.amount)  FROM booking as a,schedules as b WHERE a.schedule_id = b.id GROUP BY  MONTH(b.dateschedule) HAVING MONTH(b.dateschedule) = '" + month + "'";
            int result = mydb.Database.SqlQuery<int>(SQL).FirstOrDefault();
            return result;
        }

        /**
         * get number ticket by month from database 
         * @param month
         * @return
         */
        public int countTicket(int month)
        {
            string SQL = "Select COUNT(a.id)  FROM booking as a,schedules as b WHERE a.schedule_id = b.id GROUP BY  MONTH(b.dateschedule) HAVING MONTH(b.dateschedule) = '" + month + "'";
            int result = mydb.Database.SqlQuery<int>(SQL).FirstOrDefault();
            return result;
        }

        /**
         * get list films now from database 
         * @return
         */
        public List<film> getFilmNow()
        {
            return mydb.Database.SqlQuery<film>("SELECT DISTINCT  a.* FROM films as a,schedules as b, showtimes as c WHERE b.film_id = a.id AND c.schedule_id = b.id AND FORMAT(b.dateschedule, 'dd/MM/yyyy' ) = FORMAT(getdate(), 'dd/MM/yyyy' ) and c.start_time >= convert(varchar(32),getdate(),108)").ToList();
        }
    }
}
