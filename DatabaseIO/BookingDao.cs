using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model;
namespace DatabaseIO
{
    
    public class BookingDao
    {
        MyDB mydb = new MyDB();
        public void Accpect(string id)
        {
            string SQL = "UPDATE booking SET status = 1 WHERE id = '" + id + "'";
            mydb.Database.ExecuteSqlCommand(SQL);
        }
    }
}
