using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using The6Bits.BitOHealth.DAL.Implementations;
using The6Bits.BitOHealth.Models;
using Xunit;
using System.Text.Json;
using The6Bits.BitOHealth.DAL.Contract;
using The6Bits.BitOHealth.ManagerLayer;
using The6Bits.Authentication.Implementations;
using The6Bits.BitOHealth.ManagerLayer;
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
        
        
        
    }

    
    [Theory]
    [MemberData(nameof(LoadUsersJson))]
    public void UpdateIsEnabledTest(User u)
    {
        Ac.Create(u);
        Ac.UpdateIsEnabled(u.Username,0);
        Assert.Equal(0,Ac.Read(u).IsEnabled);

    }
    [Theory]
    [MemberData(nameof(LoadUsersJson))]
    public void AcceptEULA(User u)
    {
        // Arrange
        Ac.Delete(u);  
        Ac.Create(u);
        // Act
        var ans = Ac.AcceptEULA(u.Username);
        User readuser = Ac.Read(u);
        // Assert
        Assert.Equal(1, readuser.privOption);
    }

    [Theory]
    [MemberData(nameof(LoadUsersJson))]
    public void DeclineEULA(User u)
    {
        // Arrange
        Ac.Delete(u);
        Ac.Create(u);
        // Act
        var ans = Ac.DeclineEULA(u.Username);
        User readuser = Ac.Read(u);
        // Assert
        Assert.Equal(0, readuser.privOption);
    }




    //DELETE TESTING ITEMS FROM DB
    //TODO:DELETE ACCOUNTS AT TEST END

    [Theory]
    [InlineData("eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJ1c2VybmFtZSI6ImZpcnN0dXNlcjI5IiwiaWF0IjoiMTY0NTg4NTY1NCJ9.lDE7wMZHk3bAxj6dYd2V6fSl5OddworGmB6mw5zn5bw")]
    [InlineData("eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJ1c2VybmFtZSI6ImZpcnN0dXNlcjI5IiwiaWF0IjoiMTY0NTg4NDgyMCJ9.FJ1qz-IooxUXtesazX36FaVDqT-XImRdwpAqd81Pg5A")]


    public void DeleteAccountValid(string userName)
    {
        var AC = new AccountMsSqlDao(conn);
        var AS = new JWTAuthenticationService(conn);

        var result = AS.getUsername(userName);

        Assert.Equal("firstuser29", result);
    }



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