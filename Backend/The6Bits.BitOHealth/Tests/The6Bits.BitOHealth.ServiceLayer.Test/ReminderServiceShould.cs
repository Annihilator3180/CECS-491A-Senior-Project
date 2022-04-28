using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using The6Bits.BitOHealth.DAL.Contract;
using The6Bits.BitOHealth.DAL.Implementations;
using The6Bits.BitOHealth.DAL.Tests;
using The6Bits.BitOHealth.Models;
using Xunit;



namespace The6Bits.BitOHealth.ServiceLayer.Test
{
    public class ReminderServiceShould : TestsBase
    {
        private ReminderService _reminderService;
        private IReminderDatabase _dao;

        public ReminderServiceShould()
        {
            _dao = new ReminderMsSqlDao(conn);
            _reminderService = new ReminderService(_dao);
        }

    }
}
