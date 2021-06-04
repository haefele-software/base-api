using System.Linq;
using Microsoft.EntityFrameworkCore;
using ProjectName.Application.Domain.Common;

namespace ProjectName.Application.Persistence.Extensions
{
    /// <summary>
    /// https://stackoverflow.com/questions/37493095/entity-framework-core-rc2-table-name-pluralization
    /// </summary>
    public static class ModelBuilderExtensions
    {
        public static void RemovePluralizingTableNameConvention(this ModelBuilder modelBuilder) =>
            // do not use plural form for table names
            modelBuilder.Model.GetEntityTypes()
                .Where(x => !x.ClrType.IsSubclassOf(typeof(ValueObject))).ToList().ForEach(e => e.SetTableName(e.DisplayName()));
    }
}
