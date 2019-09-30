using System;
using System.Collections.Generic;

namespace AT_Door.Efs.Entities
{
    public partial class Card
    {
        public Card()
        {
            HistoryCard = new HashSet<HistoryCard>();
            HistoryDoor = new HashSet<HistoryDoor>();
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

        public virtual Users FkUser { get; set; }
        public virtual ICollection<HistoryCard> HistoryCard { get; set; }
        public virtual ICollection<HistoryDoor> HistoryDoor { get; set; }
    }
}
