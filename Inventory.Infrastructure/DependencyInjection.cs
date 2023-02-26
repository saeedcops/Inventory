using Inventory.Core.Interfaces;
using Inventory.Infrastructure.Implementation;
using Inventory.Web.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Inventory.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection serviceDescriptors, IConfiguration conf)
        {

          serviceDescriptors.AddScoped<IITemRepository, ItemRepository>();


            serviceDescriptors.AddDbContext<ApplicationDbContext>(x =>
            {
                x.UseSqlServer(conf.GetConnectionString("DefaultConnection"));
            });



            return serviceDescriptors;
        }
    }
}
