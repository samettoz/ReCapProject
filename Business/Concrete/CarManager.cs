using Business.Abstract;
using Business.Constans;
using Business.ValidationRules.FluentValidation;
using Core.Aspects.Autofac.Validation;
using Core.CrossCuttingConcerns.Validation.FluentValidation;
using Core.Utility.Business;
using Core.Utility.Results;
using DataAccess.Absract;
using DataAccess.Concrete.Entity_Framework;
using Entities.Concrete;
using Entities.Dto;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Concrete
{
    public class CarManager : ICarService
    {
        ICarDal _carDal;
        IBrandServices _brandServices;
        public CarManager(ICarDal carDal, IBrandServices brandServices)
        {
            _carDal = carDal;
            _brandServices = brandServices;

        }

        [ValidationAspect(typeof(CarValidator))]
        public IResult Add(Car car)
        {
            IResult result = BusinessRules.Run(CheckIfCarNameExısts(car.Name),
                CheckIfBrandLimitExceded());
            if (result != null)
            {
                return result;
            }
            _carDal.Add(car);
            return new SuccessResult(Messages.CarAdded);
        }

        public IResult Delete(Car car)
        {
            _carDal.Delete(car);
            return new SuccessResult();
        }

        public IDataResult<List<Car>> GetAll()
        {
            return new SuccessDataResult<List<Car>>(_carDal.GetAll());
        }

        public IDataResult<List<CarDetailDto>> GetBrandId(int id)
        {
            return new SuccessDataResult<List<CarDetailDto>>(_carDal.GetCarDetails(c => c.BrandId == id));
        }

        public IDataResult<Car> GetById(int id)
        {
            return new SuccessDataResult<Car>(_carDal.Get(c => c.Id == id));
        }

        public IDataResult<List<CarDetailDto>> GetCarDetails()
        {
            return new SuccessDataResult<List<CarDetailDto>>(_carDal.GetCarDetails());
        }

        public IDataResult<List<CarDetailDto>> GetColorId(int id)
        {
            return new SuccessDataResult<List<CarDetailDto>>(_carDal.GetCarDetails(c => c.ColorId == id));
        }

        public IResult Update(Car car)
        {
            var result = BusinessRules.Run(CheckIfCarNameExısts(car.Name));
            if (result != null)
            {
                return result;
            }
            _carDal.Update(car);
            return new SuccessResult();
        }

        private IResult CheckIfCarNameExısts(string carName)
        {
            var result = _carDal.GetAll(c => c.Name == carName).Any();
            if (result) 
            {
                return new ErrorResult(Messages.CarNameAlready);
            }
            return new SuccessResult(); 
        }

        private IResult CheckIfBrandLimitExceded()
        {
            var result = _brandServices.GetAll().Data.Count();
            if (result > 10)
            {
                return new ErrorResult(Messages.BrandLimitExceded);
            }
            return new SuccessResult();

        }

    }
}
