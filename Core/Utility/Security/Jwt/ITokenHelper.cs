using Core.Entities.Concrete;
using Core.Entities.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Utility.Security.Jwt
{
    public interface ITokenHelper
    {
        AccessToken CreateToken(User user, List<Entities.Dtos.OperationClaimDto> operationClaims);   
    }
}
