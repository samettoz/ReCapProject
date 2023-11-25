using Core.DataAccess;
using Core.Entities.Concrete;
using Core.Entities.Dtos;
using Entities.Concrete;
using Entities.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Absract
{
    public interface IUserDal : IEntityRepository<User>
    {
        List<Core.Entities.Dtos.OperationClaimDto> GetClaims(User user);
    }
}
