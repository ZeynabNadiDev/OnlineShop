using MediatR;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IDP.Application.Commands.User
{
    public record UserCommand:IRequest<bool>
    {
        [Required(ErrorMessage = "This data is required.")]
        [MinLength(4)]
        public required string FullName { get; set; }
        public required string NationalCode { get; set; }
        public string? Address { get; set; }
        public string? Email { get; set; }
        public string? PhoneNumber { get; set; }

    }
}
