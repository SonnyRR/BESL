namespace BESL.SandBox
{
    using BESL.Persistence;
    using Microsoft.EntityFrameworkCore;
    using ISO3166;
    using System;
    using BESL.Domain.Extensions;

    public class Program
    {
        public static void Main()
        {
            Console.WriteLine(Country.List.GetCountryByValue("BG333").ThreeLetterCode);
        }
    }
}
