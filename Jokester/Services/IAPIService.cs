
namespace Jokester.Services
{
    public interface IAPIService
    {
        Task<string> MakeAPIRequest(string reqURL);
    }

    public class APIService: IAPIService
    {
        private readonly HttpClient client;

        public APIService(HttpClient client)
        {
            this.client = client;
        }

        public async Task<string> MakeAPIRequest(string reqURL)
        {
            try
            {
                return await client.GetStringAsync(reqURL);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
           
            return null;
        }
    }

    public class MockAPIRequest : IAPIService
    {
        public string Result { get; set; }

        public Task<string> MakeAPIRequest(string reqURL)
        {
            return Task.FromResult(Result);
        }
    }
}
