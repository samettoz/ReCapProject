using Core.DataAccess.Entity_Framework;
using DataAccess.Absract;
using DataAccess.Concrete.Entity_Framework;
using Entities.Concrete;
using Entities.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Concrete
{
    public class EfRentalDal : EfEntityRepositoryBase<Rental, ReCapProjectDbContext>, IRentalDal
    {
        public List<RentalDetailDto> GetRentalDetails(Expression<Func<Rental, bool>> filter = null)
        {
            using (ReCapProjectDbContext context = new())
            {
                var result = from rental in filter == null ? context.Rentals : context.Rentals.Where(filter)
                             join car in context.Cars on rental.CarId equals car.Id
                             join customer in context.Customers on rental.CustomerId equals customer.Id
                             select new RentalDetailDto()
                             {
                                 CarId = car.Id,
                                 CarName = car.Name,
                                 CustomerId = customer.Id,
                                 CustomerName = customer.CompanyName,
                                 Id = rental.Id,
                                 RentDate = rental.RentDate,
                                 ReturnDate = rental.ReturnDate
                             };
                return result.ToList();
            }
        }
    }
}
