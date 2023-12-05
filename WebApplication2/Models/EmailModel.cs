using System;
using System.Web;
using System.Net.Mail;
namespace WebApplication2.Models
{
    public class EmailModel { 
       public String TO { get; set; }
       public String Subject { get; set; }
       public String Body { get; set; }
      /* public HttpPostedFileBase Attachment { get; set; }*/
        public String Email { get; set; }
        public String password { get; set; }


    }
}
