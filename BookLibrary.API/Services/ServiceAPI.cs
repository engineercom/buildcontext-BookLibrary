using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Security.Claims;
using System.Text;

namespace BookLibrary.API.Services;

public static class ServiceAPI
{
    public static void AddAPI(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddSwaggerGen(options =>
        {
            //options.SwaggerDoc("v1", new OpenApiInfo
            //{
            //    Title = "BlookLibrary API",
            //    Version = "v1"

            //});
            //1-ekle
            var jwtSecurityScheme = new OpenApiSecurityScheme
            {
                BearerFormat = "Jwt",
                Name = "Jwt Authentication",
                In = ParameterLocation.Header,
                Type = SecuritySchemeType.Http,
                Scheme = JwtBearerDefaults.AuthenticationScheme,
                Description = "Buraya sadece token değerini yapıştırın(bearer yazmanıza gerek yok)",
                Reference = new OpenApiReference
                {

                    Id = JwtBearerDefaults.AuthenticationScheme,
                    Type = ReferenceType.SecurityScheme

                }

            };
            //options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
            //{

            //    Description = "Jwt Authorization Header Kullanımı ",
            //    Name = "Authorization",
            //    In = ParameterLocation.Header,
            //    Type = SecuritySchemeType.Http,
            //    Scheme = "bearer",
            //    BearerFormat = "JWT"

            //});

            //2
            options.AddSecurityDefinition(jwtSecurityScheme.Reference.Id, jwtSecurityScheme);
            //            options.AddSecurityRequirement(new OpenApiSecurityRequirement
            //            {
            //    {
            //    new OpenApiSecurityScheme{

            //    Reference=new OpenApiReference{

            //    Type=ReferenceType.SecurityScheme,
            //    Id="Bearer"
            //    }

            //    },

            //    new string[]{}
            //    }

            //});
            //3
            options.AddSecurityRequirement(new OpenApiSecurityRequirement
    {

        { jwtSecurityScheme,Array.Empty<string>()}

    });

        });

        services.AddAuthentication(options =>
        {
            //4
            options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
            options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            //5
        }).AddJwtBearer(options =>
        {
            options.TokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuer = true,
                ValidateAudience = true,
                ValidateLifetime = true,
                ValidateIssuerSigningKey = true,

                ValidIssuer = configuration["Jwt:Issuer"],
                ValidAudience = configuration["Jwt:Audience"],
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["Jwt:Key"]!)),
                //NameClaimType = ClaimTypes.Name,
                //RoleClaimType = ClaimTypes.Role


            };


        });
    }
}
