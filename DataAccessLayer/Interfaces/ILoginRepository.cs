using DataAccessLayer.ViewModels;

namespace DataAccessLayer.Interfaces;

public interface ILoginRepository
{
    string GenerateOTP();
    bool SendOTP(string email, string otp);
    OwnerVM Login(LoginVM model);
}
