using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;
namespace WebApplication2.Models
{
    public class UserViewModel
    {
        public string Id {  get; set; }
        [DisplayName("First Name")]
        public string FirstName { get; set; }
        [DisplayName("Last Name")]
        public string LastName { get; set; }
        [DisplayName("E-Mail")]
        [EmailAddress(ErrorMessage ="please enter a vaild email address")]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        [DisplayName("Phone no")]
        public String Phone {  get; set; }

        public List<UserViewModel> UserInRoles { get; set;}
        public bool IsLockedOut {  get; set; }




    }
}