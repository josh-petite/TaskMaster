using StructureMap.Pipeline;

namespace Server.Configuration
{
	public class WcfOperationLifecycle : ILifecycle
	{
	    public void EjectAll()
	    {
	        throw new System.NotImplementedException();
	    }

	    public IObjectCache FindCache()
	    {
	        throw new System.NotImplementedException();
	    }

	    public string Scope { get; private set; }
	}
}