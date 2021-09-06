﻿using Model;
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
        public List<post> getListIntroduce()
        {
            return mydb.posts.Where(p => p.id_cpost == 2).ToList();
        }
        public room getName(int id)
        {
            return mydb.rooms.Where(r => r.id == id).FirstOrDefault();
        }
        public List<post> getAll()
        {
            return mydb.posts.ToList();
        }
        public void add(string title, string idcate, string image, string description)
        {
            string SQL = "INSERT INTO post(title, description, image, id_cpost) VALUES(N'" + title + "',N'" + description + "','" + image + "','" + idcate + "')";
            mydb.Database.ExecuteSqlCommand(SQL);

        }
        public void update(string title, string idcate, string image, string description, string id)
        {
            string SQL = "UPDATE post SET title = N'" + title + "',description = N'" + description + "',image = '" + image + "',id_cpost = '" + idcate + "' WHERE id = '" + id + "'";
            mydb.Database.ExecuteSqlCommand(SQL);
        }
        public void delete(string id)
        {
            string SQL = "DELETE FROM post WHERE id = '" + id + "'";
            mydb.Database.ExecuteSqlCommand(SQL);
        }
    }
}
