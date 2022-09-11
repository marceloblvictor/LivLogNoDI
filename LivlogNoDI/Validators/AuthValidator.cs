using LivlogNoDI.Models.DTO;

namespace LivlogNoDI.Validators
{
    public class AuthValidator
    {
        public void ValidateNewUser(IEnumerable<UserDTO> users, UserDTO dto)
        {
            if (users.Any(u => u.Username == dto.Username))
            {
                throw new Exception("O nome de usuário fornecido já se encontra em utilização");
            }

            if (dto.Password is { Length: < 6 })
            {
                throw new Exception("A senha deve possuir no mínimo 6 caracteres");
            }
            
            if (users.Any(u => u.Email == dto.Email))
            {
                throw new Exception("O email fornecido já se encontra em utilização");
            }
        }
    }
}
