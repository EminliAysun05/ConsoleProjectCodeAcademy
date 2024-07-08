

namespace ConsoleProjectCodeAcademy.Exceptions.Validations
{
    public static class RegisterValidations
    {
        //ilk herf boyuk olsun 
        public static bool IsFullnameValidate(this string fullname)
        {
            //index methodu
            //bu method aradaki boslugu tapir, sonra o bosluqdan sonra gelen (+1) 
            //herfin boyuk olub olmamasini (isUpper) yoxlayir

            int secondWordIndex = fullname.IndexOf(' ');  //Asiman 

            if (secondWordIndex < fullname.Length - 1) //eger bosluq onun sonuncu elementi deyilse secondWordIndexi artir
                secondWordIndex++;

            if (fullname.Length < 2 || fullname.Length > 22)
            {
                return false;
            }
            if (char.IsUpper(fullname[secondWordIndex]) && char.IsUpper(fullname[0]))
            {
                return true;
            }

            return false;
        }

        public static bool IsEmailValidate(this string email)
        {
            if ((email.Contains("@")) && email.Length > 2 && email.Length < 20)
            {
                return true;
            }
            return false;
        }

        public static bool IsPasswordValidate(this string password)
        {
            bool isExistUpperCase = false;
            bool isExistDigit = false;
            if (!(password.Length >= 8))
            {
                return false;
            }
            foreach (char c in password)
            {
                if (char.IsUpper(c))
                    isExistUpperCase = true;

                if (char.IsDigit(c))
                    isExistDigit = true;

            }
            return isExistDigit && isExistUpperCase;

        }

        //fullname.Contains(" ");
    }
}
