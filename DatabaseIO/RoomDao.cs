﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model;
namespace DatabaseIO
{
    public class RoomDao
    {
        MyDB mydb = new MyDB();
        public List<room> getAll()
        {
            return mydb.rooms.ToList();
        }
    }
}
