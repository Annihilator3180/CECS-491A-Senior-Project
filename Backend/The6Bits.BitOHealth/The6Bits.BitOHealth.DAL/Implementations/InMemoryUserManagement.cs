using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Net.Security;
using Dapper;
using The6Bits.BitOHealth.Models;



namespace The6Bits.BitOHealth.DAL.Implementations
{
    public class InMemoryUserManagement<T> : IRepository<User>
    {
        private readonly IList<User> _dataStore;
        
        
        public InMemoryUserManagement() 
        {
            _dataStore = new List<User>();
        }


        //TODO:RENAME ERROR
        public string Create(User user)
        {
            try
            {
                _dataStore.Add(user);
                return "account created";
            }
            catch
            {
                return "error";
            }
            
        }

        public User Read(User user)
        {
            try
            {
                var loc = 0;
                foreach (User u in _dataStore)
                {
                    if (u.Username == user.Username)
                    {
                        return _dataStore[loc];
                    }
                }
                //USER NOT FOUND IN LIST RETURN 

                return new User();
            }
            catch
            {
                //error
                return new User();
            }
        }

        public bool Update(User user)
        {
            try
            {
                var loc = 0;
                foreach (User u in _dataStore)
                {
                    if (u.Username == user.Username)
                    {
                        _dataStore[loc] = user;
                        return true;
                    }
                }
                //USER NOT FOUND IN LIST
                return false;
            }
            catch
            {
                //error
                return false;
            }
        }

        public bool Delete(User user)
        {
            try
            {
                var loc = 0;
                foreach (User u in _dataStore)
                {
                    if (u.Username == user.Username)
                    {
                        _dataStore.Remove(u);
                        return true;
                    }
                }
                return false;
            }
            catch
            {
                return false;

            }


            //USER NOT FOUND IN LIST
        }
        

    }
}