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

        public User Read()
        {
            throw new NotImplementedException();
        }

        public bool Create(User user)
        {
            try
            {
                _dataStore.Add(user);
                return true;
            }
            catch
            {
                return false;
            }
            
        }

        public bool Update(User user)
        {
            var loc = 0;
            foreach (User u in _dataStore)
            {
                if (u.Email == user.Email)
                {
                    _dataStore[loc] = user;
                    return true;
                }
            }
            return false;
        }

        public bool Delete(User user)
        {
            return true;
        }
        

    }
}