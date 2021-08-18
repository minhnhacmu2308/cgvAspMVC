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
        public void Add(string name)
        {
            string SQL = "INSERT INTO category_post(name) VALUES('" + name + "')";
            mydb.Database.ExecuteSqlCommand(SQL);

        }
        public void Update(string name, string id)
        {
            string SQL = "UPDATE category_post SET name = '" + name + "' WHERE id = '" + id + "'";
            mydb.Database.ExecuteSqlCommand(SQL);
        }
        public void Delete(string id)
        {
            string SQL = "DELETE FROM category_post WHERE id = '" + id + "'";
            mydb.Database.ExecuteSqlCommand(SQL);
        }
    }
}
