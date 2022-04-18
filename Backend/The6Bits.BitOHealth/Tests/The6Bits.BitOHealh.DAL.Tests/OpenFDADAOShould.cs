using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using System.Threading.Tasks;
using The6Bits.BitOHealth.DAL.Contract;
using The6Bits.BitOHealth.DAL.Implementations;
using The6Bits.BitOHealth.Models;
using System.Net.Http;
using Xunit;

namespace The6Bits.BitOHealth.DAL.Tests;



public class OpenFDADAOShould : TestsBase
{
    private IDrugDataSet _MedicationDao;

    public OpenFDADAOShould()
    {
        _MedicationDao = new OpenFDADAO(new HttpClient()
        {
            BaseAddress = new Uri("https://api.fda.gov/drug/")
        }, _openFDA);
    }
    [Fact]
    public async void ValidGenericNameTest()
    {
        //arrange
        string caffieneTest = "caffeine";
        //act
        List<DrugName> testdrugNames = await _MedicationDao.GetGenericDrugName(caffieneTest);
        string caffieneResponse = testdrugNames[0].generic_name.ToLower();
        //assert
        Assert.Contains(caffieneTest, caffieneResponse);
    }
    [Fact]
    public async void ValidBrandNameTest()
    {
        //arrange
        string adderallTest = "adderall";
        //act
        List<DrugName> testdrugNames = await _MedicationDao.GetBrandDrugName(adderallTest);
        string adderallResponse = testdrugNames[0].brand_name.ToLower();
        //assert
        Assert.Contains(adderallTest, adderallResponse);
    }
    [Fact]
    public async void InvalidBrandNameTest()
    {
        //arrange
        string badName = "asfdsaasdgfaswdgasfd";
        //act
        List<DrugName> testdrugNames = await _MedicationDao.GetBrandDrugName(badName);
        string badNameResponse = testdrugNames[0].brand_name;
        //assert
        Assert.Equal("", badNameResponse);
    }
    [Fact]
    public async void InvalidGenericNameTest()
    {
        //arrange
        string badName = "asfdsaasdgfaswdgasfd";
        //act
        List<DrugName> testdrugNames = await _MedicationDao.GetGenericDrugName(badName);
        string badNameResponse = testdrugNames[0].generic_name;
        //assert
        Assert.Contains("", badNameResponse);
    }

    [Fact]
    public async void GetDrugInfo()
    {
        //arrange
        string caffeineTest = "caffeine";
        //act
        drugInfo testdrugNames = await _MedicationDao.GetDrugInfo(caffeineTest);
        
        //assert
        Assert.Contains(caffeineTest, testdrugNames.openfda.generic_name[0].ToLower());
    }
}

