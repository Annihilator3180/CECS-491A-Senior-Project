using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using The6Bits.Authentication.Implementations;
using The6Bits.BitOHealth.ControllerLayer;
using The6Bits.BitOHealth.DAL.Implementations;
using The6Bits.BitOHealth.Models;
using The6Bits.DBErrors;
using The6Bits.Logging.DAL.Implementations;
using Xunit;
namespace The6Bits.BitOHealth.Controller.Tests
{
    public class ReminderControllerShould : ControllerBase
    {

        [Fact]
        public async void StoreFoodLogShould()
        {

        }
    }
}
