using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using The6Bits.BitOHealth.DAL.Implementations;
using The6Bits.BitOHealth.ServiceLayer;
using Xunit;

namespace The6Bits.BitOHealth.ServiceLayer.Test
{
    public class CreateRecordsTest
    {
        RecordsService _MHS;

        public CreateRecordsTest()
        {
            _MHS = new RecordsService(new RecordsMsSqlDAO("Server=localhost\\SQLEXPRESS;Database=master;Trusted_Connection=True;"));
            
        }

        [Fact]
        public void ValidFileSize()
        {
            //arrange
            string Filename = "Test.pdf";
            string username = "username";
            int fileSize = 111000000;
            //act
            string returnString = _MHS.ValidateFileSizeRecords(Filename, username, fileSize);
            //assert
            Assert.Equal("File size valid", returnString);
        }

        [Fact]
        public void InvalidFileSize()
        {
            //arrange
            string Filename = "Test.pdf";
            string username = "username";
            int fileSize = 1;
            //act
            string returnString = _MHS.ValidateFileSizeRecords(Filename, username, fileSize);
            //assert
            Assert.Equal("File size invalid", returnString);
        }

        [Fact]
        public void ValidFileType()
        {
            //arrange
            string Filename = "Test.pdf";
            string username = "username";
            string filePath = "C:\\Users\\Owner\\Documents\\";
            //act
            string returnString = _MHS.VerifyFileTypeRecords(Filename, username, filePath);
            //assert
            Assert.Equal("Valid file type", returnString);
        }

        [Fact]
        public void InvalidFileType()
        {
            //arrange
            string Filename = "Test.docx";
            string username = "username";
            string filePath = "C:\\Users\\Owner\\Documents\\";
            //act
            string returnString = _MHS.VerifyFileTypeRecords(Filename, username, filePath);
            //assert
            Assert.Equal("Invalid file type", returnString);

        }

        [Fact]
        public void ValidFileName()
        {
            //arrange
            string Filename = "TestFile1";
            string username = "username";
            string filePath = "C:\\Users\\Owner\\Documents\\";
            //act
            string returnString = _MHS.VerifyFileNameRecords(Filename, username, filePath);
            //assert
            Assert.Equal("new record name", returnString);
        }

        [Fact]
        public void InvalidFileName()
        {
            //arrange
            string Filename = "";
            string username = "username";
            string filePath = "C:\\Users\\Owner\\Documents\\";
            //act
            string returnString = _MHS.VerifyFileNameRecords(Filename, username, filePath);
            //assert
            Assert.Equal("Invalid filename", returnString);
        }

        [Fact]
        public void ValidSystemStorage()
        {
            //arrange
            string Filename = "Test.pdf";
            string username = "username";
            string filePath = "C:\\Users\\Owner\\Documents\\";
            //act
            string returnString = _MHS.VerifySystemStorageRecords(Filename, username, filePath);
            //assert
            Assert.Equal("Uploaded to datastore successfully", returnString);
        }

        [Fact]
        public void InvalidSystemStorage()
        {
            //arrange
            string Filename = "Test.pdf";
            string username = "username";
            string filePath = "C:\\Users\\Owner\\Documents\\";
            //act
            string returnString = _MHS.VerifySystemStorageRecords(Filename, username, filePath);
            //assert
            Assert.Equal("Uploaded to datastore failed", returnString);
        }

        [Fact]
        public void ValidCreateRecords()
        {
            //arrange
            string recordName = "TestRecords";
            string username = "username";
            //act
            string returnString = _MHS.CreateRecords(recordName, username);
            //assert
            Assert.Equal("Valid record name", returnString);

        }

        [Fact]
        public void InvalidCreateRecords()
        {
            //arrange
            string recordName = "";
            string username = "username";
            //act
            string returnString = _MHS.CreateRecords(recordName, username);
            //assert
            Assert.Equal("Invalid record name. Record name needs to be 1-100 chars long.", returnString);
        }

        [Fact]
        public void ValidUploadRecords()
        {
            //arrange
            string FileName = "Test.pdf";
            string username = "username";
            string filePath = "C:\\Users\\Owner\\Documents\\";
            //act
            string returnString = _MHS.UploadRecordsWinDao(FileName, username, filePath);
            //assert
            Assert.Equal("Uploaded successful to Win DAO", returnString);
        }

        [Fact]
        public void InvalidUploadRecords()
        {
            //arrange
            string FileName = "Test.docx";
            string username = "username";
            string filePath = "C:\\Users\\Owner\\Documents\\";
            //act
            string returnString = _MHS.UploadRecordsWinDao(FileName, username, filePath);
            //assert
            Assert.Equal("Uploaded failed to Win DAO", returnString);

        }

    }
}
