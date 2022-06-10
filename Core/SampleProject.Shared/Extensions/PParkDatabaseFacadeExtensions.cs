using Dapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Npgsql;
using SampleProject.Shared.Helpers;

namespace SampleProject.Shared.Extensions
{
    public static class SampleProjectDatabaseFacadeExtensions
    {
        public static async Task MigrateScriptsAsync(this DatabaseFacade databaseFacade, string assemblyName, CancellationToken cancellationToken = default)
        {
            if (string.IsNullOrWhiteSpace(assemblyName)) ArgumentNullException.ThrowIfNull(assemblyName);

            await databaseFacade.MigrateAsync(cancellationToken).ConfigureAwait(false);
            var allResources = ReflectionHelper.LoadEmbeddedResources(assemblyName, "MigrationScripts");
            await using var connection = new NpgsqlConnection(databaseFacade.GetConnectionString());
            foreach (var allResource in allResources)
            {
                await connection.ExecuteAsync(allResource);
            }
        }
    }
}
