using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using The6Bits.BitOHealth.DAL.Implementations;
using The6Bits.BitOHealth.Models;
using Xunit;
using System.Text.Json;
using The6Bits.BitOHealth.DAL.Contract;

//using Microsoft.Extensions.Configuration;

namespace The6Bits.BitOHealth.DAL.Tests;



public class AccountMsSqlDaoShould : TestsBase
{
    private IRepositoryAuth<string> Ac;


    //INITIALIZATION STUFF
    //TODO:ADD ACCOUNTS AT TEST START
    public AccountMsSqlDaoShould()
    {
        Ac = new AccountMsSqlDao(conn);
    }



    [Theory]
    [InlineData("Short@1", "SAS")]
    [InlineData("LONGGAACCCA11", "Passwordka!!1")]
    public void CheckPasswordInvalid(string username, string password)
    {


        var ans = Ac.CheckPassword(username, password);
        Assert.Equal("not found", ans);

    }

    [Theory]
    [MemberData(nameof(LoadUsersJson))]

    public void CheckPasswordValid(User u)
    {
        //TEMP CONNSTRING
        Ac.Create(u);

        var ans = Ac.CheckPassword(u.Username, u.Password);
        Assert.Equal("credentials found", ans);


        Ac.Delete(u);

    }


    [Theory]
    [MemberData(nameof(LoadUsersJson))]
    public void UsernameExistsValid(User u)
    {
        Ac.Create(u);
        var ans = Ac.UsernameExists(u.Username);
        Assert.Equal("username exists", ans);


        Ac.Delete(u);


    }

    [Theory]
    [InlineData("zz")]
    [InlineData("dasdsadassddasdsa")]
    public void UsernameExistsInvalid(string username)
    {
        var ans = Ac.UsernameExists(username);
        Assert.Equal("username not found", ans);

    }
    
    [Theory]
    [MemberData(nameof(LoadUsersJson))]
    public void CreateValid(User u)
    {
        
        
        Ac.Create(u);
        User readuser = Ac.Read(u);
        
        
        Assert.Equal(u.Username,readuser.Username);

        Ac.Delete(u);

    }

    
    [Theory]
    [MemberData(nameof(LoadUsersJson))]
    public void ReadTest(User u)
    {
        Ac.Create(u);

        Assert.Equal(u.Username,Ac.Read(u).Username);

        
        Ac.Delete(u);

    }
    
    
    
    [Theory]
    [MemberData(nameof(LoadUsersJson))]
    public void DeleteTest(User u)
    {
        Ac.Create(u);
        Ac.Delete(u);
        Ac.Read(u);
        
        Assert.NotEqual(u.Username,Ac.Read(u).Username);
        
        
        
    }

    
    [Theory]
    [MemberData(nameof(LoadUsersJson))]
    public void UpdateIsEnabledTest(User u)
    {
        Ac.Create(u);
        Ac.UpdateIsEnabled(u.Username,0);
        Assert.Equal(0,Ac.Read(u).IsEnabled);

    }










//DELETE TESTING ITEMS FROM DB
    //TODO:DELETE ACCOUNTS AT TEST END

    
    
    public static IEnumerable<object[]> LoadUsersJson()
    {
        
        DirectoryInfo di = new DirectoryInfo(Directory.GetCurrentDirectory());
        string p = di.Parent.Parent.Parent.Parent.ToString();
        string filePath = Path.GetFullPath(p + @"/TestData/AccountTestData.json");
        var json = File.ReadAllText(filePath);
        var people = JsonSerializer.Deserialize<List<User>>(json);
        var objectList = new List<object[]>();
        foreach (var data in people)
        {
            objectList.Add(new object[] {data});
        }
        return objectList;
        
    }


}