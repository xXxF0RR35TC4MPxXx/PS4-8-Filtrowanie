using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http.Features;
using System.Net;
using Microsoft.Extensions.Configuration;

namespace PS4_8_Filtrowanie.Pages.Filters
{
    public class CustomFilterAttributes : ResultFilterAttribute
    {
        public CustomFilterAttributes() { }
        public override async Task OnResultExecutionAsync(ResultExecutingContext context, ResultExecutionDelegate next)
        {

            IPAddress IP = context.HttpContext.Connection.RemoteIpAddress;
            IPAddress IPv4 = IP.MapToIPv4();
            IPAddress IPv6 = IP.MapToIPv6();
            List<string> hosts = new List<string>();
            
            var temp = Dns.GetHostAddresses(Dns.GetHostName());
            foreach (var _ in temp)
            { hosts.Add(_.ToString()); }

            

            var result = context.Result;
            if (result is PageResult page)
            {
                page.ViewData["IP_Addressv4"] = IPv4;
                page.ViewData["IP_Addressv6"] = IPv6;
                page.ViewData["IP_Addressv6local"] = hosts[0];
                page.ViewData["IP_Addressv4local"] = hosts[1];
            }
            await next.Invoke();
        }
    }
}
