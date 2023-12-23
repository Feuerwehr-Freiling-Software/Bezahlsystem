using Bunit;
using Microsoft.Extensions.Logging;
using OAOPS.Client.Pages;
using Xunit;
using Moq;
using OAOPS.Shared.Services;
using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;
using System.Threading.Tasks;
using OAOPS.Client.DTO;
using System.Linq;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using OAOPS.Shared.DTO;
using System;
using System.Text;
using OAOPS.Shared.Models;
using OAOPS.Shared.Data;

namespace TestProject
{
    public class UnitTest1 : TestContext
    {
        private readonly Mock<ApplicationDbContext> _mockDbContext;
        private readonly Mock<UserManager<ApplicationUser>> _mockUserManager;
        private readonly Mock<ILogger<UserService>> _mockLogger;
        private readonly Mock<RoleManager<IdentityRole>> _mockRoleManager;
        private readonly Mock<DbSet<ApplicationUser>> _mockUserSet;

        public UnitTest1()
        {
            _mockDbContext = new Mock<ApplicationDbContext>();
            _mockUserManager = new Mock<UserManager<ApplicationUser>>();
            _mockLogger = new Mock<ILogger<UserService>>();
            _mockRoleManager = new Mock<RoleManager<IdentityRole>>();
            _mockUserSet = new Mock<DbSet<ApplicationUser>>();
        }

        [Fact]
        public void CounterShouldIncrementWhenClicked()
        {
            var cut = RenderComponent<Counter>();
            
            cut.Find("button").Click();

            cut.Find("p").MarkupMatches("<p role=\"status\">Current count: 1</p>");

            cut.Find("button").Click();

            cut.Find("p").MarkupMatches("<p role=\"status\">Current count: 2</p>");
        }

        [Fact]
        public void CounterShouldIncrementByValueWhenClicked()
        {
            int value = 2;
            var cut = RenderComponent<Counter>(parameters => parameters.Add(p => p.Value, value));

            cut.Find("button").Click();

            cut.Find("p").MarkupMatches($"<p role=\"status\">Current count: {value}</p>");
        }

    }
}