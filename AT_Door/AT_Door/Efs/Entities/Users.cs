using System;
using System.Collections.Generic;

namespace AT_Door.Efs.Entities
{
    public partial class Users
    {
        public Users()
        {
            Card = new HashSet<Card>();
            HistoryCard = new HashSet<HistoryCard>();
        }

        public string Id { get; set; }
        public string Name { get; set; }
        public string Code { get; set; }
        public int? Permission { get; set; }
        public string Description { get; set; }
        public string CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public string ModifiedBy { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public byte[] RowStatus { get; set; }
        public int Status { get; set; }

        public virtual ICollection<Card> Card { get; set; }
        public virtual ICollection<HistoryCard> HistoryCard { get; set; }
    }
}
