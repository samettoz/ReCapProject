using Core.Entities.Concrete;
using Core.Entities.Dtos;
using Core.Utility.Results;
using Microsoft.EntityFrameworkCore.SqlServer.Query.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OperationClaimDto = Core.Entities.Dtos.OperationClaimDto;

namespace Business.Abstract
{
    public interface IUserSevices
    {
        IDataResult<List<OperationClaimDto>> GetClaims(User user);
        IResult Add(User user);
        IDataResult<User> GetByMail(string email);
    }
}
