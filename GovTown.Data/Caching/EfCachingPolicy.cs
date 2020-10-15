using EFCache;
using GovTown.Core.Domain.Themes;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.Entity.Core.Metadata.Edm;
using System.Linq;

namespace GovTown.Data.Caching
{

    /* TODO: (mc)
	 * ========================
	 *		1. Let developers register custom caching policies for single entities (from plugins)
	 *		2. Caching policies should contain expiration info and cacheable rows count
	 *		3. Backend: Let users decide which entities to cache
	 *		4. Backend: Let users purge the cache
	 */

    internal class EfCachingPolicy : CachingPolicy
	{
		private static readonly HashSet<string> _cacheableSets = new HashSet<string>
			{
				typeof(ThemeVariable).Name,
				//typeof(Topic).Name
			};

		protected override bool CanBeCached(ReadOnlyCollection<EntitySetBase> affectedEntitySets, string sql, IEnumerable<KeyValuePair<string, object>> parameters)
		{
			var entitySets = affectedEntitySets.Select(x => x.Name);
			var result = entitySets.Any(x => _cacheableSets.Contains(x));
			return result;
		}

		protected override void GetExpirationTimeout(ReadOnlyCollection<EntitySetBase> affectedEntitySets, out TimeSpan slidingExpiration, out DateTimeOffset absoluteExpiration)
		{
			base.GetExpirationTimeout(affectedEntitySets, out slidingExpiration, out absoluteExpiration);
			absoluteExpiration = DateTimeOffset.Now.AddHours(24);
		}

		protected override void GetCacheableRows(ReadOnlyCollection<EntitySetBase> affectedEntitySets, out int minCacheableRows, out int maxCacheableRows)
		{
			base.GetCacheableRows(affectedEntitySets, out minCacheableRows, out maxCacheableRows);
		}
	}
}
