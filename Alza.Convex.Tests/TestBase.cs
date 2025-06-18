using Alza.Convex.Logic.DIContainer;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace Alza.Convex.Tests
{
    public class TestBase
    {
        protected IServiceProvider ServiceProvider { get; }
        protected ILogger<TestBase> Logger => ServiceProvider.GetRequiredService<ILogger<TestBase>>();

        public TestBase()
        {
            ServiceProvider = DIContainer.GetServiceProvider();
        }
    }
}
