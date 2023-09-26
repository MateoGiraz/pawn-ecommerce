using Moq;
using Service.User;
using Moq;
using Service.Exception;

namespace Test.Service.UserTest;

[TestClass]
public class UserServiceTest
{
    private const string Email = "TestEmail@gmail.com";
    private const string Password = "currentPassword";
    private const string DifferentPassword = "differentPassword";
    private const string ToUpdateAddress = "1234 Laughter Lane";
    private const string NewAddress = "101 Prankster Place";

    private IUser GetMockUser()
    {
        var mockUser = new Mock<IUser>();
        mockUser.Setup(user => user.Email).Returns(Email);
        mockUser.Setup(user => user.Address).Returns(ToUpdateAddress);
        mockUser.Setup(user => user.PasswordHash).Returns(HashPassword(Password));
        
        return mockUser.Object;
    }
    
    private IUser GetSecondaryMockUser()
    {
        var mockUser = new Mock<IUser>();
        mockUser.Setup(user => user.Email).Returns(Email);
        mockUser.Setup(user => user.Address).Returns(NewAddress);
        mockUser.Setup(user => user.PasswordHash).Returns(HashPassword(DifferentPassword));
        
        return mockUser.Object;
    }
    
    [TestMethod]
    public void CanCreateUserService_Ok()
    {
        var mockRepository = new Mock<IUserRepository>();
        var userService = new UserService(mockRepository.Object);
        Assert.IsNotNull(userService);
    }

    [TestMethod]
    public void CanSignUpUser_Ok()
    {
        var mockUser = GetMockUser();
        var mockRepository = new Mock<IUserRepository>();
        
        var userService = new UserService(mockRepository.Object);
        userService.SignUp(mockUser);
    }
    
    [ExpectedException(typeof(RepositoryException))]
    [TestMethod]
    public void CanSignUpUser_RepeatedUser_Throw()
    {
        var mockUser = GetMockUser();
        
        var mockRepository = new Mock<IUserRepository>();
        mockRepository.Setup(repo => repo.Get(mockUser.Email)).Returns(mockUser);
        
        var userService = new UserService(mockRepository.Object);
        userService.SignUp(mockUser);
    }
    
    [ExpectedException(typeof(RepositoryException))]
    [TestMethod]
    public void CanLogInUser_UnregisteredUser_Throw()
    {
        var mockUser = GetMockUser();

        var mockRepository = new Mock<IUserRepository>();
        mockRepository.Setup(repo => repo.Get(mockUser.Email)).Returns(() => null);
        
        var userService = new UserService(mockRepository.Object);
        userService.LogIn(Email, Password);
    }
    
    [ExpectedException(typeof(RepositoryException))]
    [TestMethod]
    public void CanLogInUser_WrongPassword_Throw()
    {
        var mockUser = GetMockUser();
        
        var mockRepository = new Mock<IUserRepository>();
        mockRepository.Setup(repo => repo.Get(Email)).Returns(mockUser);

        var userService = new UserService(mockRepository.Object);
        userService.LogIn(Email, DifferentPassword);
    }
    
    [TestMethod]
    public void CanLogInUser_Ok()
    {
        var mockUser = GetMockUser();

        var mockRepository = new Mock<IUserRepository>();
        mockRepository.Setup(repo => repo.Get(Email)).Returns(mockUser);

        var userService = new UserService(mockRepository.Object);
        var loggedInUser = userService.LogIn(Email, Password);
        
        Assert.AreEqual(mockUser, loggedInUser);
    }

    private string HashPassword(string Password)
    {
        const int saltFactor = 12;
            
        var salt = BCrypt.Net.BCrypt.GenerateSalt(saltFactor);
        var hashedPassword = BCrypt.Net.BCrypt.HashPassword(Password, salt);

        return hashedPassword;
    }
        
    [ExpectedException(typeof(RepositoryException))]
    [TestMethod]
    public void CanDeleteUser_AlreadyDeletedUser_Throw()
    {
        var mockUser = GetMockUser();

        var mockRepository = new Mock<IUserRepository>();
        mockRepository.Setup(repo => repo.Get(Email)).Returns(() => null);

        var userService = new UserService(mockRepository.Object);
        userService.DeleteUser(mockUser);
    }
    
    [TestMethod]
    public void CanDeleteUser_Ok()
    {
        var mockUser = GetMockUser();

        var mockRepository = new Mock<IUserRepository>();
        mockRepository.Setup(repo => repo.Get(Email)).Returns(mockUser);

        var userService = new UserService(mockRepository.Object);
        userService.DeleteUser(mockUser);
    }
    
    [ExpectedException(typeof(RepositoryException))]
    [TestMethod]
    public void CanUpdateUser_NotExistingUser_Throw()
    {
        var toUpdateMockUser = GetMockUser();
        var mockUser = GetSecondaryMockUser();
        
        var mockRepository = new Mock<IUserRepository>();
        mockRepository.Setup(repo => repo.Get(Email)).Returns(() => null);
        mockRepository.Setup(repo => repo.Update(toUpdateMockUser));
        
        var userService = new UserService(mockRepository.Object);
        userService.UpdateUser(mockUser);
    }
}