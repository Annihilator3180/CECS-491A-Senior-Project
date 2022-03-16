using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using The6Bits.Authentication.Contract;
using The6Bits.BitOHealth.ManagerLayer;
using The6Bits.DBErrors;
using The6Bits.BitOHealth.DAL.Contract;

using The6Bits.Logging.Implementations;
using The6Bits.Logging.DAL.Contracts;

namespace The6Bits.BitOHealth.ControllerLayer
{
    [ApiController]
    [Route("HealthRecorder")]
    public class HealthRecorderController : ControllerBase
    {
        private IAuthenticationService _authentication;
        private LogService logService;
        private HealthRecorderManager _HRM;
        private IRepositoryHealthRecorderDAO _dao;

        public HealthRecorderController(IAuthenticationService authentication, ILogDal logDal, IDBErrors dBErrors, IRepositoryHealthRecorderDAO dao)
        {
            _authentication = authentication;
            logService = new LogService(logDal);
            _HRM = new HealthRecorderManager(dBErrors, dao);
            _dao = dao;
        }

        [HttpPost("CreateRecord")]

        public string CreateRecord()
        {

        }


    }
}
