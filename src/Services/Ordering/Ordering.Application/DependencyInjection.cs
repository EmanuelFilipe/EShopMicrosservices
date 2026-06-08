using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ordering.Application
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {
            // Register application services, e.g. MediatR handlers, AutoMapper profiles, etc.
            // services.AddMediatR(typeof(DependencyInjection).Assembly);
            // services.AddAutoMapper(typeof(DependencyInjection).Assembly);
            return services;
        }
    }
}
