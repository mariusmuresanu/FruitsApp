using FluentValidation;
using FruitsApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FruitsApp.ModelValidators
{
    public class FruitValidator : AbstractValidator<Fruit>
	{
		public FruitValidator()
		{
			RuleFor(x => x.MarketPrice).InclusiveBetween(5, 1000);
			RuleFor(x => x.DateAdded).LessThan(DateTime.Now);
			RuleFor(x => x.FruitUpkeepDifficulty)
				.Equal(FruitUpkeepDifficulty.Easy)
				.When(x => x.MarketPrice >= 5 && x.MarketPrice <= 10);

		}
		
	}
}
