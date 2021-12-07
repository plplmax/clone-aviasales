using clone_aviasales.Domain.Model;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.ModelBinding.Binders;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace clone_aviasales.Utils
{
    public class CustomTicketsFiltersModelBinderProvider : IModelBinderProvider
    {
        public IModelBinder GetBinder(ModelBinderProviderContext context)
        {
            ILoggerFactory loggerFactory = context.Services.GetRequiredService<ILoggerFactory>();
            IModelBinder binder = new CustomTicketsFiltersModelBinder(new SimpleTypeModelBinder(typeof(TicketsFilters), loggerFactory));
            return context.Metadata.ModelType == typeof(TicketsFilters) ? binder : null;
        }
    }
}
