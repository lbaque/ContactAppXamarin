using Microsoft.EntityFrameworkCore;

namespace Api.Contact.Context
{
    public static class MigrationManager
    {
        public static IHost MigrateDatabase<TContext>(this IHost host) where TContext : ApiContactContext
        {
            using (var scope = host.Services.CreateScope())
            {
                using (var appContext = scope.ServiceProvider.GetRequiredService<TContext>())
                {
                    try
                    {
                        appContext.Database.Migrate();

                    }
                    catch (Exception ex)
                    {
                        //Log errors or do anything you think it's needed
                        throw;
                    }
                }
            }
            return host;
        }
    }
}
