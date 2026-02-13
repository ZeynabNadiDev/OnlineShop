using Auth;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IDP.Application.Queries.Auth
{
    public record AuthQuery:IRequest<JsonWebToken>
    {
        public required string MobileNumber { get; set; }
        public required string OtpCode { get; set; }
    }
}
