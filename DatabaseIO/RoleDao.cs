using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model;
namespace DatabaseIO
{
    public class RoleDao
    {
        MyDB db = new MyDB();
        public List<role> getAll()
        {           
            return db.roles.ToList();
        }
    }
}
