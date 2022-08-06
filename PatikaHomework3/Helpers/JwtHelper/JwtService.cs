namespace PatikaHomework3.Helpers.JwtHelper;


using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using PatikaHomework3.Data.Context;
using PatikaHomework3.Data.Model;
using PatikaHomework3.Dto.Dto;
using PatikaHomework3.Dto.Response;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;



public interface IJwtService
{
    AuthenticateResponse Authenticate(AuthenticateRequest model);

}

public class JwtService : IJwtService
{
    // users hardcoded for simplicity, store in a db with hashed passwords in production applications

    private readonly EfContext _efContext;
    private readonly AppSettings _appSettings;

    public JwtService(IOptions<AppSettings> appSettings,EfContext efContext)
    {
        _appSettings = appSettings.Value;
        _efContext = efContext;
    }

    public AuthenticateResponse Authenticate(AuthenticateRequest model)
    {

        var password = ValidationHelper.GetSha(model.Password);
        var user = _efContext.Account.SingleOrDefault(x => x.Password == password && String.Equals(x.UserName,model.Username));

        //var user = _users.SingleOrDefault(x => x.Username == model.Username && x.Password == model.Password);

        // return null if user not found
        if (user as Account == null ) return null;

        // authentication successful so generate jwt token
        var token = generateJwtToken(user);

        return new AuthenticateResponse(user, token);
    }

    // helper methods

    private string generateJwtToken(Account user)
    {
        // generate token that is valid for 7 days
        var tokenHandler = new JwtSecurityTokenHandler();
        var secret = _appSettings.Secret;
        
        var key = Encoding.ASCII.GetBytes(secret);
        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(new[] { new Claim("id", user.Id.ToString()) }),
            Expires = DateTime.UtcNow.AddDays(7),
            SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
        };
        var token = tokenHandler.CreateToken(tokenDescriptor);
        return tokenHandler.WriteToken(token);
    }
}