using BookLibrary.Domain;
using BookLibrary.Infrastructure.Persistence;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookLibrary.Infrastructure.Extensions;

public static class InfrastructureService
{
    public static void AddInfrastructure(this IServiceCollection services,IConfiguration configuration)
    {

        services.AddDbContext<AppDbContext>(options => {
            options.UseSqlServer(configuration.GetConnectionString("DefaultConnection"));
        
        });
        services.AddIdentity<AppUser, AppRole>(options => {
            options.User.RequireUniqueEmail = true;

            options.Password.RequiredLength = 6;

        }).AddEntityFrameworkStores<AppDbContext>()
            .AddDefaultTokenProviders();
    
    }
}
