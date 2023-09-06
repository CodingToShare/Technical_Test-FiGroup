using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Moq;
using webapi.Exceptions;
using webapi.Interfaces;
using webapi.ModelDto;
using webapi.Services;

namespace UnitTestAPI
{
    [TestFixture]
    public class UnitTestAuthService
    {
        private Mock<UserManager<IdentityUser>> _userManagerMock;
        private Mock<IJwtSettings> _jwtSettingsMock;
        private AuthService _authService;

        [SetUp]
        public void Setup()
        {
            var store = new FakeUserStore();
            _userManagerMock = new Mock<UserManager<IdentityUser>>(store, null, null, null, null, null, null, null, null);
            _jwtSettingsMock = new Mock<IJwtSettings>();
            _authService = new AuthService(_userManagerMock.Object, _jwtSettingsMock.Object);
        }

        [Test]
        public async System.Threading.Tasks.Task Login_ValidCredentials_ReturnsJwt()
        {
            // Arrange  
            var user = new IdentityUser { Email = "test@test.com" };
            var model = new LoginModel { Email = "test@test.com", Password = "Pa$$w0rd1" };
            var roles = new List<string> { "Admin", "User" };
            _userManagerMock.Setup(x => x.Users).Returns(new List<IdentityUser> { user }.AsQueryable());
            _userManagerMock.Setup(x => x.CheckPasswordAsync(user, model.Password)).ReturnsAsync(true);
            _userManagerMock.Setup(x => x.GetRolesAsync(user)).ReturnsAsync(roles);
            _jwtSettingsMock.Setup(x => x.Secret).Returns("secret");

            // Act  
            var result = await _authService.Login(model);

            // Assert  
            Assert.IsNotNull(result);
            Assert.IsNotEmpty(result);
        }

        [Test]
        public void Login_InvalidCredentials_ThrowsException()
        {
            // Arrange  
            var model = new LoginModel { Email = "test@test.com", Password = "Pa$$w0rd1" };
            _userManagerMock.Setup(x => x.Users).Returns(new List<IdentityUser>().AsQueryable());

            // Act & Assert  
            Assert.ThrowsAsync<LoginCredentialsException>(() => _authService.Login(model));
        }

        [Test]
        public async System.Threading.Tasks.Task Register_ValidModel_ReturnsTrue()
        {
            // Arrange  
            var model = new RegisterModel { Email = "test@test.com", Password = "Pa$$w0rd1" };
            _userManagerMock.Setup(x => x.CreateAsync(It.IsAny<IdentityUser>(), model.Password)).ReturnsAsync(IdentityResult.Success);

            // Act  
            var result = await _authService.Register(model);

            // Assert  
            Assert.IsTrue(result);
        }

        [Test]
        public void Register_InvalidModel_ThrowsException()
        {
            // Arrange  
            var model = new RegisterModel { Email = "test@test.com", Password = "Pa$$w0rd1" };
            _userManagerMock.Setup(x => x.CreateAsync(It.IsAny<IdentityUser>(), model.Password)).ReturnsAsync(IdentityResult.Failed(new IdentityError { Description = "Email is already taken" }));

            // Act & Assert  
            Assert.ThrowsAsync<AccountRegisterException>(() => _authService.Register(model));
        }

    }
}
