﻿using System;
using System.Collections.Generic;

namespace AT_Door.Efs.Entities
{
    public partial class HistoryDoor
    {
        public string Id { get; set; }
        public string FkDoorId { get; set; }
        public string FkCardId { get; set; }
        public int Action { get; set; }
        public string Description { get; set; }
        public string CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public string ModifiedBy { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public byte[] RowStatus { get; set; }
        public int Status { get; set; }

        public virtual Card FkCard { get; set; }
        public virtual Door FkDoor { get; set; }
    }
}
