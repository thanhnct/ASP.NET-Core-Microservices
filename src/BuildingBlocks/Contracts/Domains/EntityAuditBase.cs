﻿using Contracts.Domains.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contracts.Domains
{
    public abstract class EntityAuditBase<T> : EntityBase<T>, IAuditable
    {
        public DateTimeOffset CreatedAt { get; set; }

        public DateTimeOffset? LastModifiedAt { get; set; }
    }
}
