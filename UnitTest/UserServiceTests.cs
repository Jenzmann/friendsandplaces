using FriendsAndPlaces.Models.API;
using FriendsAndPlaces.Services;
using Moq;
using Xunit;

namespace FriendsAndPlaces.Tests.Services
{
    public class UserServiceTests
    {
        private readonly Mock<AuthTokenService> _authTokenServiceMock;
        private readonly Mock<LocationService> _locationServiceMock;
        private readonly UserService _userService;

        public UserServiceTests()
        {
            _authTokenServiceMock = new Mock<AuthTokenService>();
            _locationServiceMock = new Mock<LocationService>();
            _userService = new UserService(_authTokenServiceMock.Object, _locationServiceMock.Object);
        }

        [Fact]
        public void AddUser_ShouldAddUserToList()
        {
            var user = new UserModel { LoginName = "testuser" };

            _userService.AddUser(user);
            var result = _userService.GetUser("testuser");

            Assert.NotNull(result);
            Assert.Equal("testuser", result.LoginName);
        }

        [Fact]
        public void UpdateLocation_ShouldUpdateCoordinates()
        {
            var user = new UserModel { LoginName = "locationUser" };
            _userService.AddUser(user);

            var location = new LocationModel
            {
                Standort = new CoordinateModel { Breitengrad = 10.1f, Laengengrad = 20.2f }
            };

            var result = _userService.UpdateLocation("locationUser", location);
            var updatedUser = _userService.GetUser("locationUser");

            Assert.True(result);
            Assert.Equal(10.1f, updatedUser.Breitengrad);
            Assert.Equal(20.2f, updatedUser.Laengengrad);
        }

        [Fact]
        public void UpdateLocation_ShouldReturnFalse_IfUserNotFound()
        {
            var location = new LocationModel
            {
                Standort = new CoordinateModel { Breitengrad = 1.1f, Laengengrad = 2.2f }
            };

            var result = _userService.UpdateLocation("nonexistent", location);

            Assert.False(result);
        }

        [Fact]
        public async Task GetLocation_ShouldReturnStoredCoordinates_WhenAuthorized()
        {
            var user = new UserModel { LoginName = "coordUser" };
            _userService.AddUser(user);
            _userService.UpdateLocation("coordUser", new LocationModel
            {
                Standort = new CoordinateModel { Breitengrad = 50.5f, Laengengrad = 60.6f }
            });

            _authTokenServiceMock.Setup(x => x.HasAuth("coordUser")).Returns(true);

            var location = await _userService.GetLocation("coordUser");

            Assert.NotNull(location);
            Assert.Equal(50.5f, location.Breitengrad);
            Assert.Equal(60.6f, location.Laengengrad);
        }

        [Fact]
        public async Task GetLocation_ShouldFallbackToAddressLookup_WhenNoCoordinates()
        {
            var user = new UserModel
            {
                LoginName = "addressUser",
                Land = "DE",
                Plz = "12345",
                Ort = "Berlin",
                Strasse = "Main St"
            };

            _userService.AddUser(user);
            _authTokenServiceMock.Setup(x => x.HasAuth("addressUser")).Returns(true);
            _locationServiceMock
                .Setup(x => x.GetLocationFromAddress("DE", "12345", "Berlin", "Main St"))
                .ReturnsAsync(new CoordinateModel { Breitengrad = 1.23f, Laengengrad = 4.56f });

            var result = await _userService.GetLocation("addressUser");

            Assert.NotNull(result);
            Assert.Equal(1.23f, result.Breitengrad);
            Assert.Equal(4.56f, result.Laengengrad);
        }

        [Fact]
        public void RemoveUser_ShouldRemoveUserFromList()
        {
            var user = new UserModel { LoginName = "removeMe" };
            _userService.AddUser(user);

            _userService.RemoveUser("removeMe");
            var result = _userService.GetUser("removeMe");

            Assert.Null(result);
        }

        [Fact]
        public void CheckLoginData_ShouldReturnTrue_WhenCorrectCredentials()
        {
            var user = new UserModel
            {
                LoginName = "loginUser",
                Passwort = new PasswordModel { Passwort = "secret" }
            };

            _userService.AddUser(user);
            var result = _userService.CheckLoginData("loginUser", "secret");

            Assert.True(result);
        }

        [Fact]
        public void CheckLoginData_ShouldReturnFalse_WhenWrongPassword()
        {
            var user = new UserModel
            {
                LoginName = "wrongPassUser",
                Passwort = new PasswordModel { Passwort = "correct" }
            };

            _userService.AddUser(user);
            var result = _userService.CheckLoginData("wrongPassUser", "wrong");

            Assert.False(result);
        }

        [Theory]
        [InlineData("abc", false)]  // Too short
        [InlineData("validName", true)]
        [InlineData("duplicate", false)]
        public void LoginNameValid_ShouldValidateCorrectly(string loginName, bool expected)
        {
            _userService.AddUser(new UserModel { LoginName = "duplicate" });

            var result = _userService.LoginNameValid(loginName);

            Assert.Equal(expected, result);
        }

        [Fact]
        public void GetAllUsers_ShouldReturnLiteModels()
        {
            _userService.AddUser(new UserModel
            {
                LoginName = "u1",
                NachName = "Doe",
                Vorname = "Jane"
            });

            var users = _userService.GetAllUsers();

            Assert.Single(users);
            Assert.Equal("u1", users[0].LoginName);
            Assert.Equal("Doe", users[0].NachName);
            Assert.Equal("Jane", users[0].Vorname);
        }
    }
}
