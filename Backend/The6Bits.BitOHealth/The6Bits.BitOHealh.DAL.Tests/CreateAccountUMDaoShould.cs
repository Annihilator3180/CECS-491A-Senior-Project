using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using The6Bits.BitOHealth.DAL.Implementations;
using The6Bits.BitOHealth.Models;

namespace The6Bits.BitOHealth.DAL.Tests
{
    public class CreateAccountUMDaoShould
    {
        [System.Diagnostics.Conditional("DEBUG")]
        public void InsertTest()
        {
            
            SqlUMDAO<User> UmDAO = new SqlUMDAO<User>();
            User rami = new User();
            rami.Username = "testname";
            rami.LastName = "test";
            rami.Email = "test@gmail.com";
            rami.IsAdmin = 0;
            rami.Password = "testpass123";
            rami.IsEnabled = 1;
            UmDAO.Create(rami);
            User ret = UmDAO.Read(rami);
            Debug.Assert( rami.Username == ret.Username );
        }


        [System.Diagnostics.Conditional("DEBUG")]

        public void UpdateTest()
        {
            SqlUMDAO<User> UmDAO = new SqlUMDAO<User>();
            User rami = new User();
            rami.Username = "testname";
            rami.LastName = "test";
            rami.Email = "test@gmail.com";
            rami.IsAdmin = 0;
            rami.Password = "testpass123";
            rami.IsEnabled = 1;
            UmDAO.Create(rami);
            User original = UmDAO.Read(rami);
            rami.Email = "test2@gmail.com";
            UmDAO.Update(rami);
            User updated = UmDAO.Read(rami);
            System.Diagnostics.Debug.WriteLine(updated.Email);
            UmDAO.Delete(rami);

            Debug.Assert("test@gmail.com" == updated.Email);
        }



        [System.Diagnostics.Conditional("DEBUG")]
        public void ReadTest()
        {
            SqlUMDAO<User> UmDAO = new SqlUMDAO<User>();
            User rami = new User();
            rami.Username = "testname";
            rami.LastName = "test";
            rami.FirstName = "test";
            rami.Email = "test@gmail.com";
            rami.IsAdmin = 0;
            rami.Password = "testpass123";
            rami.IsEnabled = 1;
            UmDAO.Create(rami);
            User original = UmDAO.Read(rami);
            UmDAO.Delete(rami);

            Debug.Assert(rami.Email==original.Email);
           
        }

        [System.Diagnostics.Conditional("DEBUG")]

        public void ReadAfterDeleteTest()
        {
            SqlUMDAO<User> UmDAO = new SqlUMDAO<User>();
            User rami = new User();
            rami.Username = "testname";
            rami.LastName = "test";
            rami.FirstName = "test";
            rami.Email = "test@gmail.com";
            rami.IsAdmin = 0;
            rami.Password = "testpass123";
            rami.IsEnabled = 1;
            UmDAO.Create(rami);
            UmDAO.Delete(rami);
            User original = UmDAO.Read(rami);

            Debug.Assert(null== original.Email);
        }





        [System.Diagnostics.Conditional("DEBUG")]
        public void EnableTest()
        {
            SqlUMDAO<User> UmDAO = new SqlUMDAO<User>();
            User rami = new User();
            rami.Username = "testname";
            rami.LastName = "test";
            rami.Email = "test@gmail.com";
            rami.IsAdmin = 0;
            rami.Password = "testpass123";
            rami.IsEnabled = 0;
            UmDAO.Create(rami);
            UmDAO.EnableAccount("testname");

            User updated = UmDAO.Read(rami);

            UmDAO.Delete(rami);

            System.Diagnostics.Debug.WriteLine(updated.IsEnabled);


           Debug.Assert(1 == updated.IsEnabled);
        }

        [System.Diagnostics.Conditional("DEBUG")]

        public void DisableTest()
        {
            SqlUMDAO<User> UmDAO = new SqlUMDAO<User>();
            User rami = new User();
            rami.Username = "testname";
            rami.LastName = "test";
            rami.Email = "test@gmail.com";
            rami.IsAdmin = 1;
            rami.Password = "testpass123";
            rami.IsEnabled = 1;
            UmDAO.Create(rami);

            UmDAO.DisableAccount("testname");

            User updated = UmDAO.Read(rami);
            UmDAO.Delete(rami);
           
            Debug.Assert(0 == updated.IsEnabled);
        }



        [System.Diagnostics.Conditional("DEBUG")]
        public void DeleteTest()
        {

            SqlUMDAO<User> UmDAO = new SqlUMDAO<User>();
            User rami = new User();
            rami.Username = "testname";
            rami.LastName = "test";
            rami.Email = "test@gmail.com";
            rami.IsAdmin = 0;
            rami.Password = "testpass123";
            rami.IsEnabled = 1;
            UmDAO.Create(rami);
            UmDAO.Delete(rami);
            User ret = UmDAO.Read(rami);
            Debug.Assert(ret.Email==null);
        }

    }
}