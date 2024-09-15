using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.NetworkInformation;
using Microsoft.Maui.Networking;

namespace Cronos.Services
{
    public static class VerificaInternetService
    {
        public static bool VerificaInternet()
        {
            var current = Connectivity.Current.NetworkAccess;

            // Verifica se há acesso à internet
            if (current == NetworkAccess.Internet)
            {
                try
                {
                    Ping ping = new Ping();
                    PingReply reply = ping.Send("8.8.8.8", 1000); 

                    if (reply.Status == IPStatus.Success)
                    {
                        return true;
                    }
                }
                catch
                {
                    return false;
                }
            }

            return false;
        }
    }
}
