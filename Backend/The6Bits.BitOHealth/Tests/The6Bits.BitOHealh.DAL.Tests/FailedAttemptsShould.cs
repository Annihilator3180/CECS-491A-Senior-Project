using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using The6Bits.BitOHealth.DAL.Contract;
using The6Bits.BitOHealth.DAL.Implementations;
using The6Bits.BitOHealth.Models;
using Xunit;

namespace The6Bits.BitOHealth.DAL.Tests;

public class FailedAttemptsShould : TestsBase
{
    private IRepositoryAuth<string> Ac;
    
    
    //INITIALIZATION STUFF
    //TODO:ADD ACCOUNTS AT TEST START
    public FailedAttemptsShould()
    {
        Ac = new AccountMsSqlDao(conn);
    }



    [Theory]
    [MemberData(nameof(FailedAttempts))]
    public void DeleteFailedAttemptsTest(FailedAttemptsModel f)
    {
        Ac.InsertFailedAttempts(f.Username);
        Assert.Equal("1",Ac.CheckFailedAttempts(f.Username));
        Ac.DeleteFailedAttempts(f.Username);

    }
    
    
    [Theory]
    [MemberData(nameof(FailedAttempts))]
    public void CheckFailDateTest(FailedAttemptsModel f)
    {
        Ac.InsertFailedAttempts(f.Username);
        
        Assert.NotEqual("none",Ac.CheckFailDate(f.Username));
        
        Ac.DeleteFailedAttempts(f.Username);


    }
    
    [Theory]
    [MemberData(nameof(FailedAttempts))]
    public void CheckFailedAttemptsTest(FailedAttemptsModel f)
    {
        Ac.InsertFailedAttempts(f.Username);
        Assert.Equal("1",Ac.CheckFailedAttempts(f.Username));
        Ac.DeleteFailedAttempts(f.Username);

        
    }
    
    [Theory]
    [MemberData(nameof(FailedAttempts))]
    public void InsertFailedAttemptsTest(FailedAttemptsModel f)
    {
        
        Ac.InsertFailedAttempts(f.Username);
        Assert.Equal("1",Ac.CheckFailedAttempts(f.Username));
        Ac.DeleteFailedAttempts(f.Username);
        
    }
    
    [Theory]
    [MemberData(nameof(FailedAttempts))]
    public void UpdateFailedAttemptsTest(FailedAttemptsModel f)
    {
        Ac.InsertFailedAttempts(f.Username);
        Ac.UpdateFailedAttempts(f.Username,3);
        Assert.Equal("3",Ac.CheckFailedAttempts(f.Username));
        Ac.DeleteFailedAttempts(f.Username);
    }
    
    
    public static IEnumerable<object[]> FailedAttempts()
    {
        
        DirectoryInfo di = new DirectoryInfo(Directory.GetCurrentDirectory());
        string p = di.Parent.Parent.Parent.Parent.ToString();
        string filePath = Path.GetFullPath(p + @"/TestData/FailedAttemptsData.json");
        var json = File.ReadAllText(filePath);
        var people = JsonSerializer.Deserialize<List<FailedAttemptsModel>>(json);
        var objectList = new List<object[]>();
        foreach (var data in people)
        {
            objectList.Add(new object[] {data});
        }
        return objectList;
        
    }
    
}