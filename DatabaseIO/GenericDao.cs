using Model;

namespace DatabaseIO
{
    /**
     * GenericDao
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
    public class GenericDao
    {
        MyDB mydb = new MyDB();
        public void AddObject<T>(T obj)
        {
            mydb.Set(obj.GetType()).Add(obj);
        } 
        public void Save()
        {
            mydb.SaveChanges();
        }
    }
}
