﻿using System.Collections.Generic;

namespace AmplaData.Data.Records
{
    public class AmplaAuditRecord
    {
        public int Id { get; set; }

        public string Location { get; set; }

        public string Module { get; set; }

        public List<AmplaAuditSession> Changes { get; set; }
    }
}