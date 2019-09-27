using System;
using System.Collections.Generic;

namespace AtDoor.Efs.Entities
{
    public partial class User
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string FkCardId { get; set; }
        public int? Permission { get; set; }
        public string Description { get; set; }
        public string CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public string ModifiedBy { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public byte[] RowStatus { get; set; }
        public int Status { get; set; }
    }
}
