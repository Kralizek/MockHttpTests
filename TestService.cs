using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace MockHttpTests
{
    public interface IService
    {
        Task<string> GetString(Uri uri);
    }

    public class TestService : IService
    {
        private readonly HttpClient _http;

        public TestService (HttpClient http)
        {
            _http = http ?? throw new ArgumentNullException(nameof(http));
        }

        public Task<string> GetString(Uri uri)
        {
            return _http.GetStringAsync(uri);
        }
    }
}