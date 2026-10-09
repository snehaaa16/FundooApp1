using BuisnessLayer.Interface;
using BuisnessLayer.Service;
using RepositoryLayer.Context;
using RepositoryLayer.Interface;
using RepositoryLayer.Service;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace Fundoo1;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        // Add services to the container.

        builder.Services.AddControllers();
        builder.Services.AddScoped<IUserBL, UserBL>();
        builder.Services.AddScoped<IUserRL, UserRL>();

        builder.Services.AddScoped<IJwtService,JwtService>();

        builder.Services.AddScoped<IForgotPasswordBL, ForgotPasswordBL>();
        builder.Services.AddScoped<IForgotPasswordRL, ForgotPasswordRL>();

        builder.Services.AddScoped<IResetPasswordRL, ResetPasswordRL>();
        builder.Services.AddScoped<IResetPasswordBL, ResetPasswordBL>();

        builder.Services.AddDbContext<FundooContext>(options =>
            options.UseNpgsql(builder.Configuration.GetConnectionString("FundooConnection"))
        );

        builder.Services.AddHttpClient<IEmailClient, EmailClient>(client =>
        {
            client.BaseAddress = new Uri("http://localhost:5118/");
        });
        // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
        builder.Services.AddEndpointsApiExplorer();

        builder.Services
            .AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(options =>{
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = builder.Configuration["Jwt:Issuer"],
                    ValidAudience = builder.Configuration["Jwt:Audience"],
                    IssuerSigningKey = new SymmetricSecurityKey(
                        Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]!)),
                    ClockSkew = TimeSpan.Zero
                };
            });

        builder.Services.AddSwaggerGen(options =>
        {
            options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
            {
                Name = "Authorization",
                Type = SecuritySchemeType.Http,
                Scheme = "bearer",
                BearerFormat = "JWT",
                In = ParameterLocation.Header,
                Description = "Sirf token paste karo (Bearer likhna nahi hai)"
            });

            options.AddSecurityRequirement(new OpenApiSecurityRequirement
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
                    new string[] { }
                }
            });
        });

        var app = builder.Build();

        // Configure the HTTP request pipeline.
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        app.UseAuthentication();
        app.UseAuthorization();


        app.MapControllers();

        app.Run();
    }
}

