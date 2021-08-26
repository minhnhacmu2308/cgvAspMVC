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
        public rating getObjectCommentById(int id)
        {
            return mydb.ratings.Where(r => r.id == id).FirstOrDefault();
        }
        public void comment(rating rating)
        {
            string SQL = "INSERT INTO ratings( film_id, rate,id_user,name_user,number_start) VALUES (@filmId,@rate,@userId,@nameuser,@number)";
            mydb.Database.ExecuteSqlCommand(SQL, new SqlParameter("@filmId", rating.film_id),
                new SqlParameter("@rate", rating.rate),
                new SqlParameter("@userId", rating.id_user),
                  new SqlParameter("@number", rating.number_start),
                new SqlParameter("@nameuser", rating.name_user)
            );
        }
        public void deleteComment(int id)
        {
            var ObjectM = getObjectCommentById(id);
            mydb.ratings.Remove(ObjectM);
            mydb.SaveChanges();
        }
    }
}
