using GovTown.Core.Infrastructure.DependencyManagement;
using System;

namespace GovTown.Core.Infrastructure
{
    /// <summary>
    /// Classes implementing this interface can serve as a portal for the 
    /// various services composing the GovTown engine. Edit functionality, modules
    /// and implementations access most GovTown functionality through this 
    /// interface.
    /// </summary>
    public interface IEngine
    {
        ContainerManager ContainerManager { get; }
        
        /// <summary>
        /// Initialize components and plugins in the GovTown environment.
        /// </summary>
        /// <param name="config">Config</param>
        void Initialize();

        T Resolve<T>(string name = null) where T : class;

        object Resolve(Type type, string name = null);

        T[] ResolveAll<T>();
    }
}
