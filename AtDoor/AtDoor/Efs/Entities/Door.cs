using System;
using System.Collections.Generic;

namespace AtDoor.Efs.Entities
{
    public partial class Door
    {
        public Door()
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

        public virtual ICollection<CardDoor> CardDoor { get; set; }
    }
}
