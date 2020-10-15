using System;

namespace GovTown.Data.Setup
{
	
	public interface ILocaleResourcesProvider
	{
		void MigrateLocaleResources(LocaleResourcesBuilder builder);
	}

}
