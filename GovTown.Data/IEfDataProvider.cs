using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using GovTown.Core.Data;

namespace GovTown.Data
{
    public interface IEfDataProvider : IDataProvider
    {
        /// <summary>
        /// Get connection factory
        /// </summary>
        /// <returns>Connection factory</returns>
        IDbConnectionFactory GetConnectionFactory();

    }
}
