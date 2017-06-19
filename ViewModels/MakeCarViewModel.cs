using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using SellYourCar.Entities;

namespace SellYourCar.ViewModels
{
    public class MakeCarViewModel : IValidatableObject
    {
        public string Url {get; set; }
        public string Name {get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            var validator = new MakeCarViewModelValidator();
        var result = validator.Validate(this);
        return result.Errors.Select(item => new ValidationResult(item.ErrorMessage, new[] { item.PropertyName }));
        }
    }

}