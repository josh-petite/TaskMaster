using System.Diagnostics;
using Shared.Contracts;
using StructureMap;
using StructureMap.Configuration.DSL;

namespace Web.Configuration
{
	public class Bootstrapper : Registry
    {
        private static bool _structureMapConfigured;
        private static readonly object Mutex = new object();

        public static void Configure()
        {
            if (!_structureMapConfigured)
                lock (Mutex)
                    if (!_structureMapConfigured)
                    {
                        var callingType = new StackTrace().GetFrame(1).GetMethod().DeclaringType;

                        ObjectFactory.Configure(o => o.Scan(s =>
                        {
                            s.WithDefaultConventions();
                            s.AssemblyContainingType(callingType);
                            s.AssemblyContainingType(typeof(TaskDro));
                            s.LookForRegistries();
                        }));

                        _structureMapConfigured = true;
                    }
        }
    }
}