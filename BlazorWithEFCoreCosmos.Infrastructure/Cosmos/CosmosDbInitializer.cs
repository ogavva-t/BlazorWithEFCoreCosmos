using System.Diagnostics;
using System.Linq;
using BlazorWithEFCoreCosmos.Entity;

namespace BlazorWithEFCoreCosmos.Infrastructure.Cosmos
{
    public static class CosmosDbInitializer
    {
        public static void Initialize(CosmosDbContext context)
        {
            // context.Database.EnsureDeleted();
            context.Database.EnsureCreated();

            Debug.WriteLine("CosmosDbInitializer");

            if (context.ToDo.Count() != 0)
            {
                return;
            }

            var t1 = new ToDo
            {
                TaskName = "Task1"
            };
            context.ToDo.Add(t1);
            context.SaveChanges();
        }
    }
}
