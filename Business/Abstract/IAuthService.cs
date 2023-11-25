using Core.Entities.Concrete;
using Core.Utility.Results;
using Core.Utility.Security.Jwt;
using Entities.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security;
using System.Text;
using System.Threading.Tasks;

namespace Business.Abstract
{
    public interface IAuthService
    {
        IDataResult<User> Register(UserForRegisterDto userForRegisterDto);
        IDataResult<User> Login(UserForLoginDto userForLoginDto);
        IResult UserExist(string email);   // kayıtlı kullanıcı var mı
        IDataResult<AccessToken> CreateAccessToken(User user);
    }
}
