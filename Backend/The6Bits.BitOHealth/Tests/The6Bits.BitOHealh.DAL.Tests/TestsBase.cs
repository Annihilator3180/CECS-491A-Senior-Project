using System;
using Xunit;


namespace The6Bits.BitOHealth.DAL.Tests;

public abstract class TestsBase : IDisposable
{
    protected TestsBase()
    {
        // Do "global" initialization here; Called before every test method.
    }

    public void Dispose()
    {
        // Do "global" teardown here; Called after every test method.
    }
}