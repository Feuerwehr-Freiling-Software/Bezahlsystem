using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace Paymentsystem.Client.Pages.Admin
{
    public partial class AccessTokens
    {
        public AccessTokens()
        {
            Interceptor.RegisterEvent();
        }

        public void Dispose() => Interceptor.DisposeEvent();
    }
}
