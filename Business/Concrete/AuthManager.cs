using Business.Abstract;
using Business.Constans;
using Core.Entities.Concrete;
using Core.Utility.Results;
using Core.Utility.Security.Hashing;
using Core.Utility.Security.Jwt;
using Entities.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Concrete
{
    public class AuthManager : IAuthService
    {
        IUserSevices _userServices;
        ITokenHelper _tokenHelper;
        public AuthManager(IUserSevices userSevices, ITokenHelper tokenHelper)
        {
            _userServices = userSevices;
            _tokenHelper = tokenHelper;   // Jwt helper'ı kullanarak token üretmek için

        }



        public IDataResult<AccessToken> CreateAccessToken(User user)
        {
            var claims = _userServices.GetClaims(user);
            var accessToken = _tokenHelper.CreateToken(user,claims.Data);
            return new SuccessDataResult<AccessToken>(accessToken,Messages.AccessTokenCreated); 
        }

        public IDataResult<User> Login(UserForLoginDto userForLoginDto)
        {
            // mailine bakılarak böyle bir kullanıcı var mı kontrol edilir
            var userToCheck = _userServices.GetByMail(userForLoginDto.Email);
            if (userToCheck == null) 
            {
                return new ErrorDataResult<User>(Messages.UserNotFound);
            }
            if (HashingHelper.VerifyPasswordHash(userForLoginDto.Password, userToCheck.Data.PasswordHash, userToCheck.Data.PasswordSalt) == false)
            {
                return new ErrorDataResult<User>(Messages.PasswordError);
            }

            return new SuccessDataResult<User>(userToCheck.Data,Messages.SuccessfulLogin);
        }

        public IDataResult<User> Register(UserForRegisterDto userForRegisterDto)
        {
            byte[] passwordHash;
            byte[] passwordSalt;
            //UserExist(userForRegisterDto.Email);
            HashingHelper.CreateHashingPassword(userForRegisterDto.Password,out passwordHash,out passwordSalt);
            var user = new User()
            {
                Email = userForRegisterDto.Email,
                FirstName = userForRegisterDto.FirstName,
                LastName = userForRegisterDto.LastName,
                PasswordHash = passwordHash,
                PasswordSalt = passwordSalt,
                Status = true        
            };
            _userServices.Add(user);
            return new SuccessDataResult<User>(user,Messages.UserRegistered);
        }

        // register işleminde kullanmak için
        public IResult UserExist(string email)
        {
            var userToCheck = _userServices.GetByMail(email);
            if (userToCheck.Data !=null)
            {
                return new ErrorResult(Messages.UserAlreadyExist);
            }
            return new SuccessResult();
        }
    }
}
