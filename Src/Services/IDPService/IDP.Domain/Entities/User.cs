using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IDP.Domain.Entities.BaseEntities;

namespace IDP.Domain.Entities
{

    public class User : BaseEntity
    {
        // First step
        public required string PhoneNumber { get; set; }

        // Secend step
        public string? FullName { get; set; }
        public string? NationalCode { get; set; }
        public string? UserName { get; set; }
        public string? Password { get; set; }
        public string? Salt { get; set; }
        public string? Address { get; set; }
        public string? Email { get; set; }

        // ✅ Status
        public bool IsPhoneNumberConfirmed { get; set; } = false;
        public bool IsProfileCompleted { get; set; } = false;

        public DateTime? PhoneConfirmedAt { get; set; }
    }

}
