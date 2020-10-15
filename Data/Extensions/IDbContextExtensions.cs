using System;
using System.Linq;
using GovTown.Core;
using GovTown.Core.Data;
using Microsoft.EntityFrameworkCore;

namespace GovTown.Data
{

    public static class IDbContextExtensions 
	{
        
		/// <summary>
		/// Executes the <c>DBCC SHRINKDATABASE(0)</c> command against the SQL Server (Express) database
		/// </summary>
		/// <param name="context">The context</param>
		/// <returns><c>true</c>, when the operation completed successfully.</returns>
		public static bool ShrinkDatabase(this IDbContext context)
		{
			if (DataSettings.Current.IsSqlServer)
			{
				try
				{
					context.ExecuteSqlCommand("DBCC SHRINKDATABASE(0)", true);
					return true;
				}
				catch { }
			}

			return false;
		}
    }
}