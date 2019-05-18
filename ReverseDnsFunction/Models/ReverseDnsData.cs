using System;
using System.Collections.Generic;
using System.Net;
using System.Text;

namespace ReverseDnsFunction.Models
{
    public class ReverseDnsData
    {

        public string[] AddressList { get; set; }

        public string[] Aliases { get; set; }

        public string HostName { get; set; }

        public ReverseDnsData(IPHostEntry hostInfo)
        {
            Aliases = new string[hostInfo.Aliases.Length];
            AddressList = new string[hostInfo.AddressList.Length];
            List<string> aliases = new List<string>();
            List<string> addresses = new List<string>();
            
            HostName = hostInfo?.HostName;
            if (hostInfo.Aliases.Length > 0)//i'm sure there's a slicker wayto do these
            {
                hostInfo.Aliases.CopyTo(Aliases, 0);
            }
            if (hostInfo.AddressList.Length > 0)//i'm sure there's a slicker wayto do these
            {
                for (int i = 0; i < hostInfo.AddressList.Length; i++)
                {
                    AddressList[i] = hostInfo.AddressList[i].ToString();
                }

            }
        }
    }
}
