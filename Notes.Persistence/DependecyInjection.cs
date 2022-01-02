using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Notes.Application.Intarfaces;

namespace Notes.Persistence
{
    public static class DependecyInjection
    {
        public static IServiceCollection AddPersistence(this IServiceCollection services, IConfiguration configuration)
        {
            var connectionString = configuration["DbConnection"];
            services.AddDbContext<NotesDBContext>(options =>
            {
                options.UseSqlServer(connectionString);
            });

            services.AddScoped<INotesDBContext>(provider =>
                provider.GetService<NotesDBContext>());

            return services;
        }
    }
}
