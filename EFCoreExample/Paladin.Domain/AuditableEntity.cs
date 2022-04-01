﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WDIPaladins.Domain
{
    public class AuditableEntity
    {
        public DateTimeOffset CreatedAt { get; init; }
            = DateTimeOffset.Now;
    }
}
