using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using DAL.Context;
using BLL.Services;
using BLL.Services.Interfaces;
using DAL.Repository;
using DAL.Repository.Interfaces;
using System.ComponentModel.Design;

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
            
            collection.AddTransient<AdministrationService>();
            collection.AddTransient<IAdministrationService,AdministrationService>();
            
            collection.AddTransient<PlaneService>();
            collection.AddTransient<IPlaneService,PlaneService>();

            collection.AddTransient<FlightService>();
            collection.AddTransient<IFlaightService,FlightService>();

            collection.AddTransient<UserService>();
            collection.AddTransient<IUserService, UserService>();

            collection.AddTransient<EmployeeService>();
            collection.AddTransient<IEmployeeService, EmployeeService>();
            

            //repositories
            collection.AddTransient<UserRepository>();
            collection.AddTransient<IUserRepository, UserRepository>();

            collection.AddTransient<EmployeeRepository>();
            collection.AddTransient<IEmployeeRepository, EmployeeRepository>();

            collection.AddTransient<PlaneRepository>();
            collection.AddTransient<IPlaneRepository, PlaneRepository>();

            collection.AddTransient<UserTokenRepository>();
            collection.AddTransient<IUserTokenRepository, UserTokenRepository>();
        }
    }
}
