using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;

namespace Contabilidad.WebForms.App_Code.General
{
    public class CIP
    {
        public CIP()
        {
            //
            // TODO: Agregar aquí la lógica del constructor
            //
        }
        public string ObtenerIP()
        {
            IPHostEntry host;
            string localIP = "";
            host = Dns.GetHostEntry(Dns.GetHostName());
            foreach (IPAddress ip in host.AddressList)
            {
                if (ip.AddressFamily.ToString() == "InterNetwork")
                {
                    localIP = ToString();
                }
            }

            return localIP;
        }
    }
}