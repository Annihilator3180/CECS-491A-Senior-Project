using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Internal;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using The6Bits.BitOHealth.DAL.Contract;
using The6Bits.BitOHealth.DAL.Implementations;
using The6Bits.BitOHealth.Models;
using The6Bits.BitOHealth.ServiceLayer;
using The6Bits.DBErrors;
using Xunit;

namespace The6Bits.BitOHealth.DAL.Tests 
{
    public class HealthRecorderMsSqlDAOShould : TestsBase
    {
        private IRepositoryHealthRecorderDAO _HealthRecorderMsSqlDAO;
        private HealthRecorderService _HealthRecorderService;
        private IDBErrors _dbErrors;

        public HealthRecorderMsSqlDAOShould()
        {
            _HealthRecorderMsSqlDAO = new HealthRecorderMsSqlDAO(conn);
            _dbErrors = new MsSqlDerrorService();
            _HealthRecorderService = new HealthRecorderService(_HealthRecorderMsSqlDAO, _dbErrors);
        }
        [Fact]
        public void SaveRecord()
        {
            string expected = "saved";

            string actual = _HealthRecorderMsSqlDAO.SaveRecord("", "bossadmin12", "test3", "test3", "");

            Assert.Equal(expected, actual);

            HealthRecorderRequestModel request = new HealthRecorderRequestModel("test3", "test3");
            HealthRecorderResponseModel response = new HealthRecorderResponseModel();
            _HealthRecorderMsSqlDAO.DeleteRecord(request, response, "bossadmin12");


        }
        [Fact]
        public void ValidateUserRecordName()
        {
            string expected = "valid request";
            _HealthRecorderMsSqlDAO.SaveRecord("", "bossadmin12", "tester", "tester", "");

            string actual = _HealthRecorderMsSqlDAO.ValidateUserRecordRequest("bossadmin12", "fakerecordname");

            Assert.Equal(expected, actual);

            HealthRecorderRequestModel request = new HealthRecorderRequestModel("tester", "tester");
            HealthRecorderResponseModel response = new HealthRecorderResponseModel();
            _HealthRecorderMsSqlDAO.DeleteRecord(request, response, "bossadmin12");
        }
        [Fact]
        public void ValidateRecordExists()
        {
            string expected = "1";
            _HealthRecorderMsSqlDAO.SaveRecord("", "bossadmin12", "tester2", "tester2", "");

            HealthRecorderRequestModel request = new HealthRecorderRequestModel("tester2", "tester2");
            HealthRecorderResponseModel response = new HealthRecorderResponseModel();
            response = _HealthRecorderMsSqlDAO.ValidateRecordExists(request, response, "bossadmin12");
            string actual = response.Data;

            Assert.Equal(expected, actual);

            _HealthRecorderMsSqlDAO.DeleteRecord(request, response, "bossadmin12");

        }
        [Fact]
        public void ExportRecord()
        {
            string expected = "";
            _HealthRecorderMsSqlDAO.SaveRecord("", "bossadmin12", "tester3", "tester3", "");

            HealthRecorderRequestModel request = new HealthRecorderRequestModel("tester3", "tester3", "1");
            HealthRecorderExportModel response = new HealthRecorderExportModel();
            response = _HealthRecorderMsSqlDAO.ExportRecord(request, "bossadmin12", response);
            string actual = response.File;

            Assert.Equal(expected, actual);

            HealthRecorderRequestModel request2 = new HealthRecorderRequestModel("tester3", "tester3");
            HealthRecorderResponseModel response2 = new HealthRecorderResponseModel();
            _HealthRecorderMsSqlDAO.DeleteRecord(request2, response2, "bossadmin12");
        }
    }
}
