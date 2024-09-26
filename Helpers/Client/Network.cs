using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;

namespace TISB.Helpers.Client {
    internal class Network {
        public static string POST(string url, string data, string userAgent = "application/t-isb") {
            try {
                byte[] dataBytes = Encoding.UTF8.GetBytes(data);

                using (var client = new HttpClient())
                using (var content = new ByteArrayContent(dataBytes)) {
                    content.Headers.ContentType = new MediaTypeHeaderValue("application/x-www-form-urlencoded");

                    client.DefaultRequestHeaders.UserAgent.ParseAdd(userAgent);

                    HttpResponseMessage response = client.PostAsync(url, content).Result;

                    response.EnsureSuccessStatusCode();

                    return response.Content.ReadAsStringAsync().Result;
                }
            }
            catch (HttpRequestException e) {
                return e.Message;
            }
            catch (Exception ex) {
                return ex.Message;
            }
        }
    }
}
