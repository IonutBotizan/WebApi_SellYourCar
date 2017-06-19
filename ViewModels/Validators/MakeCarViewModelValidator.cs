using FluentValidation;
using System;
namespace SellYourCar.ViewModels
{
    public class MakeCarViewModelValidator : AbstractValidator<MakeCarViewModel>
    {
        public MakeCarViewModelValidator()
        {
            RuleFor(makeCar=> makeCar.Name).NotEmpty().WithMessage("Name cannot be empty");
        }   
    }
}
