using Core.DataAccess.Entity_Framework;
using Core.Entities.Concrete;
using Core.Entities.Dtos;
using DataAccess.Absract;
using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OperationClaimDto = Core.Entities.Dtos.OperationClaimDto;

namespace DataAccess.Concrete.Entity_Framework
{
    public class EfUserDal : EfEntityRepositoryBase<User, ReCapProjectDbContext>, IUserDal
    {
        public List<OperationClaimDto> GetClaims(User user)
        {
            using (var context = new ReCapProjectDbContext())
            {
                var result = from operationClaim in context.OperationClaims
                             join userOperationClaim in context.UserOperationClaims
                             on operationClaim.Id equals userOperationClaim.OperationClaimId
                             where userOperationClaim.UserId == user.Id
                             select new OperationClaimDto() { Id = operationClaim.Id, Name = operationClaim.Name ,UserName = $"{user.FirstName} {user.LastName}"};
                return result.ToList();
            }
        }
    }
}
