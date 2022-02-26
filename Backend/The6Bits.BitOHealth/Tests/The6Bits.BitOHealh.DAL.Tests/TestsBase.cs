using System;
using System.IO;
using Microsoft.Extensions.Configuration;
using Xunit;


namespace The6Bits.BitOHealth.DAL.Tests;

public abstract class TestsBase : IDisposable
{
    public string conn { get; set; }

    protected TestsBase()
    {

        IConfigurationRoot configuration = new ConfigurationBuilder()
            .SetBasePath(System.IO.Path.Combine(AppContext.BaseDirectory,@"..\..\..\"))
            .AddJsonFile("appsettings.json")
            .Build();
         conn = configuration.GetConnectionString("DefaultConnection");


        // Do "global" initialization here; Called before every test method.
    }

    public void Dispose()
    {
        // Do "global" teardown here; Called after every test method.
    }
}