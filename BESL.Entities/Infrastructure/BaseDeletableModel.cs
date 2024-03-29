﻿namespace BESL.Entities.Infrastructure
{
    using System;

    public abstract class BaseDeletableModel<TKey> : BaseModel<TKey>, IDeletableEntity
        where TKey: IComparable<TKey>, IEquatable<TKey>
    {
        public bool IsDeleted { get; set; }

        public DateTime? DeletedOn { get; set; }
    }
}
