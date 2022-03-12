using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using The6Bits.BitOHealth.DAL;
using The6Bits.BitOHealth.DAL.Tests;
using The6Bits.BitOHealth.ServiceLayer;
using The6Bits.DBErrors;
using The6Bits.BitOHealth.Models;
using Xunit;
namespace The6Bits.BitOHealth.ServiceLayer.Test
{
    public class MedicationServiceShould: TestsBase
    {
        MedicationService _MS;
        public MedicationServiceShould()
    {
        _MS = new MedicationService(new MsSqlMedicationDAO(conn), new OpenFDADAO());
        }
    [Fact]
    public void CheckDuplicatesTest()
        {
            DrugName drug1 = new DrugName("1", "1", "1");
            DrugName drug2 = new DrugName("2", "2", "2");
            DrugName drug3 = new DrugName("3", "3", "3");
            List<DrugName> testDrugs=new List<DrugName> { drug1, drug2 };
            List<DrugName> testDuplicates = new List<DrugName> { drug1, drug3 };
            List<DrugName> combinedDrugs = _MS.CheckDuplicates(testDrugs, testDuplicates);
            Assert.Equal(3, combinedDrugs.Count);
        }
    }
}
