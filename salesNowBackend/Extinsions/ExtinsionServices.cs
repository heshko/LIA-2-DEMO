using salesNowBackend.Contracts;
using salesNowBackend.FirestoreRepository;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using salesNowBackend.Logger;

namespace salesNowBackend.Extinsions
{
    public static class ExtinsionServices
    {
        
        public static void ConfigureCors(this IServiceCollection service)
        {
            service.AddCors(option =>
            {
                option.AddPolicy("CorsPolicy", option =>
                {
                    option.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod();

                });
            });
        }

        public static void ConfigureFirestoreDb(this IServiceCollection service)
        {
            service.AddScoped<IFirestorRepositoryManager, FireStoreRepossitoryManager>();
        }

        public static void ConfigureLogger(this IServiceCollection service)
        {
            service.AddScoped<ILoggerManager, LoggerManager>();
        }
        public static void ConfigureCompany(this IServiceCollection service)
        {
            service.AddScoped<ICompanyFirestore, CompanyFirestore>();
        }
        public static void ConfigureActivity(this IServiceCollection service)
        {
            service.AddScoped<IActivityFirestore, ActivityFirestore>();
        }
        public static void ConfigureContactPerson(this IServiceCollection service)
        {
            service.AddScoped<IContactPersonFirestore, ContactPersonFirestore>();
         
        }
        public static void ConfigureBusinessOpportunity(this IServiceCollection service)
        {
            service.AddScoped<IBusinessOpportunityFirestore, BusinessOpportunityFirestore>();

        }

    }
}
