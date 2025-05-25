using System.Text.RegularExpressions;

namespace RetailCitikold.Domain.Helpers;

public  class PasswordValidatorLocal
{
    public  bool IsValidPassword(string password, out string error)
    {
        error = "";

        if (string.IsNullOrWhiteSpace(password) || password.Length < 8)
        {
            error = "La contraseña debe tener al menos 8 caracteres.";
            return false;
        }

        if (!Regex.IsMatch(password, "[A-Z]"))
        {
            error = "La contraseña debe contener al menos una letra mayúscula.";
            return false;
        }

        if (!Regex.IsMatch(password, "[a-z]"))
        {
            error = "La contraseña debe contener al menos una letra minúscula.";
            return false;
        }

        if (!Regex.IsMatch(password, "[0-9]"))
        {
            error = "La contraseña debe contener al menos un número.";
            return false;
        }

        if (!Regex.IsMatch(password, @"[\W_]")) 
        {
            error = "La contraseña debe contener al menos un carácter especial.";
            return false;
        }

        return true;
    }
}