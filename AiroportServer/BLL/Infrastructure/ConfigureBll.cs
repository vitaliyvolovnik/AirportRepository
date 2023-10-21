using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using DAL.Context;
using BLL.Services;
using BLL.Services.Interfaces;
using DAL.Repository;
using DAL.Repository.Interfaces;

namespace BLL.Infrastructure
{
    public static class ConfigureBll
    {

        public static void Configure(IServiceCollection collection,string connectionString)
        {
            //dbcontext
            collection.AddDbContext<AirportContext>(x => x.UseSqlServer(connectionString));


            //services
            collection.AddTransient<AuthService>();
            collection.AddTransient<IAuthService,AuthService>();

            //repositories

            collection.AddTransient<UserRepository>();
            collection.AddTransient<IUserRepository, UserRepository>();
        }
    }
}
