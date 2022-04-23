using Microsoft.AspNetCore.Http.Internal;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using The6Bits.BitOHealth.DAL.Implementations;
using The6Bits.BitOHealth.Models.WeightManagement;
using Xunit;

namespace The6Bits.BitOHealth.DAL.Tests
{
    public class WeightManagementWindowsDaoShould : TestsBase
    {
        private WeightManagementWindowsDao _dao;

        public WeightManagementWindowsDaoShould()
        {
            _dao = new WeightManagementWindowsDao("%USERPROFILE%\\Pictures\\TestImages\\");
        }

        [Theory]
        [InlineData("dummy.txt")]
        [InlineData("dasjkdhaskdasm.txt")]
        public async void SaveImageShould(string filename)
        {
            var bytes = Encoding.UTF8.GetBytes("This is a dummy file");
            IFormFile file = new FormFile(new MemoryStream(bytes), 0, bytes.Length, "Data", filename);
            IWeightManagerResponse res = await _dao.SaveImage(file, "testuser");




            Assert.True(System.IO.File.Exists((string)res.Result));




            await _dao.DeleteImage(((string) res.Result), "testuser");
        }




        [Theory]
        [InlineData("dummy.txt")]
        [InlineData("dasjkdhaskdasm.txt")]
        public async void DeleteImageShould(string filename)
        {
            var bytes = Encoding.UTF8.GetBytes("This is a dummy file");
            IFormFile file = new FormFile(new MemoryStream(bytes), 0, bytes.Length, "Data", filename);
            IWeightManagerResponse res = await _dao.SaveImage(file, "testuser");
            await _dao.DeleteImage(((string)res.Result), "testuser");



            Assert.False(System.IO.File.Exists((string)res.Result));

        }


    }
}
