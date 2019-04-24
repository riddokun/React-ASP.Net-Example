using System;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace AspNetReact.Models
{
    public class LogOnModel
    {
        TestDBDataContext _cxt = new TestDBDataContext();

        [Required]
        [Display(Name = "User name")]
        public string UserName { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [Display(Name = "Remember me?")]
        public bool RememberMe { get; set; }

        [Display(Name = "Role")]
        public char Role { get; set; }

        //Model class used to determine the actual user
        public LogOnModel GetUser(LogOnModel logon, bool cookie)
        {
            if (cookie == false)
            {
                logon.Password = Encode(logon.Password);
            }

            WebUser user = new WebUser();
            try
            {
                user = _cxt.WebUsers.Single(a => a.Password == logon.Password
                    && a.UserID == logon.UserName && a.Active == 'Y');
            }
            catch
            {
                logon = null;
                return logon;
            }

            logon.Role = user.Role;

            return logon;
        }

        public string Encode(string value)
        {
            var hash = System.Security.Cryptography.SHA1.Create();
            var encoder = new System.Text.ASCIIEncoding();
            var combined = encoder.GetBytes(value ?? "");
            return BitConverter.ToString(hash.ComputeHash(combined)).ToLower().Replace("-", "");
        }
    }

    //Model used to allow users to register
    public class RegisterViewModel
    {
        [Required]
        public string Username { get; set; }

        [Required]
        public string Email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [Display(Name = "Confirm Password")]
        [Compare("Password", ErrorMessage = "Confirm password doesn't match, try again!")]
        public string ConfirmPassword { get; set; }
    }
}