using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Reflection;
using GovTown.Core.Data;
using GovTown.Data.Migrations;
using GovTown.Data.Setup;
using GovTown.Core.Domain.Directory;

namespace GovTown.Data
{
	
	/// <summary>
    /// Object context
    /// </summary>
	public class SmartObjectContext : ObjectContextBase
    {
        public DbSet<Country> Countries { get; set; } 

		static SmartObjectContext()
		{
			//var initializer = new MigrateDatabaseInitializer<SmartObjectContext, MigrationsConfiguration>
			//{
			//	TablesToCheck = new[] { "Country" }
			//};

			//Database.SetInitializer<SmartObjectContext>(initializer);
            
            
            Database.SetInitializer(new MigrateDatabaseToLatestVersion<SmartObjectContext, MigrationsConfiguration>());

        }

		/// <summary>
		/// For tooling support, e.g. EF Migrations
		/// </summary>
		public SmartObjectContext()
			: base()
		{
		}

		public SmartObjectContext(string nameOrConnectionString)
            : base(nameOrConnectionString)
        {
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            ////dynamically load all configuration
            ////System.Type configType = typeof(LanguageMap);   //any of your configuration classes here
            ////var typesToRegister = Assembly.GetAssembly(configType).GetTypes()
            //var typesToRegister = Assembly.GetExecutingAssembly().GetTypes()
            //    .Where(type => 
            //        !String.IsNullOrEmpty(type.Namespace)
            //        && type.BaseType != null 
            //        && type.BaseType.IsGenericType 
            //        && type.BaseType.GetGenericTypeDefinition() == typeof(EntityTypeConfiguration<>));

            var typesToRegister = from t in Assembly.GetExecutingAssembly().GetTypes()
                        where t.Namespace.HasValue() && 
                              t.BaseType != null &&
                              t.BaseType.IsGenericType
                        let genericType = t.BaseType.GetGenericTypeDefinition()
                        where genericType == typeof(EntityTypeConfiguration<>) || genericType == typeof(ComplexTypeConfiguration<>)
                        select t;

            foreach (var type in typesToRegister)
            {
                dynamic configurationInstance = Activator.CreateInstance(type);
                modelBuilder.Configurations.Add(configurationInstance);
            }
            //...or do it manually below. For example,
            //modelBuilder.Configurations.Add(new LanguageMap());

            base.OnModelCreating(modelBuilder);
        }

    }
}