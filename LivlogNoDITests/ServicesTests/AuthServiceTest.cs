using LivlogNoDI.Models.DTO;
using LivlogNoDI.Models.Entities;
using LivlogNoDI.Services;

namespace LivlogNoDITests.ServicesTests
{
    public class AuthServiceTest
    {
        AuthService _service { get; set; }

        User ValidUser { get; set; } = new()
        {
            Id = 1,
            Username = "marceloblvictor",
            Password = "abcdef1234",
            Email = "marceloblvictor@gmail.com"
        };

        IList<User> ValidUsers = new List<User>()
        {
            new()
            {
                Id = 1,
                Username = "marceloblvictor",
                Password = "abcdef1234",
                Email = "marceloblvictor@gmail.com"
            },
            new()
            {
                Id = 2,
                Username = "marceloblvictorXXX",
                Password = "abcdef1234",
                Email = "marceloblvictorXXX@gmail.com"
            },
            new()
            {
                Id = 3,
                Username = "marceloblvictorYYY",
                Password = "abcdef1234",
                Email = "marceloblvictorYYY@gmail.com"
            },
            new()
            {
                Id = 4,
                Username = "marceloblvictorZZZ",
                Password = "abcdef1234",
                Email = "marceloblvictorZZZ@gmail.com"
            },
        };

        UserDTO ValidUserDTO { get; set; } = new()
        {
            Id = 1,
            Username = "marceloblvictor",
            Password = "abcdef1234",
            Email = "marceloblvictor@gmail.com"
        };

        IList<UserDTO> ValidUsersDTOs = new List<UserDTO>()
        {
            new()
            {
                Id = 1,
                Username = "marceloblvictor",
                Password = "abcdef1234",
                Email = "marceloblvictor@gmail.com"
            },
            new()
            {
                Id = 2,
                Username = "marceloblvictorXXX",
                Password = "abcdef1234",
                Email = "marceloblvictorXXX@gmail.com"
            },
            new()
            {
                Id = 3,
                Username = "marceloblvictorYYY",
                Password = "abcdef1234",
                Email = "marceloblvictorYYY@gmail.com"
            },
            new()
            {
                Id = 4,
                Username = "marceloblvictorZZZ",
                Password = "abcdef1234",
                Email = "marceloblvictorZZZ@gmail.com"
            },
        };

        public AuthServiceTest()
        {
            _service = new AuthService();
        }

        [Fact]
        public void GetUserByUserName_ValidUsernameGiven_ReturnsUserWithGivenUsername()
        {
            // Arrange
            var validUsername = "marceloblvictor";

            // Act
            var user = _service.GetUserByUsername(ValidUsersDTOs, validUsername);

            // Assert
            Assert.True(user.Username == validUsername);
        }

        [Fact]
        public void GetUserByUserName_InvalidUsernameGiven_ReturnsNull()
        {
            // Arrange
            var invalidUsername = "marceloblvictorttttt";

            // Act
            var user = _service.GetUserByUsername(ValidUsersDTOs, invalidUsername);

            // Assert
            Assert.True(user is null);
        }

        [Fact]
        public void IsUserValid_GivenValidUser_ReturnsTrue()
        {
            // Act
            var result = _service.IsUserValid(ValidUsersDTOs, ValidUserDTO);

            // Assert
            Assert.True(result);
        }

        [Fact]
        public void IsUserValid_GivenInvalidUsername_ReturnsFalse()
        {
            // Arrange
            var invalidUser = ValidUserDTO;
            invalidUser.Username = "123456";

            // Act
            var result = _service.IsUserValid(ValidUsersDTOs, ValidUserDTO);

            // Assert
            Assert.True(result is false);
        }

        [Fact]
        public void IsUserValid_GivenInvalidPassword_ReturnsFalse()
        {
            // Arrange
            var invalidUser = ValidUserDTO;
            invalidUser.Password = "123456";

            // Act
            var result = _service.IsUserValid(ValidUsersDTOs, ValidUserDTO);

            // Assert
            Assert.True(result is false);
        }

