using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DatabaseIO;
using Model;
namespace CGV.Controllers.Admin
{
    public class AdminRoomController : Controller
    {
        RoomDao room = new RoomDao();
        SeatDao seatD = new SeatDao();
        public MyDB db = new MyDB();
        // GET: AdminRoom
        public ActionResult Index(string mess)
        {
            if (Session["usr"] == null)
            {
                return RedirectToAction("Index", "AdminAuthen");
            }
            ViewBag.Msg = mess;
            Session["checkactive"] = "room";
            Utils.CheckActive.checkActive();
            List<room> list = db.rooms.ToList();        
            return View(list);
        }
        [HttpPost]
        public ActionResult Add(FormCollection form)
        {
            var listAll = seatD.getAll();
            var name = form["roomname"];
            var soluong = form["number_seat"];
            bool result = room.checkName(name);
            if (Session["usr"] == null)
            {
                return RedirectToAction("Index", "AdminAuthen");
            }
            else
            {
                if (result)
                {
                    var message = "1";
                    return RedirectToAction("Index", new { mess = message });
                }
                else
                {
                    room.Add(name);
                    var roomByID = room.getId(name);
                    for (int i = 0; i < Int32.Parse(soluong); i++)
                    {
                        room.AddRoomSeat(roomByID.id, listAll[i].id);
                    }
                    var message = "2";
                    return RedirectToAction("Index", new { mess = message });
                }
            }
          
        }
        public ActionResult Update(FormCollection form)
        {
            var name = form["roomname"];
            var id = form["id"];
            var number = form["number_seat1"];
         
            var roomname = room.getName(Int32.Parse(id));
            var listAll = seatD.getAll();
            if (Session["usr"] == null)
            {
                return RedirectToAction("Index", "AdminAuthen");
            }
            else
            {
                if (roomname.room_name == name)
                {
                    room.DeleteRoomSeat(Int32.Parse(id));
                    room.Update(name, id);
                    var roomByID = room.getId(name);
                    for (int i = 0; i < Int32.Parse(number); i++)
                    {
                        room.AddRoomSeat(roomByID.id, listAll[i].id);
                    }
                    var message = "2";
                    return RedirectToAction("Index", new { mess = message });
                }
                else
                {
                    bool result = room.checkName(name);
                    if (result)
                    {
                        var message = "1";
                        return RedirectToAction("Index", new { mess = message });
                    }
                    else
                    {
                        room.DeleteRoomSeat(Int32.Parse(id));
                        room.Update(name, id);
                        var roomByID = room.getId(name);
                        for (int i = 0; i < Int32.Parse(number); i++)
                        {
                            room.AddRoomSeat(roomByID.id, listAll[i].id);
                        }
                        var message = "2";
                        return RedirectToAction("Index", new { mess = message });
                    }
                }


            }


        }
        public ActionResult Delete(FormCollection form)
        {
            var id = form["id"];
            if (Session["usr"] == null)
            {
                return RedirectToAction("Index", "AdminAuthen");
            }
            else
            {
                bool checkActive = room.checkActive(Int32.Parse(id));
                if (checkActive)
                {
                    var message = "3";
                    return RedirectToAction("Index", new { mess = message });
                }
                else
                {
                    room.DeleteRoomSeat(Int32.Parse(id));
                    room.Delete(id);
                    var message = "2";
                    return RedirectToAction("Index", new { mess = message });
                }
              
               
            }
           

        }
    }
}