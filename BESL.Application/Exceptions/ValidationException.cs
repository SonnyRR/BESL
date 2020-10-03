namespace BESL.Application.Exceptions
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using FluentValidation.Results;

    using static BESL.SharedKernel.GlobalConstants;

    public class ValidationException : BaseCustomException
    {
        public ValidationException()
            : base()
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

        public override string Message
        {
            get
            {
                StringBuilder stringBuilder = new StringBuilder();
                stringBuilder.AppendLine(VALIDATION_EXCEPTION_BASE_MSG);

                foreach (var kvp in this.Failures)
                {
                    string propertyName = kvp.Key;
                    string[] propertyFailure = kvp.Value;

                    stringBuilder.AppendLine($"{propertyName}:");

                    foreach (var failure in propertyFailure)
                    {
                        stringBuilder.AppendLine($" *{failure}");
                    }
                }

                return stringBuilder.ToString().Trim();
            }
        }
    }
}
