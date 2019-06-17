using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Models.Requests.Emails
{
   public class EmailBase
    {
        [Required, StringLength(255), EmailAddress(ErrorMessage = "The email address is not valid")]
        public string To { get; set; }
        [Required, StringLength(255, MinimumLength = 1, ErrorMessage = "First Name length must be greater than 2 and less than 255 characters")]
        public string FirstName { get; set; }
        [Required, StringLength(255, MinimumLength = 1, ErrorMessage = "Last Name length must be greater than 2 and less than 255 characters")]
        public string LastName { get; set; }
    }
}
