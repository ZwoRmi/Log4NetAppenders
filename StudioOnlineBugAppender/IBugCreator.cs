using System;
using System.Net.Http;

namespace StudioOnlineBugAppender
{
    public interface IBugCreator {
        bool UseProxy { get; set; }
        Uri Proxy { get; set; }
        HttpContent Response { get; }
        void SendBug(IBug bug);
    }
}