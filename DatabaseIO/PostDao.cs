using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DatabaseIO
{

    public class PostDao
    {
        MyDB mydb = new MyDB();

        public post getDetailPromotion(string id)
        {
            int promotionId = Int32.Parse(id);
            return mydb.posts.Where(p => p.id == promotionId).FirstOrDefault();
        }
        public room getName(int id)
        {
            return mydb.rooms.Where(r => r.id == id).FirstOrDefault();
        }
        public List<post> getAll()
        {
            return mydb.posts.ToList();
        }
    }
}
