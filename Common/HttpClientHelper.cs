using System.Text;

namespace EmployeeManagementSystem.Common
{
    public class HttpClientHelper
    {
        // Make a POST request to the specified endpoint with the given data
        public static async Task<string> MakePostRequest(string baseUrl, string endpoint, string apiRequestData)
        {
            // Configure socket handler settings for HTTP client
            var socketsHandler = new SocketsHttpHandler
            {
                PooledConnectionLifetime = TimeSpan.FromMinutes(10),
                PooledConnectionIdleTimeout = TimeSpan.FromMinutes(5),
                MaxConnectionsPerServer = 10,
            };

            // Create and configure the HTTP client
            using (HttpClient httpClient = new HttpClient(socketsHandler))
            {
                httpClient.Timeout = TimeSpan.FromMinutes(5);
                httpClient.BaseAddress = new Uri(baseUrl);

                // Prepare the request content with the specified data
                StringContent apiRequestContent = new StringContent(apiRequestData, Encoding.UTF8, "application/json");

                // Send the POST request
                var httpResponse = await httpClient.PostAsync(endpoint, apiRequestContent);

                // Read the response content as a string
                var httpResponseString = await httpResponse.Content.ReadAsStringAsync();

                // Throw an exception if the response status is not successful
                if (!httpResponse.IsSuccessStatusCode)
                {
                    throw new Exception(httpResponseString);
                }

                // Return the response content as a string
                return httpResponseString;
            }
        }

        // Make a GET request to the specified endpoint
        public static async Task<string> MakeGetRequest(string baseUrl, string endPoint)
        {
            // Configure socket handler settings for HTTP client
            var socketHandler = new SocketsHttpHandler
            {
                PooledConnectionIdleTimeout = TimeSpan.FromMinutes(10),
                PooledConnectionLifetime = TimeSpan.FromMinutes(5),
                MaxConnectionsPerServer = 10
            };

            // Create and configure the HTTP client
            using (var httpClient = new HttpClient(socketHandler))
            {
                httpClient.Timeout = TimeSpan.FromMinutes(5);
                httpClient.BaseAddress = new Uri(baseUrl);

                // Send the GET request
                var response = await httpClient.GetAsync(endPoint);

                // Read the response content as a string
                var responseString = await response.Content.ReadAsStringAsync();

                // Throw an exception if the response status is not successful
                if (!response.IsSuccessStatusCode)
                {
                    throw new Exception(responseString);
                }

                // Return the response content as a string
                return responseString;
            }
        }
    }
}
