using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace api.Models.DTO
{
    public class UserGameDTO
    {
        public int GameId { get; set; }
        public bool IsFavorite { get; set; }
    }

}