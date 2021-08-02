using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model;
namespace DatabaseIO
{
    public class UserDao
    {
        MyDB mydb = new MyDB();
        public List<usercgv> getAll()
        {
            return mydb.usercgvs.ToList();
        }
    }
}