        [Fact]
        public void ArePasswordsEqual_GivenEqualPasswords_ReturnsTrue()
        {
            // Arrange
            var validPassword = "abcdef1234";

            // Act
            var result = _service.ArePasswordsEqual(validPassword, ValidUserDTO.Password);

            // Assert
            Assert.True(result);
        }

        [Fact]
        public void ArePasswordsEqual_GivenDifferentePasswords_ReturnsFalse()
        {
            // Arrange
            var invalidPassword = "abcdefdfsd4";

            // Act
            var result = _service.ArePasswordsEqual(invalidPassword, ValidUserDTO.Password);

            // Assert
            Assert.True(result is false);
        }

        [Fact]
        public void FilterUserByUsername_GivenValidUsername_ReturnsUsersWithGivenName()
        {
            // Arrange
            var validUsername = "marceloblvictor";

            // Act
            var filteredUsers = _service.FilterUserByUsername(ValidUsersDTOs, validUsername);

            // Assert
            Assert.True(filteredUsers.All(u => u.Username == validUsername));
        }

        [Fact]
        public void FilterUserByUsername_GivenInvalidUsername_ReturnsEmptyList()
        {
            // Arrange
            var invalidUsername = "marcelosdfsdictor";

            // Act
            var filteredUsers = _service.FilterUserByUsername(ValidUsersDTOs, invalidUsername);

            // Assert
            Assert.True(filteredUsers.Count == 0);
        }

        [Fact]
        public void GenerateToken_GivenValidUserClaimsData_ReturnsTokenString()
        {
            // Act
            var token = _service.GenerateToken(ValidUserDTO);

            // Assert
            Assert.True(!string.IsNullOrEmpty(token));
        }

        [Fact]
        public void GenerateToken_GivenInvalidEmail_ThrowsException()
        {
            // Arrange
            var invalidUser = ValidUserDTO;
            ValidUserDTO.Email = "";

            // Act
            var tokenGenerationFunc = () => _service.GenerateToken(invalidUser);

            // Assert
            Assert.ThrowsAny<Exception>(tokenGenerationFunc);
        }

        [Fact]
        public void GenerateToken_GivenEmptyUsername_ThrowsException()
        {
            // Arrange
            var invalidUser = ValidUserDTO;
            ValidUserDTO.Username = "";

            // Act
            var tokenGenerationFunc = () => _service.GenerateToken(invalidUser);

            // Assert
            Assert.ThrowsAny<Exception>(tokenGenerationFunc);
        }

        [Fact]
        public void CreateDTO_GenerateDTOWithEntityData_Succes()
        {
            // Arrange
            var validUser = ValidUser;

            // Act
            var dto = _service.CreateDTO(validUser);

            // Assert
            Assert.True(dto.Id == validUser.Id);
            Assert.True(dto.Username == validUser.Username);
            Assert.True(dto.Password == validUser.Password);
            Assert.True(dto.Email == validUser.Email);
        }

        [Fact]
        public void CreateDTOS_GenerateDTOSWithEntityData_Succes()
        {
            // Arrange
            var validUsers = ValidUsers;

            // Act
            var dtos = _service.CreateDTOs(validUsers);

            // Assert

            foreach (var dto in dtos)
            {
                var validUser =
                    validUsers.First(u => u.Id == dto.Id);

                Assert.True(dto.Id == validUser.Id);
                Assert.True(dto.Username == validUser.Username);
                Assert.True(dto.Password == validUser.Password);
                Assert.True(dto.Email == validUser.Email);
            }
        }

        [Fact]
        public void CreateEntity_GenerateEntityWithDTOData_Succes()
        {
            // Arrange
            var validUserDto = ValidUserDTO;

            // Act
            var entity = _service.CreateEntity(validUserDto);

            // Assert
            Assert.True(entity.Id == validUserDto.Id);
            Assert.True(entity.Username == validUserDto.Username);
            Assert.True(entity.Password == validUserDto.Password);
            Assert.True(entity.Email == validUserDto.Email);
        }
    }
}