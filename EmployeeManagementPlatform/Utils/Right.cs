using EmployeeManagementPlatform.Context;
using EmployeeManagementPlatform.Models;
using EmployeeManagementPlatform.ModelView;
using EmployeeManagementPlatform.Utils;
using System.Collections.Generic;
using System.Data.Entity.Validation;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace EmployeeManagementPlatform.Utils
{
    public class Right
    {
        public static bool Verify(int rightRequired, Employee session) {
            bool ok = false;
            if (session.IntRight == rightRequired)
                ok = true;
            return ok;
        }
        
    }
    public enum Rights
    {
        ADMIN = 1,
        USER = 2,
        ACCOUNTANT = 3,
    }

    public enum Status
    {
        CHANGE_PASSWORD = 1,
        INACTIV = 2,
        ACTIV = 3,

    }
}