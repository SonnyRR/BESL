namespace BESL.Domain.Infrastructure
{
    using System;
    using System.ComponentModel.DataAnnotations;

    public abstract class BaseModel<TKey>
    {
        [Key]
        public TKey Id { get; set; }

        public DateTime CreatedOn { get; set; }

        public DateTime? ModifiedOn { get; set; }
    }
}
