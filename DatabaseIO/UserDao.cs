using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DatabaseIO
{
    public class UserDao
    {
        MyDB mydb = new MyDB();
        
        public usercgv getInformation(string email)
        {
            return mydb.usercgvs.Where(u => u.email == email).FirstOrDefault();
        }
        public usercgv getUpdateProfile(string email, string password)
        {
            return mydb.usercgvs.Where(u => u.email == email && u.password == password).FirstOrDefault();
        }
        public void updatePassword(string email, string password,string passwordNew)
        {
            usercgv u = getUpdateProfile(email, password);
            u.password = passwordNew;
            mydb.SaveChanges();
        }
        public void updateProfile(string email, usercgv user)
        {
            usercgv usercgv = getInformation(email);
            usercgv.phonenumber = user.phonenumber;
            usercgv.username = user.username;
            mydb.SaveChanges();
        }
       
    }
}
