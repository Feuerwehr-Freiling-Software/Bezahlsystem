using Xunit;
using Moq;
using OAOPS.Shared.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
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
using Microsoft.AspNetCore.Routing;
using OAOPS.Shared.Interfaces;

namespace OAOPS.Tests
{
    public class UserServiceTests
    {
        private readonly Mock<ApplicationDbContext> _mockDbContext;
        private readonly Mock<UserManager<ApplicationUser>> _mockUserManager;
        private readonly Mock<ILogger<UserService>> _mockLogger;
        private readonly Mock<RoleManager<IdentityRole>> _mockRoleManager;
        private readonly Mock<DbSet<ApplicationUser>> _mockUserSet;

        public UserServiceTests()
        {
            _mockDbContext = new Mock<ApplicationDbContext>();
            _mockUserManager = new Mock<UserManager<ApplicationUser>>();
            _mockLogger = new Mock<ILogger<UserService>>();
            _mockRoleManager = new Mock<RoleManager<IdentityRole>>();
            _mockUserSet = new Mock<DbSet<ApplicationUser>>();
        }

        [Fact]
        public async Task GetAllUsers_ReturnsListOfUserDto()
        {
            // Arrange
            var userService = new UserService(_mockDbContext.Object, _mockUserManager.Object, _mockLogger.Object, _mockRoleManager.Object);
            var users = new List<ApplicationUser>
            {
                new ApplicationUser { Balance = 100, Comment = "Test", FirstName = "John", LastName = "Doe", UserName = "johndoe", IsConfirmedUser = true },
                new ApplicationUser { Balance = 200, Comment = "Test2", FirstName = "Jane", LastName = "Doe", UserName = "janedoe", IsConfirmedUser = false }
            };
            _mockUserSet.As<IQueryable<ApplicationUser>>().Setup(m => m.Provider).Returns(users.AsQueryable().Provider);
            _mockUserSet.As<IQueryable<ApplicationUser>>().Setup(m => m.Expression).Returns(users.AsQueryable().Expression);
            _mockUserSet.As<IQueryable<ApplicationUser>>().Setup(m => m.ElementType).Returns(users.AsQueryable().ElementType);
            _mockUserSet.As<IQueryable<ApplicationUser>>().Setup(m => m.GetEnumerator()).Returns(users.GetEnumerator());

            // Act
            var result = await userService.GetAllUsers();

            // Assert
            Assert.NotNull(result);
            Assert.Equal(2, result.Count);
            Assert.Equal("johndoe", result[0].Username);
            Assert.Equal("janedoe", result[1].Username);
        }

        [Fact]
        public async Task GetUserBalance_ReturnsExpectedBalance_WhenUserExists()
        {
            var userService = new UserService(_mockDbContext.Object, _mockUserManager.Object, _mockLogger.Object, _mockRoleManager.Object);

            // Arrange
            var username = "testUser";
            var expectedBalance = 100.0;
            var users = new ApplicationUser[]
            {
                new ApplicationUser { NormalizedUserName = username.ToUpper(), Balance = expectedBalance }
            }.AsQueryable();

            _mockUserSet.As<IQueryable<ApplicationUser>>().Setup(m => m.Provider).Returns(users.Provider);
            _mockUserSet.As<IQueryable<ApplicationUser>>().Setup(m => m.Expression).Returns(users.Expression);
            _mockUserSet.As<IQueryable<ApplicationUser>>().Setup(m => m.ElementType).Returns(users.ElementType);
            _mockUserSet.As<IQueryable<ApplicationUser>>().Setup(m => m.GetEnumerator()).Returns(users.GetEnumerator());

            // Act
            var result = await userService.GetUserBalance(username);

            // Assert
            Assert.Equal(expectedBalance, result);
        }

        [Fact]
        public async Task GetUserBalance_ReturnsZero_WhenUserDoesNotExist()
        {
            var userService = new UserService(_mockDbContext.Object, _mockUserManager.Object, _mockLogger.Object, _mockRoleManager.Object);

            // Arrange
            var username = "nonExistentUser";
            var users = new ApplicationUser[0].AsQueryable();

            _mockUserSet.As<IQueryable<ApplicationUser>>().Setup(m => m.Provider).Returns(users.Provider);
            _mockUserSet.As<IQueryable<ApplicationUser>>().Setup(m => m.Expression).Returns(users.Expression);
            _mockUserSet.As<IQueryable<ApplicationUser>>().Setup(m => m.ElementType).Returns(users.ElementType);
            _mockUserSet.As<IQueryable<ApplicationUser>>().Setup(m => m.GetEnumerator()).Returns(users.GetEnumerator());

            // Act
            var result = await userService.GetUserBalance(username);

            // Assert
            Assert.Equal(0.0, result);
        }
    }
}
