namespace BESL.Application.Exceptions
{
    using System.Collections.Generic;
    using System.Linq;
    using FluentValidation.Results;

    public class ValidationException : BaseCustomException
    {
        public ValidationException()
            : base("One or more validation failures have occurred.")
        {
            this.Failures = new Dictionary<string, string[]>();
        }

        public ValidationException(IList<ValidationFailure> failures)
            : this()
        {
            var propertyNames = failures
                .Select(e => e.PropertyName)
                .Distinct()
                .ToList();

            foreach (var propertyName in propertyNames)
            {
                var propertyFailures = failures
                    .Where(e => e.PropertyName == propertyName)
                    .Select(e => e.ErrorMessage)
                    .ToArray();

                this.Failures.Add(propertyName, propertyFailures);
            }
        }

        public IDictionary<string, string[]> Failures { get; }
    }
}
