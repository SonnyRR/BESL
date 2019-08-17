namespace BESL.Infrastructure
{
    using System;
    using BESL.Application.Interfaces;

    public class MachineDateTime : IDateTime
    {
        public DateTime Now => DateTime.Now;

        public DateTime UtcNow => DateTime.UtcNow;
    }
}
