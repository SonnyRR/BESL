﻿namespace BESL.Application.Infrastructure.Validators
{
    using System.Threading;
    using System.Threading.Tasks;

    using FluentValidation.Validators;
    using Microsoft.AspNetCore.Http;

    using BESL.Application.Interfaces;

    public class CustomGameImageFileValidator : AsyncValidatorBase
    {
        private readonly IFileValidate fileValidate;

        public CustomGameImageFileValidator(IFileValidate fileValidate) : base("{ErrorMessage}")
        {
            this.fileValidate = fileValidate;
        }

#pragma warning disable CS1998
        protected override async Task<bool> IsValidAsync(PropertyValidatorContext context, CancellationToken cancellation)
        {
            var fileToValidate = context.PropertyValue as IFormFile;

            var (Valid, ErrorMessage) = this.fileValidate.ValidateFile(fileToValidate);

            if (!Valid)
            {
                context.MessageFormatter.AppendArgument("ErrorMessage", ErrorMessage);
                return false;
            }

            return true;
        }
# pragma warning restore
    }
}
