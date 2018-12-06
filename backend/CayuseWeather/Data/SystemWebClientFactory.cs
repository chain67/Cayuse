using System;
using System.Net;

namespace Data
{
    //WebClient is not injectable but a factory is
    public interface IWebClient : IDisposable
    {

        string DownloadString(string url);
    }

   public interface IWebClientFactory
    {
        IWebClient Create();
    }

    public class SystemWebClient : WebClient, IWebClient
    {

    }

    public class SystemWebClientFactory : IWebClientFactory
    {

        public IWebClient Create()
        {
            return new SystemWebClient();
        }

    }
}
