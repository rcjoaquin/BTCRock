using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace BTCRock.bitcoin
{
    public class HttpClient
    {
        private WebClient client;
        private string endPoint;
        private int port;
        private string user;
        private string password;

        public HttpClient(String endPoint, int port, string user, string password)
        {
            client = new WebClient();

            this.endPoint = endPoint;
            this.port = port;
            this.user = user;
            this.password = password;

            client.Credentials = new NetworkCredential(this.user, this.password); //doesnt work

            //So use THIS instead to send credentials RIGHT AWAY

            string credentials = Convert.ToBase64String(Encoding.ASCII.GetBytes(this.user + ":" + this.password));
            client.Headers[HttpRequestHeader.Authorization] = string.Format("Basic {0}", credentials);
        }

        public HttpClient() 
        {
            client = new WebClient();
        }

        public string Post(string payload) 
        {  
            
            using (client)
            {
                client.Headers[HttpRequestHeader.ContentType] = "application/x-www-form-urlencoded";
                string HtmlResult = client.UploadString(endPoint+":"+port, payload);

                return HtmlResult;
            }

        }
    }
}
