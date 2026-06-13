using Data.Persistence;
using Data.Repositories;
using Domain;
using Domain.Abstractions;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace Data
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddData(this IServiceCollection services,
            string connectionString)

        {
            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(connectionString));

            services.AddScoped<IRepository<personEntity, Guid>, PersonRepository>();
            services.AddScoped<ICodeRepository<personEntity>, PersonRepository>();

            services.AddScoped<IRepository<VisitEntity, Guid>, VisitRepository>();
            services.AddScoped<IVisitRepository<VisitEntity>, VisitRepository>();

            return services;
        }
    }
}
