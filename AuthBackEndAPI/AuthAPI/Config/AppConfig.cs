using System.Security.Cryptography.X509Certificates;
using System.Text;
using AuthAPI.DataBase;
using AuthAPI.Models;
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
                ConfigureDataBase(builder.Services, connectionString);
                ConfigureRepositories(builder.Services);
                CORSConfig.ConfigureCORS(builder.Services);
                JWTConfig.ConfigureJWT(builder.Services, SecretKey);

                builder.Services.AddControllers();

                // Add services to the container.
                // Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
                builder.Services.AddOpenApi();
            }
            catch (Exception ex)
            {
                throw new Exception($"Erro ao iniciar as dependencias: {ex.Message}", ex);
            }
        }

        public static void ConfigureDataBase(IServiceCollection services, string connectionString)
        {
            services.AddDbContext<AppDbContext>(options =>
                options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString)));
        }

        public static void ConfigureRepositories(IServiceCollection services)
        {
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<ISessionRepository, SessionRepository>();
            services.AddScoped<UserAuthService>();
            services.AddScoped<UserProfileService>();
        }
    }
}