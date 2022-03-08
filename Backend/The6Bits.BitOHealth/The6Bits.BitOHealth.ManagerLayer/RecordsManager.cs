using System;
using System.ComponentModel.Design;
using The6Bits.BitOHealth.DAL;
using The6Bits.BitOHealth.DAL.Implementations;
using The6Bits.BitOHealth.ServiceLayer;
using The6Bits.BitOHealth.Models;
using The6Bits.Logging;
using The6Bits.Logging.Implementations;
using The6Bits.Authorization.Contract;

namespace The6Bits.BitOHealth.ManagerLayer
{
    public class RecordsManager
    {
        public IAuthorizationService auth;
        public string token;
        private RecordsService _MHS;
    }

    public MHManager(IRepositoryUM<User> daoType)
    {
        _MHS = new RecordsService(daoType);
    }

    public string ValidateFileSizeRe3cords(User user)
    {
        string fileName;
        string username;
        int fileSize;

        if(fileSize < 0.5 MB)
        {
            return fileName + "File size too small. Try again";
        }
        if(fileSize > 12 MB){
            return fileName + "File size too large. Try again";
        }
        
    }
}
