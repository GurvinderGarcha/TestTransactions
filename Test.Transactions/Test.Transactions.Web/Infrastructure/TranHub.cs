using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using Microsoft.AspNet.SignalR;

namespace WebApplication2.Infrastructure
{
    public class TranHub : Hub
    {
        public TranHub()
        {
            var taskTimer = Task.Factory.StartNew(async () =>
            {
                while (true)
                {
                    string timeNow = DateTime.Now.ToString();
                    //Sending the server time to all the connected clients on the client method SendServerTime()
                    Clients.All.SendServerTime(timeNow);
                    //Delaying by 3 seconds.
                    await Task.Delay(3000);
                }
            }, TaskCreationOptions.LongRunning
               );
        }

        public void RequestProgress()
        {
            
        }

        public void SendMessage()
        {
            
        }
    }
}