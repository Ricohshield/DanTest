using System;
using System.ComponentModel.DataAnnotations;
using Octokit;

namespace DanTest.Models.ViewModels
{
    public class RepoSearchRequestViewModel
    {

        public long Id { get; set; }
        [StringLength(50, MinimumLength = 3)]
        [Required]
        public string Term { get; set; }
        public Language Language { get; set; }
        [Range(0, int.MaxValue)]
        [LessOrEqaulThan("ForkTo")]
        public int? ForkFrom { get; set; }
        [Range(0, int.MaxValue)]
        public int? ForkTo { get; set; }
        [Range(0, int.MaxValue)]
        [LessOrEqaulThan("StarsTo")]
        public int? StarsFrom { get; set; }
        [Range(0, int.MaxValue)]
        public int? StarsTo { get; set; }
        [Range(0, int.MaxValue)]
        [LessOrEqaulThan("SizeTo")]
        public int? SizeFrom { get; set; }
        [Range(0, int.MaxValue)]
        public int? SizeTo { get; set; }
        public bool IsArchived { get; set; }
        public DateTime TimeStamp { get; set; }
    }

    public class LessOrEqaulThanAttribute : ValidationAttribute
    {
        private readonly string _comparisonProperty;

        public LessOrEqaulThanAttribute(string comparisonProperty)
        {
            _comparisonProperty = comparisonProperty;
        }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            ErrorMessage = ErrorMessageString;
            var currentValue = (int?)value;

            var property = validationContext.ObjectType.GetProperty(_comparisonProperty);

            if (property == null)
                throw new ArgumentException("Property with this name not found");

            var comparisonValue = (int?)property.GetValue(validationContext.ObjectInstance);

            if (comparisonValue == null || currentValue == null)
            {
                return ValidationResult.Success;
            }

            return currentValue > comparisonValue ? new ValidationResult(ErrorMessage) : ValidationResult.Success;
        }
    }
}
