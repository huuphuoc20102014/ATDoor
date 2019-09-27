using System;
using System.Collections.Generic;

namespace AtDoor.Efs.Entities
{
    public partial class Card
    {
        public Card()
        {
            CardDoor = new HashSet<CardDoor>();
        }

        public string Id { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public string ModifiedBy { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public byte[] RowStatus { get; set; }
        public int Status { get; set; }
        public string FkUserId { get; set; }

        public virtual ICollection<CardDoor> CardDoor { get; set; }
    }
}
