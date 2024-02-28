using NUnit.Framework;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using BlogTracker.Models;
using Microsoft.AspNetCore.Routing;
using NuGet.ContentModel;
using BlogTracker.Controllers;
using Microsoft.EntityFrameworkCore;
using BlogTracker.Data;
[TestFixture]
public class DatabaseContextTests
{
    [Test]
    public void SeedData_AdminInfoSeeded_Success()
    {
        // Arrange
        var options = new DbContextOptionsBuilder<BlogTrackerdbContext>()
            .UseInMemoryDatabase(databaseName: "BlogTrackerdb")
            .Options;

        using (var context = new BlogTrackerdbContext(options))
        {
            // Act
            var adminInfo = context.AdminInfo.FirstOrDefault();

            // Assert
            Assert.IsNotNull(adminInfo);
            Assert.AreEqual("admin@gmail.com", adminInfo.EmailId);
            Assert.AreEqual("admin123", adminInfo.Password);
        }
    }
}
