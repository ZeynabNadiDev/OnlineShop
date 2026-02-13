using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IDP.Application.Helper
{
    public static class OtpGenerator
    {
        public static string Generate()
        {
            return Random.Shared.Next(100000, 999999).ToString();
        }
    }
}

