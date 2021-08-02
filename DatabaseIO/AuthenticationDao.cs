using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security.Cryptography;
using Model;

namespace DatabaseIO
{
    public class AuthenticationDao
    {
        MyDB mydb = new MyDB();
        public string md5(string password)
         {
                MD5 md = MD5.Create();
                byte[] inputString = System.Text.Encoding.ASCII.GetBytes(password);
                byte[] hash = md.ComputeHash(inputString);
                StringBuilder sb = new StringBuilder();
                for(int i = 0; i<hash.Length;i++)
                {
                    sb.Append(hash[i].ToString("x"));
                }
                return sb.ToString();
         }
        public bool checkEmail(string email)
        {
            var user = mydb.usercgvs.Where(u => u.email == email).FirstOrDefault();
            if(user != null)
            {
                return true;
            }
            return false; ;
        }
        public bool checklogin(string email,string password)
        {
            var result = mydb.usercgvs.Where(u => u.email == email && u.password == password).FirstOrDefault();
            if(result != null)
            {
                return true;

            }
            else
            {
                return false;
            }
        }

    }
}
