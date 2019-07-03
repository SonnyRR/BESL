namespace BESL.Application.Exceptions
{
    using System.Collections.Generic;
    using System.Linq;

    using FluentValidation.Results;

    using static BESL.Common.GlobalConstants;

    public class ValidationException : BaseCustomException
    {
        public ValidationException()
            : base(VALIDATION_EXCEPTION_BASE_MSG)
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
