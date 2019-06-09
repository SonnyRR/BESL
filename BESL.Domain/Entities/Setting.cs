namespace BESL.Domain.Entities
{
    using BESL.Domain.Infrastructure;   

    public class Setting : BaseDeletableModel<int>
    {
        public string Name { get; set; }

        public string Value { get; set; }
    }
}
