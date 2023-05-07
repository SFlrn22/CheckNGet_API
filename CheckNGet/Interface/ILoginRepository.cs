using CheckNGet.Models;
using CheckNGet.Models.DTO;

namespace CheckNGet.Interface
{
    public interface ILoginRepository
    {
        string Generate(User user);
        User Authenticate(UserLoginDTO userLogin);
    }
}
