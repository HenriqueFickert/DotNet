using System.ComponentModel.DataAnnotations;

namespace DotNet.Application.Attributes
{
    public class PasswordValidationAttribute : ValidationAttribute
    {
        public override bool IsValid(object value)
        {
            if (value == null)
                return false;

            string password = value.ToString();

            bool hasUppercase = false;
            bool hasNumber = false;
            bool hasSymbol = false;

            foreach (char c in password)
            {
                if (char.IsUpper(c))
                    hasUppercase = true;
                else if (char.IsDigit(c))
                    hasNumber = true;
                else if (!char.IsLetterOrDigit(c))
                    hasSymbol = true;

                if (hasUppercase && hasNumber && hasSymbol)
                    return true;
            }

            return false;
        }

        public override string FormatErrorMessage(string name)
        {
            return $"A senha deve conter pelo menos uma letra maiúscula, um número e um símbolo.";
        }
    }
}