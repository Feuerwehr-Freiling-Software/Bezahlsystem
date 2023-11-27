using Xunit;
using Moq;
using OAOPS.Shared.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Threading.Tasks;
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
        private readonly Mock<DbSet<ApplicationUser>> _mockUserSet;
        private readonly Mock<IUserService> _mockUserService;

        public UserServiceTests()
        {
            _mockUserSet = new Mock<DbSet<ApplicationUser>>();
            _mockUserService = new Mock<IUserService>();
        }


        [Fact]
        public async Task GetAllUsers_ReturnsListOfUserDto()
        {
            // Arrange
            var users = new List<ApplicationUser>
            {
                new() { Balance = 100, Comment = "Test", FirstName = "John", LastName = "Doe", UserName = "johndoe", IsConfirmedUser = true },
                new() { Balance = 200, Comment = "Test2", FirstName = "Jane", LastName = "Doe", UserName = "janedoe", IsConfirmedUser = false }
            };
            _mockUserSet.As<IQueryable<ApplicationUser>>().Setup(m => m.Provider).Returns(users.AsQueryable().Provider);
            _mockUserSet.As<IQueryable<ApplicationUser>>().Setup(m => m.Expression).Returns(users.AsQueryable().Expression);
            _mockUserSet.As<IQueryable<ApplicationUser>>().Setup(m => m.ElementType).Returns(users.AsQueryable().ElementType);
            _mockUserSet.As<IQueryable<ApplicationUser>>().Setup(m => m.GetEnumerator()).Returns(users.GetEnumerator());

            _mockUserService.Setup(m => m.GetAllUsers()).ReturnsAsync(users.Select(u => new UserDto
            {
                Balance = u.Balance,
                Comment = u.Comment,
                FirstName = u.FirstName,
                LastName = u.LastName,
                Username = u.UserName,
                IsConfirmedUser = u.IsConfirmedUser
            }).ToList());

            // Act
            var result = await _mockUserService.Object.GetAllUsers();

            // Assert
            Assert.NotNull(result);
            Assert.Equal(2, result.Count);
            Assert.Equal("johndoe", result[0].Username);
            Assert.Equal("janedoe", result[1].Username);
        }

        [Fact]
        public async Task GetUserBalance_ReturnsExpectedBalance_WhenUserExists()
        {
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

            _mockUserService.Setup(m => m.GetAllUsers()).ReturnsAsync(users.Select(u => new UserDto
            {
                Balance = u.Balance,
                Comment = u.Comment,
                FirstName = u.FirstName,
                LastName = u.LastName,
                Username = u.UserName,
                IsConfirmedUser = u.IsConfirmedUser
            }).ToList());

            _mockUserService.Setup(m => m.GetUserBalance(username)).ReturnsAsync(expectedBalance);

            // Act
            var fUsers = await _mockUserService.Object.GetAllUsers();
            var fUser = fUsers.FirstOrDefault(x => x.Username == username);
            var result = await _mockUserService.Object.GetUserBalance(username);

            // Assert
            Assert.Equal(expectedBalance, result);
        }

        [Fact]
        public async Task GetUserBalance_ReturnsZero_WhenUserDoesNotExist()
        {
            // Arrange
            var username = "nonExistentUser";
            var users = new ApplicationUser[0].AsQueryable();

            _mockUserSet.As<IQueryable<ApplicationUser>>().Setup(m => m.Provider).Returns(users.Provider);
            _mockUserSet.As<IQueryable<ApplicationUser>>().Setup(m => m.Expression).Returns(users.Expression);
            _mockUserSet.As<IQueryable<ApplicationUser>>().Setup(m => m.ElementType).Returns(users.ElementType);
            _mockUserSet.As<IQueryable<ApplicationUser>>().Setup(m => m.GetEnumerator()).Returns(users.GetEnumerator());

            _mockUserService.Setup(m => m.GetAllUsers()).ReturnsAsync(users.Select(u => new UserDto
            {
                Balance = u.Balance,
                Comment = u.Comment,
                FirstName = u.FirstName,
                LastName = u.LastName,
                Username = u.UserName,
                IsConfirmedUser = u.IsConfirmedUser
            }).ToList());

            // Act
            var result = await _mockUserService.Object.GetUserBalance(username);

            // Assert
            Assert.Equal(0.0, result);
        }

        [Fact]
        public async Task GetUsersFiltered_WithUsername_ReturnsFilteredUsers()
        {
            // Arrange
            string username = "J";
            List<UserDto> expectedUsers = new List<UserDto>
            {
                new UserDto
                {
                    Balance = 100.0,
                    Comment = "Example comment",
                    FirstName = "John",
                    IsConfirmedUser = true,
                    LastName = "Doe",
                    Username = "example1",
                    Role = "Admin"
                },
                new UserDto
                {
                    Balance = 200.0,
                    Comment = "Another example comment",
                    FirstName = "Jane",
                    IsConfirmedUser = false,
                    LastName = "Smith",
                    Username = "example2",
                    Role = "User"
                }
            };

            _mockUserService.Setup(m => m.GetUsersFiltered(username, null, null)).ReturnsAsync(expectedUsers.Select(u => new UserDto
            {
                Balance = u.Balance,
                Comment = u.Comment,
                FirstName = u.FirstName,
                LastName = u.LastName,
                Username = u.Username,
                IsConfirmedUser = u.IsConfirmedUser
            }).ToList());

            // Act
            var result = await _mockUserService.Object.GetUsersFiltered(username);

            // Assert
            Assert.Equal(expectedUsers.Count, result.Count);
            Assert.True(expectedUsers.All(u => result.Any(r => r.Username == u.Username)));
        }

        [Fact]
        public async Task GetUsersFiltered_WithPageAndPageSize_ReturnsPagedUsers()
        {
            // Arrange
            int page = 1;
            int pageSize = 10;
            List<UserDto> expectedUsers = new List<UserDto>
            {
                new UserDto
                {
                    Balance = 100.0,
                    Comment = "Example comment",
                    FirstName = "John",
                    IsConfirmedUser = true,
                    LastName = "Doe",
                    Username = "example1",
                    Role = "Admin"
                },
                new UserDto
                {
                    Balance = 200.0,
                    Comment = "Another example comment",
                    FirstName = "Jane",
                    IsConfirmedUser = false,
                    LastName = "Smith",
                    Username = "example2",
                    Role = "User"
                }
            };

            _mockUserService.Setup(m => m.GetUsersFiltered(null, page, pageSize)).ReturnsAsync(expectedUsers.Select(u => new UserDto
            {
                Balance = u.Balance,
                Comment = u.Comment,
                FirstName = u.FirstName,
                LastName = u.LastName,
                Username = u.Username,
                IsConfirmedUser = u.IsConfirmedUser
            }).ToList());

            // Act
            var result = await _mockUserService.Object.GetUsersFiltered(null, page, pageSize);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(expectedUsers.Count, result.Count);
            Assert.True(expectedUsers.All(u => result.Any(r => r.Username == u.Username)));
        }

        [Fact]
        public async Task GetUsersFiltered_WithUsernameAndPageAndPageSize_ReturnsFilteredAndPagedUsers()
        {
            // Arrange
            string username = "J";
            int page = 1;
            int pageSize = 10;
            List<UserDto> expectedUsers = new List<UserDto>
            {
                new UserDto
                {
                    Balance = 100.0,
                    Comment = "Example comment",
                    FirstName = "John",
                    IsConfirmedUser = true,
                    LastName = "Doe",
                    Username = "example1",
                    Role = "Admin"
                },
                new UserDto
                {
                    Balance = 200.0,
                    Comment = "Another example comment",
                    FirstName = "Jane",
                    IsConfirmedUser = false,
                    LastName = "Smith",
                    Username = "example2",
                    Role = "User"
                }
            };

            _mockUserService.Setup(m => m.GetUsersFiltered(username, page, pageSize)).ReturnsAsync(expectedUsers.Select(u => new UserDto
            {
                Balance = u.Balance,
                Comment = u.Comment,
                FirstName = u.FirstName,
                LastName = u.LastName,
                Username = u.Username,
                IsConfirmedUser = u.IsConfirmedUser
            }).ToList());

            // Act
            var result = await _mockUserService.Object.GetUsersFiltered(username, page, pageSize);

            // Assert
            Assert.Equal(expectedUsers.Count, result.Count);
            Assert.True(expectedUsers.All(u => result.Any(r => r.Username == u.Username)));
        }
    }
}
