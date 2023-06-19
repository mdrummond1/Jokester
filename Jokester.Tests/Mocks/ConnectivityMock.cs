using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jokester.Tests.Mocks
{
    internal class ConnectivityMock : IConnectivity
    {
        public IEnumerable<ConnectionProfile> ConnectionProfiles => throw new NotImplementedException();

        public NetworkAccess NetworkAccess { get; set; }

        public event EventHandler<ConnectivityChangedEventArgs>? ConnectivityChanged;

        
    }
}
