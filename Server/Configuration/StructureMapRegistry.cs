using System.Diagnostics;
using Server.Log;
using Shared.Contracts;
using StructureMap;
using StructureMap.Configuration.DSL;

namespace Server.Configuration
{
    public class ServerRegistry : Registry
    {
        private static bool _structureMapConfigured;
        private static readonly object Mutex = new object();

        public ServerRegistry()
        {
            For(typeof (IConfiguration)).Singleton().Use(typeof (Configuration)).OnCreation<IConfiguration>(o => o.Initialize());
            For(typeof (ILogger)).LifecycleIs(new WcfOperationLifecycle()).Use(typeof (Logger));
        }

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
