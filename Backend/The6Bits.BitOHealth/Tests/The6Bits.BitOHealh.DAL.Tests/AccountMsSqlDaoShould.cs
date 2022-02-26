using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using The6Bits.BitOHealth.DAL.Implementations;
using The6Bits.BitOHealth.Models;
using Xunit;
using System.Text.Json;
//using Microsoft.Extensions.Configuration;

namespace The6Bits.BitOHealth.DAL.Tests;



public class AccountMsSqlDaoShould : TestsBase
{
    private string _connect;

    //INITIALIZATION STUFF
    //TODO:ADD ACCOUNTS AT TEST START
    public AccountMsSqlDaoShould()
    {
        _connect = "Server=localhost\\SQLEXPRESS;Database=Master;Trusted_Connection=True;";

    }



    [Theory]
    [InlineData("Short@1", "SAS")]
    [InlineData("LONGGAACCCA11", "Passwordka!!1")]
    public void CheckPasswordInvalid(string username, string password)
    {
        //TEMP CONNSTRING



        AccountMsSqlDao Ac = new AccountMsSqlDao(_connect);
        var ans = Ac.CheckPassword(username, password);
        Assert.Equal("not found", ans);

    }

    [Theory]
    [InlineData("bossadmin12", "Password!1")]
    [InlineData("raoaa1!eq", "boofbabA!1")]
    public void CheckPasswordValid(string username, string password)
    {
        //TEMP CONNSTRING


        AccountMsSqlDao Ac = new AccountMsSqlDao(_connect);
        var ans = Ac.CheckPassword(username, password);
        Assert.Equal("credentials found", ans);

    }


    [Theory]
    [InlineData("bossadmin12")]
    [InlineData("raoaa1!eq")]
    public void UsernameExistsValid(string username)
    {
        AccountMsSqlDao Ac = new AccountMsSqlDao(_connect);
        var ans = Ac.UsernameExists(username);
        Assert.Equal("username exists", ans);
    }

    [Theory]
    [InlineData("zz")]
    [InlineData("dasdsadassddasdsa")]
    public void UsernameExistsInvalid(string username)
    {
        AccountMsSqlDao Ac = new AccountMsSqlDao(_connect);
        var ans = Ac.UsernameExists(username);
        Assert.Equal("username not found", ans);

    }
    [Theory]
    [MemberData(nameof(LoadUsersJson))]
    public void CreateValid(User u)
    {
        AccountMsSqlDao Ac = new AccountMsSqlDao(_connect);
        
        
        Ac.Create(u);
        User readuser = Ac.Read(u);
        
        
        Assert.Equal(u.Username,readuser.Username);
        
        //CLEANUP
        


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