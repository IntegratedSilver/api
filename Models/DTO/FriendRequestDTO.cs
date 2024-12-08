using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace api.Models.DTO
{
    public class FriendRequestDTO
    {
        internal object SentAt;
        internal object ReceiverName;
        internal object ReceiverId;
        internal object SenderName;
        internal object SenderId;
        internal object Status;
        internal object Id;

        public int AddresseeId { get; set; }
        public int RequestId { get; internal set; }
        public UserProfileDTO Requester { get; internal set; }
        public DateTime CreatedAt { get; internal set; }
    }

}