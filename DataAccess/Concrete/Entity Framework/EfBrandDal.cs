using Core.DataAccess.Entity_Framework;
using DataAccess.Absract;
using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Concrete.Entity_Framework
{
    public class EfBrandDal : EfEntityRepositoryBase<Brand,ReCapProjectDbContext>, IBrandDal
    {
    }
}
