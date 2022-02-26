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
        _connect = "Server=localhost\\SQLEXPRESS;Database=master;Trusted_Connection=True;";

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
    [InlineData("Test1","CodeTest","Test")]
    public void SaveActivationCodeInsert(string username, string code, string codeType)
    {
        //arrange
        DateTime time=DateTime.Now;
        //act
        Ac.SaveActivationCode(username,time, code, codeType);
        String codeResult=Ac.getCode(username, codeType); 
        //Assert
        Assert.Equal(code, codeResult);
        //cleanup
        Ac.DeleteCode("Test1", "Test");
        
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
    public void UnactivatedSaveTest(User u)
    {
        //arrange
        Ac.Delete(u);
        //act
        String userExist = Ac.UnactivatedSave(u);
        User result=Ac.Read(u);
        //Assert
        Assert.Equal(u.Username, result.Username);
        Assert.Equal(0, result.IsEnabled);
        //Cleanup
        Ac.Delete(u);
    }
    [Theory]
    [MemberData(nameof(LoadUsersJson))]
    public void UsernameExistsTest(User u)
    {
        //arrange
        Ac.Delete(u);
        Ac.Create(u);
        //act
        String userExist= Ac.UsernameExists(u.Username);
        //Assert
        Assert.Equal("username exists", userExist);
        //Cleanup
        Ac.Delete(u);
    }

    [Theory]
    [MemberData(nameof(LoadUsersJson))]
    public void UsernameDoesntExistsTest(User u)
    {
        //arrange
        Ac.Delete(u);
        //act
        String userExist = Ac.UsernameExists(u.Username);
        //Assert
        Assert.Equal("username not found", userExist);
        //Cleanup
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
        
        //CLEANUP
        


    }




//DELETE TESTING ITEMS FROM DB
    //TODO:DELETE ACCOUNTS AT TEST END

    
    //use when User type needs to be used
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