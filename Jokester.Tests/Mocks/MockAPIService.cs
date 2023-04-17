using Jokester.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jokester.Tests.Mocks
{
    internal class MockAPIService : IAPIService
    {
        public string RequestReturnValue { get; set; }
        public MockAPIService()
        {
            RequestReturnValue = string.Empty;
        }

        public Task<string> MakeAPIRequest(string reqURL)
        {
            return Task.FromResult(RequestReturnValue);
        }
    }
}
