using System.Text;
using AuthAPI.DataBase;
using AuthAPI.Repositories;
using AuthAPI.Service;
using dotenv.net;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

namespace AuthAPI.Config
{
    public static class AppConfig
    {
        public static void StartDependencies(WebApplicationBuilder builder)
        {
            var envVars = DotEnv.Read();
            string connectionString = envVars["DATABASECONNECTION"];
            string SecretKey = envVars["SECRETKEY"];

            try
            {
                builder.Services.AddDbContext<AppDbContext>(options =>
                options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString)));

                builder.Services.AddControllers();

                builder.Services.AddScoped<UserService, UserService>();
                builder.Services.AddScoped<UserRepository, UserRepository>();
                builder.Services.AddScoped<SessionRepository, SessionRepository>();


                // Configuração de CORS
                builder.Services.AddCors(options =>
                {
                    options.AddDefaultPolicy(policy =>
                        policy.AllowAnyOrigin()
                            .AllowAnyMethod()
                            .AllowAnyHeader());
                });

                // Configuração da autenticação JWT
                var key = Encoding.ASCII.GetBytes(SecretKey);
                builder.Services.AddAuthentication(options =>
                {
                    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                })
                .AddJwtBearer(options =>
                {
                    options.RequireHttpsMetadata = false;
                    options.SaveToken = true;
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = new SymmetricSecurityKey(key),
                        ValidateIssuer = false,
                        ValidateAudience = false
                    };
                });
            }
            catch (Exception ex)
            {
                throw new Exception($"Erro ao iniciar as dependencias: {ex.Message}", ex);
            }
        }
    }
}