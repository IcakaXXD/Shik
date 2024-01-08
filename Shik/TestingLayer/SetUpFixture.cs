using DataLayer;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;
using System;

namespace TestingLayer
{
    [SetUpFixture]
    public static class SetupFixture
    {
        public static ShikDBContext dbContext;
        [OneTimeSetUp]
        public static void OneTimeSetUp()
        {
            // TODO: Add code here that is run before
            //  all tests in the assembly are run
            DbContextOptionsBuilder builder = new DbContextOptionsBuilder();
            builder.UseInMemoryDatabase(Guid.NewGuid().ToString());
            dbContext = new ShikDBContext(builder.Options);
        }

        [OneTimeTearDown]
        public static void OneTimeTearDown()    
        {
            // TODO: Add code here that is run after
            //  all tests in the assembly have been run
            dbContext.Dispose();
        }

    }
}