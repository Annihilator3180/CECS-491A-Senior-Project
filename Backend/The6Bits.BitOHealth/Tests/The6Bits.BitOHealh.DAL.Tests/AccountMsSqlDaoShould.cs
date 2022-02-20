using System;
using The6Bits.BitOHealth.DAL.Implementations;
using Xunit;
//using Microsoft.Extensions.Configuration;

namespace The6Bits.BitOHealth.DAL.Tests;



public class AccountMsSqlDaoShould : IDisposable
{
    private string _connect;
    //INITIALIZATION STUFF
    //TODO:ADD ACCOUNTS AT TEST START
    public AccountMsSqlDaoShould ()
    {
         _connect = "Server=localhost\\SQLEXPRESS;Database=master;Trusted_Connection=True;";

    }
    


    [Theory]
    [InlineData("Short@1","SAS")]
    [InlineData("LONGGAACCCA11","Passwordka!!1")]
    public void CheckPasswordInvalid(string username, string password)
    {
        //TEMP CONNSTRING
        
        
        
        AccountMsSqlDao Ac = new AccountMsSqlDao(_connect);
        var ans = Ac.CheckPassword(username, password);
        Assert.Equal("not found",ans);
        
    }
    
    [Theory]
    [InlineData("bossadmin12","Password!1")]
    [InlineData("raoaa1!eq","boofbabA!1")]
    public void CheckPasswordValid(string username, string password)
    {
        //TEMP CONNSTRING
        
        
        AccountMsSqlDao Ac = new AccountMsSqlDao(_connect);
        var ans = Ac.CheckPassword(username, password);
        Assert.Equal("credentials found",ans);
        
    }


    [Theory]
    [InlineData("bossadmin12")]
    [InlineData("raoaa1!eq")]
    public void UsernameExistsValid(string username)
    {
        AccountMsSqlDao Ac = new AccountMsSqlDao(_connect);
        var ans = Ac.UsernameExists(username);
        Assert.Equal("username exists",ans);
    }

    [Theory]
    [InlineData("zz")]
    [InlineData("dasdsadassddasdsa")]
    public void UsernameExistsInvalid(string username)
    {
        AccountMsSqlDao Ac = new AccountMsSqlDao(_connect);
        var ans = Ac.UsernameExists(username);
        Assert.Equal("username not found",ans);
        
    }




    //DELETE TESTING ITEMS FROM DB
    //TODO:DELETE ACCOUNTS AT TEST END

    public void Dispose()
    {
        
    }


}