using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model;
namespace DatabaseIO
{
    public class CategoryPostDao
    {
        MyDB mydb = new MyDB();
        public List<category_post> getAll()
        {
            return mydb.category_post.ToList();
        }
    }
}
