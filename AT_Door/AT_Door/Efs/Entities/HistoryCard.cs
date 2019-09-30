using System;
using System.Collections.Generic;

namespace AT_Door.Efs.Entities
{
    public partial class HistoryCard
    {
        public string Id { get; set; }
        public string FkCardId { get; set; }
        public string FkUserId { get; set; }
        public int Action { get; set; }
        public string Description { get; set; }
        public string CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public string ModifiedBy { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public byte[] RowStatus { get; set; }
        public int Status { get; set; }

        public virtual Card FkCard { get; set; }
        public virtual Users FkUser { get; set; }
    }
}
