using DataAccessLayer.Interfaces;
using System.Net.Mail;
using System.Net;
using DataAccessLayer.ViewModels;
using DataAccessLayer.Data;

namespace DataAccessLayer.Implementations;

public class LoginRepository : ILoginRepository
{

    private readonly IGenericRepository _genericRepository;
    private readonly RealEstateContext db;
    public LoginRepository(IGenericRepository genericRepository, RealEstateContext db)
    {
        _genericRepository = genericRepository;
        this.db = db;

    }

    public string GenerateOTP()
    {
        Random rnd = new Random();
        return rnd.Next(1000, 10000).ToString();
    }


    public bool SendOTP(string email, string otp)

    {
        try
        {
            MailMessage message = new MailMessage();
            message.From = new MailAddress("realestate@gmail.com"); 
            message.To.Add(email);
            message.Subject = "OTP for Sign Up";
            message.Body = $"Your OTP for sign up is: {otp}";

            SmtpClient smtp = new SmtpClient("smtp.gmail.com", 587);
            smtp.UseDefaultCredentials = false;
            smtp.Credentials = new NetworkCredential("realestate@gmail.com", "real-estate");
            smtp.EnableSsl = true;


            smtp.Send(message);

            smtp.Dispose();

            return true;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Failed to send email: {ex.Message}");
            return false;
        }
    }

    public OwnerVM Login(LoginVM model)
    {
        var result = db.Owners.Where(p => p.Email == model.Email && p.Password == model.Password).FirstOrDefault();
        if (result != null)
        {
            var ownerModel = new OwnerVM()
            {
                Id = result.Id,
                FirstName = result.FirstName,
                LastName = result.LastName,
                Email = result.Email,
                Contact = result.Contact,
                Password = result.Password,
            };
            return ownerModel;
        }
        else
        {
            return null;
        }
    }
}
