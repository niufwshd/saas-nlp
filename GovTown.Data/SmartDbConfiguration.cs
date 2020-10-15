using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Core.Common;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.Infrastructure.DependencyResolution;
using System.Linq;
using EFCache;
using GovTown.Core.Data;
using GovTown.Core.Infrastructure;
using GovTown.Data.Setup;
using GovTown.Data.Caching;

namespace GovTown.Data
{

	public class SmartDbConfiguration : DbConfiguration
	{
		public SmartDbConfiguration()
		{
			IEfDataProvider provider = null;
			try
			{
				provider = (new EfDataProviderFactory(DataSettings.Current).LoadDataProvider()) as IEfDataProvider;
			}
			catch { /* GovTown is not installed yet! */ }

			if (provider != null)
			{
				base.SetDefaultConnectionFactory(provider.GetConnectionFactory());

				// prepare EntityFramework 2nd level cache
				ICache cache = null;
				try
				{
					var innerCache = EngineContext.Current.Resolve<Func<Type, GovTown.Core.Caching.ICache>>();
					cache = new EfCacheImpl(innerCache(typeof(GovTown.Core.Caching.StaticCache)));
				}
				catch
				{
					cache = new InMemoryCache();
				}

				var transactionHandler = new CacheTransactionHandler(cache);
				AddInterceptor(transactionHandler);

				Loaded +=
				  (sender, args) => args.ReplaceService<DbProviderServices>(
					(s, _) => new CachingProviderServices(s, transactionHandler,
					  new EfCachingPolicy()));
			}
		}
	}

}
