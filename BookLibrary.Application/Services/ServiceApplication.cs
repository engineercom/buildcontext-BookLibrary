using BookLibrary.Application.Mapping;
using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace BookLibrary.Application.Services
{
    public static class ServiceApplication
    {
        public static void AddApplication(this IServiceCollection services)
        {
            services.AddAutoMapper(options=>options.AddProfile<AutoMapping>());
            services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());
        }
    }
}
