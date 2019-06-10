namespace BESL.Domain.Extensions
{
    using System.Linq;

    using ISO3166;

    public static class ISO3166Extensions
    {
        public static Country GetCountryByValue(this Country[] countries, string nameOrCode)
        {
            Country countryToReturn;

            if (nameOrCode.Length == 2)
            {
                countryToReturn = Country
                    .List
                    .FirstOrDefault(c => c.TwoLetterCode == nameOrCode.ToUpper());
            }
            else if (nameOrCode.Length == 3)
            {
                if (int.TryParse(nameOrCode, out _))
                {
                    countryToReturn = Country
                    .List
                    .FirstOrDefault(c => c.NumericCode == nameOrCode);
                }
                else
                {
                    countryToReturn = Country
                    .List
                    .FirstOrDefault(c => c.ThreeLetterCode == nameOrCode.ToUpper());
                }
            }
            else
            {
                countryToReturn = Country
                    .List
                    .FirstOrDefault(c => c.Name == nameOrCode);
            }

            return countryToReturn;
        }
    }
}
