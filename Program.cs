using Microsoft.EntityFrameworkCore;
using AutoMapper;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Text;
using Chinese_Sale.Middleware;
using Microsoft.OpenApi.Models;
using System.Collections.Immutable;
using Chinese_Sale.Models;
using Chinese_Sale.DAL;
using Chinese_Sale.BL;
using webapi;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddDbContext<SaleContext>(option => option.UseSqlServer(builder.Configuration.GetConnectionString("SaleContext")));
builder.Services.AddScoped<IPresentDal, PresentDal>();
builder.Services.AddScoped<IPresentService, PresentService>();
builder.Services.AddScoped<IDonorService, DonorService>();
builder.Services.AddScoped<IDonorDal, DonorDal>();
builder.Services.AddScoped<IUserDal, UserDal>();
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IOrderService, OrderService>();
builder.Services.AddScoped<IOrderDal, OrderDal>();
builder.Services.AddScoped<IPresentsOrderService, PresentsOrderService>();
builder.Services.AddScoped<IPresentsOrderDal, PresentsOrderDal>();
builder.Services.AddScoped<IRaffleService, RaffleService>();
builder.Services.AddScoped<IRaffleDal, RaffleDal>();
// Add services to the container.
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddCors(options =>//לאיזה דומיין - פורט יאפשר לגשת
{
    options.AddPolicy("CorsPolicy",
        builder =>
        {
            builder.WithOrigins("http://localhost:4200",
                "development web site").AllowAnyHeader().AllowAnyMethod();
        });
});

builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "Your API", Version = "v1" });
    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Description = "JWT Authorization header using the Bearer scheme",
        Type = SecuritySchemeType.Http,
        Scheme = "bearer"
    });
    c.AddSecurityRequirement(new OpenApiSecurityRequirement
            {
                {
                    new OpenApiSecurityScheme
                    {
                        Reference = new OpenApiReference
                        {
                            Type = ReferenceType.SecurityScheme,
                            Id = "Bearer"
                        }
                    },
                    Array.Empty<string>()
                }
            });

    c.OperationFilter<SecurityRequirementsOperationFilter>();
});

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
   .AddJwtBearer(options =>
   {
       options.TokenValidationParameters = new TokenValidationParameters
       {
           ValidateIssuer = true,
           ValidateAudience = true,
           ValidateLifetime = true,
           ValidateIssuerSigningKey = true,
           ValidIssuer = builder.Configuration["Jwt:Issuer"],
           ValidAudience = builder.Configuration["Jwt:Audience"],
           IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]))
       };
   });
var app = builder.Build();
app.UseAuthentication();
app.UseAuthorization();
app.UseWhen(context => !context.Request.Path.StartsWithSegments("/api/User/Login")&& !context.Request.Path.StartsWithSegments("/api/User/Register"), orderApp =>
{
    orderApp.Use(async (context, next) =>
    {
        if (context.Request.Headers.ContainsKey("Authorization"))
        {
            var authorizationHeader = context.Request.Headers["Authorization"].ToString();
            if (authorizationHeader.StartsWith("Bearer "))
            {
                context.Request.Headers["Authorization"] = authorizationHeader.Substring("Bearer ".Length);
            }
        }

        await next();
    });
    orderApp.UseMiddleware<AuthenticationMiddleware>();
});
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}



//app.UseMvc();

app.UseHttpsRedirection();



app.MapControllers();

app.UseCors("CorsPolicy");

app.Run();
