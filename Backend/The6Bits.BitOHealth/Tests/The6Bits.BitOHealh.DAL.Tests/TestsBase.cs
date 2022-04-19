using System;
using System.IO;
using FoodAPI;
using Microsoft.Extensions.Configuration;
using The6Bits.BitOHealth.Models;
using Xunit;



namespace The6Bits.BitOHealth.DAL.Tests;

public abstract class TestsBase : IDisposable
{
    public string conn { get; set; }
    public string keyPath { get; set; }
    public  openFDAConfig _openFDA {get; set;}

    public EdamamConfig edmamConfig {get; set;}

    protected TestsBase()
    {

        var configurationBuilder = new ConfigurationBuilder()
            .SetBasePath(System.IO.Path.Combine(AppContext.BaseDirectory, @"..\..\..\"))
            .AddJsonFile("appsettings.json");
        if (File.Exists(System.IO.Path.Combine(AppContext.BaseDirectory, @"..\..\..\") + "secrets.json"))
        {
            configurationBuilder.AddJsonFile("secrets.json");
        }

        var configuration = configurationBuilder.Build();

        conn = configuration.GetConnectionString("DefaultConnection");
         keyPath = configuration.GetSection("PKs")["JWT"];
        _openFDA = new openFDAConfig() { APIKey = "imFf95grrUBnCaPj2DA3MQQtpCpBmnPFiTtXfbD8"};





        if (File.Exists(System.IO.Path.Combine(AppContext.BaseDirectory, @"..\..\..\") + "secrets.json"))
        {
            edmamConfig = new EdamamConfig() { AppId = configuration.GetSection("Edamam")["ID"], AppKey = configuration.GetSection("Edamam")["KEY"] };
        }


        // Do "global" initialization here; Called before every test method.
    }

    public void Dispose()
    {
        // Do "global" teardown here; Called after every test method.
    }
}