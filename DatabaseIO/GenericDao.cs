using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DatabaseIO
{
    public class GenericDao
    {
        MyDB mydb = new MyDB();
        public void AddObject<T>(T obj)
        {
            mydb.Set(obj.GetType()).Add(obj);
        } 
        public void Save()
        {
            mydb.SaveChanges();
        }
    }
}
