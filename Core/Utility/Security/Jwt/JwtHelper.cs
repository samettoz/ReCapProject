using Core.Entities.Concrete;
using Core.Entities.Dtos;
using Core.Extensions;
using Core.Utility.Security.Encryption;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using OperationClaimDto = Core.Entities.Dtos.OperationClaimDto;

namespace Core.Utility.Security.Jwt
{
    public class JwtHelper : ITokenHelper
    {
        public IConfiguration Configuration { get; }
        TokenOptions _tokenOptions;
        DateTime _accessTokenExpiration;
        public JwtHelper(IConfiguration configuration)
        {
            Configuration = configuration;
            _tokenOptions = Configuration.GetSection("TokenOptions").Get<TokenOptions>(); 
            _accessTokenExpiration = DateTime.Now.AddMinutes(_tokenOptions.AccessTokenExpiration);

        }
        public AccessToken CreateToken(User user, List<OperationClaimDto> operationClaims)
        {
            var securityKey = SecurityKeyHelper.CreateSecurityKey(_tokenOptions.SecurityKey);
            var singningCredential = SigningCredentialHelper.CreateSigningCredentials(securityKey);
            var jwt = CreateJwtSecurityToken(_tokenOptions, singningCredential, user, operationClaims);
            var jwtSecurityTokenHandler = new JwtSecurityTokenHandler();
            var token = jwtSecurityTokenHandler.WriteToken(jwt);

            return new AccessToken() 
            {
                Token = token,
                Expiration = _accessTokenExpiration,
            };

        }

        public JwtSecurityToken CreateJwtSecurityToken(TokenOptions tokenOptions,SigningCredentials signingCredentials,
            User user,List<OperationClaimDto> operationClaims)
        {
            var jwtSecurityToken = new JwtSecurityToken(
                issuer: _tokenOptions.Issuer,
                audience: _tokenOptions.Audience,
                expires: _accessTokenExpiration,
                notBefore: DateTime.Now,
                claims: SetClaims(user,operationClaims),
                signingCredentials: signingCredentials
                );
            return jwtSecurityToken;
        }

        private IEnumerable<Claim> SetClaims(User user, List<OperationClaimDto> operationClaims)
        {
            var claims = new List<Claim>();
            claims.AddNameIdentifier(user.Id.ToString());
            claims.AddName($"{user.FirstName} {user.LastName}");
            claims.AddEmail(user.Email);
            claims.AddRoles(operationClaims.Select(c => c.Name).ToArray());

            return claims;


        }
    }
}
