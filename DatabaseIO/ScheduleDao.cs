﻿using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DatabaseIO
{
    public class ScheduleDao
    {
        MyDB mydb = new MyDB();
        public List<schedule> getSchedule(string id)
        {
                int filmId = Int32.Parse(id);
            string SQL = "Select * from schedules where film_id = '" + id + "' and YEAR(dateschedule) >= YEAR(getdate()) and MONTH(dateschedule) >= MONTH(getdate()) and DAY(dateschedule) >= DAY(getdate())";

           return mydb.Database.SqlQuery<schedule>(SQL).ToList();
        }
        public schedule getName(int id)
        {
            return mydb.schedules.Where(s => s.id == id).FirstOrDefault();
        }
        public List<schedule> getAll()
        {
            return mydb.schedules.ToList();
        }
        public void Add(string filmid, string dateschedule)
        {
            string SQL = "INSERT INTO schedules(film_id,dateschedule) VALUES('" + filmid + "','" + dateschedule + "')";
            mydb.Database.ExecuteSqlCommand(SQL);

        }
        public void Update(string filmid, string dateschedule, string id)
        {
            string SQL = "UPDATE schedules SET film_id = '" + filmid + "', dateschedule = '" + dateschedule + "' WHERE id = '" + id + "'";
            mydb.Database.ExecuteSqlCommand(SQL);
        }
        public void Delete(string id)
        {
            string SQL = "DELETE FROM schedules WHERE id = '" + id + "'";
            mydb.Database.ExecuteSqlCommand(SQL);
        }
        public bool checkName(DateTime datesche,int id)
        {
            var user = mydb.schedules.Where(u => u.dateschedule == datesche && u.film_id == id).FirstOrDefault();
            if (user != null)
            {
                return true;
            }
            return false; ;
        }
    }
}
