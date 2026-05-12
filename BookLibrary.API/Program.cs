using BookLibrary.API.Services;
using BookLibrary.Application.Dtos.BookDtos;
using BookLibrary.Application.Services;
using BookLibrary.Application.Validations;
using BookLibrary.Infrastructure.Extensions;
using FluentValidation;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Text;


var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddInfrastructure(builder.Configuration);
builder.Services.AddApplication();
builder.Services.AddAPI(builder.Configuration);
builder.Services.AddScoped<IValidator<CreateBookDto>, CreateBookValidator>();
builder.Services.AddScoped<BookService>();
builder.Services.AddScoped<TokenService>();
builder.Services.AddCors(options => {
    options.AddPolicy("AllowAll", policy =>
    {
        policy.AllowAnyHeader()
        .AllowAnyOrigin()
        .AllowAnyMethod();

    });

});
builder.Services.AddHttpClient();
builder.Services.AddScoped<GoogleBooksService>();
var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI(c =>
{
    c.RoutePrefix = string.Empty;
    c.SwaggerEndpoint("swagger/v1/swagger.json", "BookLibrary API V1");

});

app.UseHttpsRedirection();
app.UseAuthentication();
app.UseRouting();
app.UseAuthorization();

app.MapControllers();
app.UseCors("AllowAll");
app.Run();
