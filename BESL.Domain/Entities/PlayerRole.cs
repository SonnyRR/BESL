﻿using BESL.Domain.Infrastructure;
using Microsoft.AspNetCore.Identity;
using System;

namespace BESL.Domain.Entities
{
    public class PlayerRole : IdentityRole, IAuditInfo, IDeletableEntity
    {
        public PlayerRole()
            : this(null)
        {
        }

        public PlayerRole(string name)
            : base(name)
        {
            this.Id = Guid.NewGuid().ToString();
        }

        public DateTime CreatedOn { get; set; }

        public DateTime? ModifiedOn { get; set; }

        public bool IsDeleted { get; set; }

        public DateTime? DeletedOn { get; set; }
    }
}