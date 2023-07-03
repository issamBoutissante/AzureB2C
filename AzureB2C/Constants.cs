using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AzureB2C
{
    internal static class Constants
    {
        public static readonly string ClientId = "33866026-b8a0-4cc6-9808-c033c64dc457";
        public static readonly string[] Scopes = new string[] { "openid", "offline_access" };
        public static readonly string TenantName = "Well2Test";
        public static readonly string TenantId = $"{TenantName}.onmicrosoft.com";
        public static readonly string SignInPolicy = "B2C_1_SignIn_SignUp";
        public static readonly string AuthorityBase = $"https://{TenantName}.b2clogin.com/tfp/{TenantId}/";
        public static readonly string AuthoritySignIn = $"{AuthorityBase}{SignInPolicy}";
    }
}
