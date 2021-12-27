using System;
using Core.Database;
using Microsoft.EntityFrameworkCore;

namespace Core.UnitTests.Utils;

public class DbContextFactory
{
    public static AreawaDbContext CreateInMemory()
    {
        var builder = new DbContextOptionsBuilder<AreawaDbContext>().UseInMemoryDatabase("InMemoryDb" + Guid.NewGuid());
        return new AreawaDbContext(builder.Options);
    }
}