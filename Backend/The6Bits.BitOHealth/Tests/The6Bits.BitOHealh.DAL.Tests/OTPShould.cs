using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using The6Bits.BitOHealth.DAL.Contract;
using The6Bits.BitOHealth.DAL.Implementations;
using The6Bits.BitOHealth.Models;
using Xunit;

namespace The6Bits.BitOHealth.DAL.Tests;



public class OTPShould : TestsBase
{
    private IRepositoryAuth<string> Ac;
    
    
    //INITIALIZATION STUFF
    //TODO:ADD ACCOUNTS AT TEST START
    public OTPShould()
    {
        Ac = new AccountMsSqlDao(conn);
    }



    [Theory]
    [MemberData(nameof(LoadOTPJson))]
    public void DeletePastOTPTest(OTPModel o)
    {
        Ac.SaveActivationCode(o.username, o.time, o.code, o.codeType);
        Ac.DeletePastOTP(o.username,o.codeType);
        Assert.Equal("0",         Ac.ValidateOTP(o.username,o.code));

    }

    
    [Theory]
    [MemberData(nameof(LoadOTPJson))]
    public void ValidateOTPTest(OTPModel o)
    {
        Ac.SaveActivationCode(o.username, o.time, o.code, o.codeType);
        
        
        Assert.Equal("1",Ac.ValidateOTP(o.username, o.code));
        
        
        Ac.DeletePastOTP(o.username,o.codeType);

    }
    
    [Theory]
    [MemberData(nameof(LoadOTPJson))]
    public void SaveActivationCode(OTPModel o)
    {
        Ac.SaveActivationCode(o.username, DateTime.UtcNow, o.code, o.codeType);
        Assert.NotEqual("none",         Ac.ValidateOTP(o.username,o.code));


    }
    
    public static IEnumerable<object[]> LoadOTPJson()
    {
        
        DirectoryInfo di = new DirectoryInfo(Directory.GetCurrentDirectory());
        string p = di.Parent.Parent.Parent.Parent.ToString();
        string filePath = Path.GetFullPath(p + @"/TestData/OTPModelTestData.json");
        var json = File.ReadAllText(filePath);
        var people = JsonSerializer.Deserialize<List<OTPModel>>(json);
        var objectList = new List<object[]>();
        foreach (var data in people)
        {
            objectList.Add(new object[] {data});
        }
        return objectList;
        
    }

}