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
        public List<CommentAjax> addCommentAjax(List<rating> listcomment)
        {
            var listAjax = new List<CommentAjax>();
            CommentAjax commentA = null;
            int lengthComment = listcomment.Count;
            for (int i = 0; i <lengthComment; ++i)
            {
                commentA = new CommentAjax();
                commentA.id = listcomment[i].id;
                commentA.film_id = listcomment[i].film_id;
                commentA.id_user = listcomment[i].id_user;
                commentA.number_start = listcomment[i].number_start;
                commentA.rate = listcomment[i].rate;
                commentA.name_user = listcomment[i].name_user;
                commentA.created_time = listcomment[i].created_time.ToString();
                listAjax.Add(commentA);
            }
            return listAjax;
        }
    }
}
