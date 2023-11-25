using Business.Abstract;
using Business.Constans;
using Core.Entities.Concrete;
using Core.Entities.Dtos;
using Core.Utility.Results;
using DataAccess.Absract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OperationClaimDto = Core.Entities.Dtos.OperationClaimDto;

namespace Business.Concrete
{
    public class UserManager : IUserSevices
    {
        IUserDal _userDal;
        public UserManager(IUserDal userDal)
        {
            _userDal = userDal;
        }

        public IResult Add(User user)
        {
            _userDal.Add(user);
            return new SuccessResult(Messages.UserAdded);
        }

        public IDataResult<User> GetByMail(string email)
        {
            return new SuccessDataResult<User>(_userDal.Get(u => u.Email == email));
        }

        public IDataResult<List<OperationClaimDto>> GetClaims(User user)
        {
            return new SuccessDataResult<List<OperationClaimDto>>(_userDal.GetClaims(user));
        }
    }
}
