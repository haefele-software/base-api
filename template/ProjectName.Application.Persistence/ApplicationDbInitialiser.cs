using System.Linq;
using System.Threading.Tasks;
using ProjectName.Application.Domain.Entities;

namespace ProjectName.Application.Persistence
{
    public class ApplicationDbInitialiser
    {
        public static async Task Initialize(ApplicationDbContext context)
        {
            await SeedEverything(context).ConfigureAwait(false);
        }


        public static async Task SeedEverything(ApplicationDbContext context)
        {
            context.Database.EnsureCreated();

            await SeedSampleDataAsync(context).ConfigureAwait(false);
        }


        private static async Task SeedSampleDataAsync(ApplicationDbContext context)
        {

            // Seed, if necessary
            if (!context.TodoLists.Any())
            {
                context.TodoLists.Add(new TodoList
                {
                    Title = "Shopping",
                    Items =
                    {
                        new TodoItem { Title = "Apples", Done = true },
                        new TodoItem { Title = "Milk", Done = true },
                        new TodoItem { Title = "Bread", Done = true },
                        new TodoItem { Title = "Toilet paper" },
                        new TodoItem { Title = "Pasta" },
                        new TodoItem { Title = "Tissues" },
                        new TodoItem { Title = "Tuna" },
                        new TodoItem { Title = "Water" }
                    }
                });

                await context.SaveChangesAsync().ConfigureAwait(false);
            }
        }
    }
}
