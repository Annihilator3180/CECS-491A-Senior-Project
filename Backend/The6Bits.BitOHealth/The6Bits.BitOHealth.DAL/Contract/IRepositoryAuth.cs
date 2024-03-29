﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using The6Bits.BitOHealth.Models;

namespace The6Bits.BitOHealth.DAL.Contract
{
    public interface IRepositoryAuth<T>
    {

        string UsernameExists(string username);
        string UserRole(string username);
        string CheckPassword(string username, string password);
        //string UsernameAndEmailExists(string username, string email);
        //public string IsEnabled(string username);
        //public string ValidateRecoveryAttempts(string username);

        public string UpdateRecoveryAttempts(string username, DateTime dT);





        public bool Create(User user);
        public User Read(User user);
        public bool Delete(User user);

        public string DeletePastOTP(string username, string codetype);

        public string SaveActivationCode(string username, DateTime time, string code, string codeType);

        public string ValidateOTP(string username, string code);

        public string CheckFailedAttempts(string username);

        public string InsertFailedAttempts(string username);
        public string UpdateFailedAttempts(string username, int updatedValue);

        public string CheckFailDate(string username);

        public string UpdateIsEnabled(string username, int updateValue);

        public string DeleteFailedAttempts(string username);
        public string AcceptEULA(string username);
        public string DeclineEULA(string username);
        public string UnactivatedSave(User user);
        public string DeleteUnActivated(User user);
        public string getCode(string username, string codeType);
        public String DeleteCode(string username, string codeType);
        public string GetTime(string code, string username);
        public string VerifySameDay(string username, string code);


        public string VerifyTwoMins(string username, string code);
        public string ResetPassword(string password, string username);
        public string ValidateRecoveryAttempts(string username);
        public string UsernameAndEmailExists(string username, string email);
        public string GetPassword(string username);
        public string GetRecoveryOTP(string username);

        public string RemoveRecoveryAttempts(string username);

        public string DeleteAccount(string username);
        public string ActivateUser(string username);
        public int ViewExists(string view);
        public bool AddTime(string view, float time);
        public bool MakeView(string view, float time);
        public Task<List<timeTotal>> BiggestTime();
        public Task<List<timeTotal>> AvgTime();
        public List<Tracking> GetLogin(string Type, int months);
        public List<Tracking> GetReg();
        public string MakeNewUser(string permUsername, string oldUsername);
        public List<searchItem> getSearchCount(string type);
    }
}
