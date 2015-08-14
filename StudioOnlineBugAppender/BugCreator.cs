using System;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;

namespace StudioOnlineBugAppender
{
    public class BugCreator : IBugCreator
    {
        private readonly string _requestUrl;
        private readonly string _password;
        private readonly string _username;

        public bool UseProxy { get; set; }
        public Uri Proxy { get; set; }
        
        private BugCreator(string account, string project)
        {
            this.UseProxy = false;
            this._requestUrl = "https://" + account + ".visualstudio.com/defaultcollection/" + project + "/_apis/wit/workitems/$Bug?api-version=1.0";
        }

        //Authenticate in visual studio with token
        public BugCreator(string account, string project, string token) : this(account, project)
        {
            this._password = token;
            this._username = account;
        }
        
        public BugCreator(string account, string project, string username, string password) : this(account,project,password)
        {
            this._username = username;
        }

        public HttpContent Response { get; private set; }

        public void SendBug(IBug bug)
        {
            HttpClient httpClient = this.GetHttpClient();
            httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic",
                                                                                           Convert.ToBase64String(
                                                                                               Encoding.ASCII.GetBytes(
                                                                                               string.Format("{0}:{1}", this._username, this._password))));
            var method = new HttpMethod("PATCH");
            var request = new HttpRequestMessage(method, this._requestUrl)
            {
                Content = new StringContent(bug.Deserialize(), Encoding.UTF8, "application/json-patch+json")
            };
            HttpResponseMessage hrm = httpClient.SendAsync(request).Result;
            Response = hrm.Content;
        }

        private HttpClient GetHttpClient()
        {
            HttpClient httpClient;
            if (this.UseProxy)
            {
                HttpClientHandler httpClientHandler = new HttpClientHandler { Proxy = this.GetProxy(), UseProxy = true, UseDefaultCredentials = true };
                httpClient = new HttpClient(httpClientHandler);
            }
            else
            {
                httpClient=new HttpClient();
            }
            return httpClient;
        }

        private WebProxy GetProxy()
        {
            return new WebProxy {Address = this.Proxy, UseDefaultCredentials = true};
        }

    }
}
