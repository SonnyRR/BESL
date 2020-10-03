namespace BESL.Entities
{
    using BESL.Entities.Infrastructure;   

    public class Setting : BaseDeletableModel<string>
    {
        public string Name { get; set; }

        public string Value { get; set; }
    }
}
