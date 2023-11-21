using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace JwtAuthenticationManager
{
    public static class CustomJwtAuthExtension
    {
        public static void AddCustomJwtAuthentication(this IServiceCollection services)
        {
            var authenticationProviderKey = "Bearer";
            services.AddAuthentication(a =>
            {
                a.DefaultAuthenticateScheme =JwtBearerDefaults.AuthenticationScheme;
                a.DefaultChallengeScheme =JwtBearerDefaults.AuthenticationScheme;

            }).AddJwtBearer(authenticationProviderKey,a =>
            {
                a.RequireHttpsMetadata =false;
                a.SaveToken =true;
                a.TokenValidationParameters=new Microsoft.IdentityModel.Tokens.TokenValidationParameters
                {
                    ValidateActor = true,
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    RequireExpirationTime = true,
                    ValidateIssuerSigningKey = true,
                    //ValidIssuer = issuer,
                   // ValidAudience = audience,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(JwtTokenHandler.JWT_TOKEN_KEY))
                };
            });
        }
    }
}
