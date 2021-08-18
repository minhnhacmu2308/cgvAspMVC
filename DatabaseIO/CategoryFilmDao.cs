using Model;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DatabaseIO
{
   
    public class CategoryFilmDao
    {
        MyDB mydb = new MyDB();
        public List<category_film> getAllCategoryFilm()
        {
            return mydb.category_film.ToList();
        }
        public void Add(string name)
        {
            string SQL = "INSERT INTO category_film(name) VALUES(N'" + name + "')";
            mydb.Database.ExecuteSqlCommand(SQL);

        }
        public void Update(string name, string id)
        {
            string SQL = "UPDATE category_film SET name = N'" + name + "' WHERE id = '" + id + "'";
            mydb.Database.ExecuteSqlCommand(SQL);
        }
        public void Delete(string id)
        {
            string SQL = "DELETE FROM category_film WHERE id = '" + id + "'";
            mydb.Database.ExecuteSqlCommand(SQL);
        }
        public bool checkName(string name)
        {
            string namef = "%" + name + "%";
            string sql = "SELECT * FROM category_film WHERE name LIKE @name";
            var user = mydb.Database.SqlQuery<category_film>(sql,new SqlParameter("@name",namef)).FirstOrDefault();
            if (user != null)
            {
                return true;
            }
            return false; ;
        }
    }
}
