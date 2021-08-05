using Model;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DatabaseIO
{
    public  class CommentDao
    {
        MyDB mydb = new MyDB();

        public List<rating> getCommentById(int id)
        {
            return mydb.ratings.Where(r => r.film_id == id).ToList();
        }
        public void comment(rating rating)
        {
            string SQL = "INSERT INTO ratings( film_id, rate,id_user,name_user) VALUES (@filmId,@rate,@userId,@nameuser)";
            mydb.Database.ExecuteSqlCommand(SQL, new SqlParameter("@filmId", rating.film_id),
                new SqlParameter("@rate", rating.rate),
                new SqlParameter("@userId", rating.id_user),
                new SqlParameter("@nameuser", rating.name_user)
            );
        }
    }
}
