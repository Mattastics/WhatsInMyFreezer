using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentValidation;
using freezer.DAL.Entities;

namespace freezer.Validators
{
    internal class FoodItemValidator : AbstractValidator<FoodItem>
    {
        public FoodItemValidator()
        { 
            RuleFor(x=> x.UPC).NotEmpty();
            RuleFor(x => x.Quantity).GreaterThanOrEqualTo(1);
        }
    }
}
