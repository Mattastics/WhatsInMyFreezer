using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentValidation;
using freezer.DAL.Migrations;

namespace freezer.Validators
{
    internal class FoodItemValidator : AbstractValidator<FoodItem>
    {
        public FoodItemValidator()
        { 
            RuleFor(x => x.Name).NotEmpty();
            RuleFor(x => x.Quantity).GreaterThanOrEqualTo(1);
        }
    }
}
