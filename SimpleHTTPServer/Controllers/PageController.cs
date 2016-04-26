using System;
using System.IO;
using System.Net;

namespace HttpServer.Controllers
{
    public class PageController : IWebController
    {
        public string Page { get; set; }

        public PageController(string page)
        {
            this.Page = page;
        }        

        public void Get(ref HttpListenerContext context)
        {
            FileStream index;
            if (context.Request.Url.AbsolutePath.Equals($@"/{this.Page}"))
            {
                index = new FileStream($@"..\web_source\{this.Page}\index.html", FileMode.Open);
            }
            else
            {
                index = new FileStream($@"..\web_source\{context.Request.Url.AbsolutePath.Replace('/', '\\')}", FileMode.Open);
            }
            
            index.CopyTo(context.Response.OutputStream);
            index.Close();
        }

        public void Post(ref HttpListenerContext context)
        {
            throw new NotImplementedException();
        }

        public void Put(ref HttpListenerContext context)
        {
            throw new NotImplementedException();
        }

        public void Patch(ref HttpListenerContext context)
        {
            throw new NotImplementedException();
        }

        public void Delete(ref HttpListenerContext context)
        {
            throw new NotImplementedException();
        }
    }
}
