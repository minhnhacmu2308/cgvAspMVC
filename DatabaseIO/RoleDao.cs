using System.Collections.Generic;
using System.Linq;
using Model;

namespace DatabaseIO
{
    /**
     * RoleDao
     * 
     * Version 1.0
     * 
     * Date 07-08-2021
     * 
     * Copyright
     * 
     * Modification Logs:
     * DATE            AUTHOR            DESCRIPTION
     * ----------------------------------------------
     * 07-08-2021      NhaNM2              Create
     */
    public class RoleDao
    {
        MyDB db = new MyDB();

        /**
         * get list role from database 
         * @return
         */
        public List<role> getAll()
        {           
            return db.roles.ToList();
        }
    }
}
