using Business.Abstract;
using Core.Utility.Results;
using DataAccess.Absract;
using Entities.Concrete;
using Entities.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Concrete
{
    public class RentalManager : IRentalServices
    {
        IRentalDal _rentalDal;
        public RentalManager(IRentalDal rentalDal)
        {
            _rentalDal = rentalDal;
        }
        public IResult Add(Rental rental)
        {
            _rentalDal.Add(rental);
            return new SuccessResult();
        }

        public IResult CheckReturnDate(int carId)
        {
            var result = _rentalDal.Get(r => r.CarId == carId);

            if (result.ReturnDate != null)
                return new SuccessResult();
            else
                return new ErrorResult();

        }

        public IResult UpdateReturnDate(int carId)
        {
            var result = _rentalDal.Get(r => r.CarId == carId);

            if (result.ReturnDate == null)
            {
                return new ErrorResult();
            }
            else
            {
                result.ReturnDate = DateTime.Now;
                _rentalDal.Update(result);
                return new SuccessResult();
            }

        }

        public IResult Delete(Rental rental)
        {
            _rentalDal.Delete(rental);
            return new SuccessResult();
        }

        public IDataResult<List<Rental>> GetAll()
        {
            return new SuccessDataResult<List<Rental>>(_rentalDal.GetAll());
        }

        public IDataResult<Rental> GetById(int id)
        {
            return new SuccessDataResult<Rental>(_rentalDal.Get(r => r.Id == id)); 
        }

        public IDataResult<List<RentalDetailDto>> GetRentalDetail(int carId)
        {
            return new SuccessDataResult<List<RentalDetailDto>>(_rentalDal.GetRentalDetails(r => r.CarId == carId));
        }

        public IResult Update(Rental rental)
        {
            _rentalDal.Update(rental);
            return new SuccessResult();
        }
    }
}
