using System.Reflection;

namespace Mediator.Mediator
{
    public class MediatorConfiguration
    {
        internal IList<Assembly> Assemblies { get; } = [];

        public void RegisterServicesFromAssembly(Assembly assembly)
        {
            Assemblies.Add(assembly);
        }
    }
}
