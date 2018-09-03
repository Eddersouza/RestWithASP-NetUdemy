using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Principal;
using Microsoft.IdentityModel.Tokens;
using RestWithASPNetUdemy.Data.VO;
using RestWithASPNetUdemy.Model;
using RestWithASPNetUdemy.Repository;
using RestWithASPNetUdemy.Security.Configuration;

namespace RestWithASPNetUdemy.Business.Implementation
{
    public class UserBusinessImpl : IUserBusiness
    {
        private IUserRepository _userRepository;
        private SigningConfiguration _signingConfiguration;
        private TokenConfiguration _TokenConfiguration;

        public UserBusinessImpl(
            IUserRepository userRepository,
            SigningConfiguration signingConfiguration,
            TokenConfiguration tokenConfiguration)
        {
            _userRepository = userRepository;
            _signingConfiguration = signingConfiguration;
            _TokenConfiguration = tokenConfiguration;
        }

        public object FindByLogin(UserVO user)
        {
            bool credentialsIsValid = false;

            if (user != null && !string.IsNullOrEmpty(user.Login))
            {
                var baseUser = _userRepository.FindByLogin(user.Login);
                credentialsIsValid = (baseUser != null
                    && user.Login == baseUser.Login
                    && user.AcessKey == baseUser.AcessKey);
            }

            if (credentialsIsValid)
            {
                ClaimsIdentity identity = new ClaimsIdentity(
                    new GenericIdentity(user.Login, "login"),
                        new[] {
                            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString("N")),
                            new Claim(JwtRegisteredClaimNames.Jti, user.Login)
                        });

                DateTime createDate = DateTime.Now;
                DateTime expirationDate = createDate.AddSeconds(_TokenConfiguration.Seconds);

                var handler = new JwtSecurityTokenHandler();
                string token = CreateToken(identity, createDate, expirationDate, handler);

                return SuccessObject(createDate, expirationDate, token);
            }
            else
            {
                return ExceptionObject();
            }
        }

        private string CreateToken(
            ClaimsIdentity identity,
            DateTime createDate,
            DateTime expirationDate,
            JwtSecurityTokenHandler handler)
        {
            var securityToken = handler.CreateToken(new SecurityTokenDescriptor
            {
                Issuer = _TokenConfiguration.Issuer,
                Audience = _TokenConfiguration.Audience,
                SigningCredentials = _signingConfiguration.SigningCredentials,
                Subject = identity,
                NotBefore = createDate,
                Expires = expirationDate
            });

            var token = handler.WriteToken(securityToken);
            return token;
        }

        private object ExceptionObject()
        {
            return new
            {
                authenticate = false,
                message = "Authentication Failed"
            };
        }

        private object SuccessObject(DateTime createDate, DateTime expirationDate, string token)
        {
            return new
            {
                authenticate = true,
                created = createDate.ToString("yyyy-MM-dd HH:mm:ss"),
                expiration = expirationDate.ToString("yyyy-MM-dd HH:mm:ss"),
                accessToken = token,
                message = "Ok"
            };
        }
    }
}