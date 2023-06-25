using System.ComponentModel.DataAnnotations;

namespace DotNet.Application.Attributes
{
    public class YearValidationAttribute : ValidationAttribute
    {
        public override bool IsValid(object value)
        {
            if (value == null)
                return false;

            int year = (int)value;

            return year >= DateTime.Now.Year;
        }

        public override string FormatErrorMessage(string name)
        {
            return $"O ano da turma deve ser igual ou posterior ao ano atual.";
        }
    }
}