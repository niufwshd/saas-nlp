using System;
using GovTown.Core;
using GovTown.Core.Data;

namespace GovTown.Data
{
    public partial class EfDataProviderFactory : DataProviderFactory
    {
		public EfDataProviderFactory()
			: this(DataSettings.Current)
		{
		}
		
		public EfDataProviderFactory(DataSettings settings)
			: base(settings)
        {
        }

        public override IDataProvider LoadDataProvider()
        {
            var providerName = Settings.DataProvider;
			if (providerName.IsEmpty())
			{
				throw new SmartException("Data Settings doesn't contain a providerName");
			}

            switch (providerName.ToLowerInvariant())
            {
                case "sqlserver":
                    return new SqlServerDataProvider();
               default:
                    throw new SmartException(string.Format("Unsupported dataprovider name: {0}", providerName));
            }
        }

    }
}
