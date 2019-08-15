using System;
using Microsoft.Extensions.DependencyInjection;
using NameSorter.BusinessServices;

namespace NameSorter
{
    class Program
    {
        static void Main(string[] args)
        {            
            var collection = new ServiceCollection();
            collection.AddScoped<INameSorter, NameSorterServices>();
            var serviceProvider = collection.BuildServiceProvider();
            var service = serviceProvider.GetService<INameSorter>();

            if(args.Length > 0) 
            {                
                service.Sort(args[0]);
            }
            else
            {
                service.Sort(".\\unsorted-names-list.txt");
            }
            
            serviceProvider.Dispose();
        }
    }
}
