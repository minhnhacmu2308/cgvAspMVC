using System;
using System.Collections.Generic;
using System.Data.SqlClient;
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
        public void add(string name)
        {
            string SQL = "INSERT INTO category_post(name) VALUES(N'" + name + "')";
            mydb.Database.ExecuteSqlCommand(SQL);
        }
        public void update(string name, string id)
        {
            string SQL = "UPDATE category_post SET name = N'" + name + "' WHERE id = '" + id + "'";
            mydb.Database.ExecuteSqlCommand(SQL);
        }
        public void delete(string id)
        {
            string SQL = "DELETE FROM category_post WHERE id = '" + id + "'";
            mydb.Database.ExecuteSqlCommand(SQL);
        }
        public bool checkName(string name)
        {
            string sql = "SELECT * FROM category_post WHERE name = N'" + name + "'";
            var user = mydb.Database.SqlQuery<category_post>(sql).FirstOrDefault();
            if (user != null){
                return true;
            }
            return false;
        }
        public bool checkUpdate(int id, string name)
        {
            string sql = "SELECT * FROM category_post WHERE name = N'" + name + "' and id = @id";
            var user = mydb.Database.SqlQuery<category_post>(sql, new SqlParameter("@id", id)).FirstOrDefault();
            if (user != null){
                return true;
            }
            return false;
        }
        public bool checkActive(int id)
        {
            string sql = "SELECT * FROM post WHERE id_cpost = @id";
            List<post> user = mydb.Database.SqlQuery<post>(sql, new SqlParameter("@id", id)).ToList();
            if (user.Count == 0){
                return true;
            }
            return false;
        }
    }
}
