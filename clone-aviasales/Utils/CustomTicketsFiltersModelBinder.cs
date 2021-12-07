using clone_aviasales.Domain.Model;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace clone_aviasales.Utils
{
    public class CustomTicketsFiltersModelBinder : IModelBinder
    {
        private readonly IModelBinder fallbackBinder;

        public CustomTicketsFiltersModelBinder(IModelBinder fallbackBinder)
        {
            this.fallbackBinder = fallbackBinder;
        }

        public Task BindModelAsync(ModelBindingContext bindingContext)
        {
            if (bindingContext == null) throw new ArgumentNullException(nameof(bindingContext));
            var transfersCountValue = bindingContext.ValueProvider.GetValue("filters[transfers_count]");
            var airlinesValue = bindingContext.ValueProvider.GetValue("filters[airlines]");
            var durationValue = bindingContext.ValueProvider.GetValue("filters[duration]");
            IList<byte> transfersCount = new List<byte>();
            IList<string> airlines = new List<string>();
            short duration = default;
            if (transfersCountValue == ValueProviderResult.None && airlinesValue == ValueProviderResult.None && durationValue == ValueProviderResult.None) return fallbackBinder.BindModelAsync(bindingContext);
            if (transfersCountValue != ValueProviderResult.None)
            {
                var transfersCountValueList = transfersCountValue.Values.ToList();
                transfersCountValueList.ForEach(count =>
                {
                    if (byte.TryParse(count, out byte result)) transfersCount.Add(result);
                });
                if (transfersCount.Count != transfersCountValueList.Count)
                {
                    bindingContext.ModelState.AddModelError("filters[transfers_count]", "The parameter takes values or an array of values: 0, 1, 2, 3, 4, 5");
                }
            }
            if (airlinesValue != ValueProviderResult.None)
            {
                airlines = airlinesValue.Values.ToList();
            }
            if (durationValue != ValueProviderResult.None && !short.TryParse((string)durationValue, out duration))
            {
                bindingContext.ModelState.AddModelError("filters[duration]", "The parameter is not an integer or too long");
            }
            bindingContext.Result = ModelBindingResult.Success(new TicketsFilters { Transfers = transfersCount, Airlines = airlines, DurationInHours = duration });
            return Task.CompletedTask;
        }
    }
}
