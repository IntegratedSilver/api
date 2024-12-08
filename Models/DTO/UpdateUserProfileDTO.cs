using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace api.Models.DTO
{
    public class UpdateUserProfileDTO
    {
        public string? Username { get; set; }
        public string? Avatar { get; set; }
    }
}