using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EmployeeManagementPlatform.ModelView
{
    public class Credentials
    {
        public string EmailCredentials { get; set; }
        
        public string PasswordCredentials { get; set; }

        public int IDQuestion { get; set; }

        public string Response { get; set; }
    }
}