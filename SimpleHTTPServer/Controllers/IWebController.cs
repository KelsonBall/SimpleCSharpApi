using System;
using System.Net;

namespace HttpServer.Controllers
{    
    public interface IWebController
    {        
        void Get(ref HttpListenerContext context);
        void Post(ref HttpListenerContext context);
        void Put(ref HttpListenerContext context);
        void Patch(ref HttpListenerContext context);
        void Delete(ref HttpListenerContext context);
    }
}
