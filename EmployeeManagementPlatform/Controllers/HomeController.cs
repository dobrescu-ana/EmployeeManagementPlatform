using EmployeeManagementPlatform.Context;
using EmployeeManagementPlatform.Models;
using EmployeeManagementPlatform.ModelView;
using EmployeeManagementPlatform.Utils;
using System.Collections.Generic;
using System.Data.Entity.Validation;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Rotativa;
using NPOI.XWPF.UserModel;

namespace EmployeeManagementPlatform.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            if (Session["user"] == null)
            {
                return RedirectToAction("LogIn");
            }
            return View();
        }

        #region Authentication

        public ActionResult LogIn()
        {

            Credentials credentials = new Credentials();
            return View(credentials);
        }

        [HttpPost]
        public ActionResult LogIn(Credentials cred)
        {

            AppContext ctx = new AppContext();

            Employee emp = ctx.Employees.Where(item => item.Email == cred.EmailCredentials && item.Password == cred.PasswordCredentials).FirstOrDefault();

            if (emp != null)
            {
                if(emp.AccountStatus == (int)Utils.Status.ACTIV)
                {
                    Session["user"] = emp;
                    Session["userID"] = emp.IDEmployee;
                    ctx.Logs.Add(new Logs("Login success", "User authentication", System.DateTime.Now, "informational", emp.LastName + " " + emp.FirstName));
                    ctx.SaveChanges();
                }
                else
                {
                    if(emp.AccountStatus == (int)Utils.Status.CHANGE_PASSWORD)
                    {
                        Session["user"] = emp;
                        Session["userID"] = emp.IDEmployee;
                        return RedirectToAction("ConfigureAccount");
                    }
                }
              
            }
            else
            {
                ctx.Logs.Add(new Logs("Login failure", "User authentication", System.DateTime.Now, "warning", "unknown user"));
                ctx.SaveChanges();
                ViewBag.ErrorMsg = "Wrong credentials";
            }
            return RedirectToAction("Index");
        }

        public ActionResult Logout() {
            Employee emp = (Employee)Session["user"];
            AppContext ctx = new AppContext();
            ctx.Logs.Add(new Logs("Logout", "Session over", System.DateTime.Now, "informational", emp.LastName + " " + emp.FirstName));
            ctx.SaveChanges();
            Session["user"] = null;
           
            return RedirectToAction("LogIn", "Home");
        }

        #endregion

        #region Configure account

        public ActionResult ConfigureAccount()
        {
            Employee emp = (Employee)Session["user"];
            AppContext ctx = new AppContext();
            ViewBag.Questions = ctx.Questions.ToList();
            return View(emp);
        }

        [HttpPost]
        public ActionResult ConfigureAccount(Employee emp)
        {
            AppContext ctx = new AppContext();
            Employee dbemp = ctx.Employees.Where(item => item.IDEmployee == emp.IDEmployee).FirstOrDefault();
            dbemp.IDQuestion = emp.IDQuestion;
            dbemp.Response = emp.Response;
            dbemp.Password = emp.Password;
            ctx.SaveChanges();
            return RedirectToAction("Index");
        }

        #endregion

        #region Forgot password

        [HttpGet]
        public ActionResult ForgotPassword()
        {
            Credentials c = new Credentials();
            AppContext ctx = new AppContext();
            ViewBag.Questions = ctx.Questions.ToList();
            return View(c);
        }

        [HttpPost]
        public ActionResult ForgotPassword(Credentials cred)
        {
            AppContext ctx = new AppContext();
            Employee emp = ctx.Employees.Where(item => item.Email == cred.EmailCredentials).FirstOrDefault();
            if (emp != null)
            {
                if (emp.IDQuestion == cred.IDQuestion && emp.Response == cred.Response)
                {
                    emp.Password = cred.PasswordCredentials;
                    ctx.SaveChanges();
                    Session["user"] = emp;
                    Session["userID"] = emp.IDEmployee;
                    return RedirectToAction("Index");
                }
                else
                {
                    return RedirectToAction("ForgotPassword");
                }
            }
            else
            {
                return RedirectToAction("Login");
            }

        }

        #endregion

        #region Change password

        [HttpGet]
        public ActionResult ChangePassword()
        {
            Credentials c = new Credentials();
            return View(c);
        }

        [HttpPost]
        public ActionResult ChangePassword(Credentials cred)
        {
            AppContext ctx = new AppContext();
            int empId = (int)Session["userID"];
            Employee emp = ctx.Employees.Where(item => item.IDEmployee == empId).FirstOrDefault();
            emp.Password = cred.PasswordCredentials;
            emp.AccountStatus = (int)Utils.Status.ACTIV;
            ctx.SaveChanges();
            Session["user"] = emp;
            Session["userID"] = emp.IDEmployee;
            return RedirectToAction("Index");
        }

        #endregion
        
        #region Profile
        public ActionResult EmployeeDetails()
        {
            AppContext ctx = new AppContext();
            int ID = System.Convert.ToInt32(Session["userID"]);
            Employee emp = (Employee)ctx.Employees.Where(item => item.IDEmployee == ID).FirstOrDefault();
            if (emp != null)
            {
                Session["user"] = emp;
                Session["userID"] = emp.IDEmployee;
                ctx.Logs.Add(new Logs("Employee details visualisation", "User view personal data", System.DateTime.Now, "informational", emp.LastName + " " + emp.FirstName));
                ctx.SaveChanges();
            }

            var dep = ctx.Departments.Where(item => item.IdDepartment == emp.IdDepartment).FirstOrDefault();
            emp.department = dep;
            return View(emp);
        }

        #endregion

        #region Department Management

        [HttpGet]
        [AccessControl(1)]
        public ActionResult AddDepartment()
        {
            Department department = new Department();

            return View(department);
        }

        [HttpPost]
        [AccessControl(1)]
        public ActionResult AddDepartment(Department department)
        {
            AppContext ctx = new AppContext();
            Employee emp = (Employee)Session["user"];
            ctx.Departments.Add(department);
            ctx.Logs.Add(new Logs("Add department", "A new department has been added to the database", System.DateTime.Now, "informational", emp.LastName + " " + emp.FirstName));
            ctx.SaveChanges();


            return RedirectToAction("Index");
        }

        public ActionResult ListDeps()
        {
            AppContext ctx = new AppContext();
            List<Department> list = ctx.Departments.ToList();
            return View(list);
        }

        #endregion

        #region Employee Management

        [AccessControl(1)]
        public ActionResult ListEmployees()
        {
            AppContext ctx = new AppContext();
            List<Employee> list = ctx.Employees.ToList();
            foreach (var e in list)
            {
                e.department = ctx.Departments.Where(item => item.IdDepartment == e.IdDepartment).FirstOrDefault();
            }
            return View(list);
            //return Json(new { data = list }, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        [AccessControl(1)]
        public ActionResult AddEmployee()
        {
            AppContext ctx = new AppContext();
            Employee emp = new Employee();
            emp.BirthDate = System.DateTime.Now;
            ViewBag.Deps = ctx.Departments.ToList();
            return View(emp);
        }


        [HttpPost]
        [AccessControl(1)]
        public ActionResult AddEmployee(Employee employee)
        {

            AppContext context = new AppContext();

            try
            {
                context.Employees.Add(employee);
                context.Logs.Add(new Logs("Add user", "A new user has been added to the database", System.DateTime.Now, "informational", employee.FirstName + " " + employee.LastName));
                context.SaveChanges();
            }
            catch (DbEntityValidationException e)
            {
                foreach (var eve in e.EntityValidationErrors)
                {
                    System.Console.WriteLine("Entity of type \"{0}\" in state \"{1}\" has the following validation errors:",
                        eve.Entry.Entity.GetType().Name, eve.Entry.State);
                     context.Logs.Add(new Logs("Add user error", "A new user has been added to the database", System.DateTime.Now, "eroor", eve.Entry.State.ToString()));


                    foreach (var ve in eve.ValidationErrors)
                    {
                        System.Console.WriteLine("- Property: \"{0}\", Error: \"{1}\"",
                            ve.PropertyName, ve.ErrorMessage);
                        context.Logs.Add(new Logs("Add user error", "A new user has been added to the database", System.DateTime.Now, "eroor", eve.Entry.State.ToString()));
                    }
                }
                throw;
            }
            return RedirectToAction("ListEmployees");
        }

        #endregion

        #region Leaves Management

        [AccessControl(1)]
        public ActionResult ListLeaves()
        {
            AppContext ctx = new AppContext();
            List<Leave> list = ctx.Leaves.ToList();

            return View(list);
        }

        [HttpGet]
        [AccessControl(2)]
        public ActionResult AddLeave()
        {
            AppContext ctx = new AppContext();
            Leave leave = new Leave();
            ViewBag.LeaveTypes = ctx.LeaveCategories.ToList();
            return View(leave);
        }


        [HttpPost]
        [AccessControl(2)]
        public ActionResult AddLeave(Leave leave)
        {
            leave.IDEmployee = ((Employee)Session["user"]).IDEmployee;
            AppContext context = new AppContext();

            try
            {
                context.Leaves.Add(leave);
                Employee emp = (Employee)Session["user"];
                context.Logs.Add(new Logs("User added leave", "User added a new leave", System.DateTime.Now, "informational", emp.LastName + " " + emp.FirstName));
                context.SaveChanges();
            }
            catch (DbEntityValidationException e)
            {
                foreach (var eve in e.EntityValidationErrors)
                {
                    System.Console.WriteLine("Entity of type \"{0}\" in state \"{1}\" has the following validation errors:",
                        eve.Entry.Entity.GetType().Name, eve.Entry.State);
                    foreach (var ve in eve.ValidationErrors)
                    {
                        System.Console.WriteLine("- Property: \"{0}\", Error: \"{1}\"",
                            ve.PropertyName, ve.ErrorMessage);
                    }
                }
                throw;
            }
            return RedirectToAction("Index");
        }
        #endregion

        #region Salary Management

        [HttpGet]
        [AccessControl(3)]
        public ActionResult AddSalary()
        {
            AppContext ctx = new AppContext();
            List<Employee> list = ctx.Employees.ToList();
            foreach (var emp in list) {
                emp.FirstName += " " + emp.LastName + " (" + emp.Email + ")";
            }
            ViewBag.Employees = list;
            Salary salary = new Salary();
            return View(salary);
        }

        [HttpPost]
        [AccessControl(3)]
        public ActionResult AddSalary(Salary salary)
        {
            AppContext context = new AppContext();
            Employee emp = (Employee)Session["user"];
            try
            {
                context.Salaries.Add(salary);
                context.Logs.Add(new Logs("Add salary", "A new saary has been added to the database", System.DateTime.Now, "informational", emp.LastName + " " + emp.FirstName));
                context.SaveChanges();
            }
            catch (DbEntityValidationException e)
            {
                foreach (var eve in e.EntityValidationErrors)
                {
                    System.Console.WriteLine("Entity of type \"{0}\" in state \"{1}\" has the following validation errors:",
                        eve.Entry.Entity.GetType().Name, eve.Entry.State);
                    context.Logs.Add(new Logs("Add salary error", "A new saary has been added to the database", System.DateTime.Now, "error", eve.Entry.State.ToString()));
                    context.SaveChanges();
                    foreach (var ve in eve.ValidationErrors)
                    {
                        System.Console.WriteLine("- Property: \"{0}\", Error: \"{1}\"",
                            ve.PropertyName, ve.ErrorMessage);
                    }
                }
                throw;
            }
            return RedirectToAction("Index");
        }

        [AccessControl(3)]
        public ActionResult ListSalary()
        {
            AppContext ctx = new AppContext();
            List<Salary> list = ctx.Salaries.ToList();
            foreach (var salary in list) {
                salary.employee = ctx.Employees.Where(item => item.IDEmployee == salary.IDEmployee).FirstOrDefault();
            }
            return View(list);
        }
        
        [AccessControl(2)]
        public ActionResult ViewMySalary() {
            AppContext ctx = new AppContext();
            Employee emp = (Employee)Session["user"];
            Salary mySalary = ctx.Salaries.Where(item => item.IDEmployee == emp.IDEmployee).FirstOrDefault();
            ctx.Logs.Add(new Logs("Salary visualisation", "User view salary", System.DateTime.Now, "informational", emp.LastName + " " + emp.FirstName));
            ctx.SaveChanges();
            return View(mySalary);
        }


        #endregion

        #region Project Management
        [HttpGet]
        [AccessControl(2)]
        public ActionResult AddProject()
        {
            Projects project = new Projects();
            return View(project);
        }

        [HttpPost]
        [AccessControl(2)]
        public ActionResult AddProject(Projects project)
        {
            project.IDEmployee = ((Employee)Session["user"]).IDEmployee;
            AppContext context = new AppContext();
            Employee emp = (Employee)Session["user"];
            try
            {
                context.Projects.Add(project);
                context.Logs.Add(new Logs("Add project", "A user added a new project", System.DateTime.Now, "informational", emp.LastName + " " + emp.FirstName));
                context.SaveChanges();
            }
            catch (DbEntityValidationException e)
            {
                foreach (var eve in e.EntityValidationErrors)
                {
                    System.Console.WriteLine("Entity of type \"{0}\" in state \"{1}\" has the following validation errors:",
                        eve.Entry.Entity.GetType().Name, eve.Entry.State);
                    context.Logs.Add(new Logs("Add project error", "A user added a new project", System.DateTime.Now, "error", eve.Entry.State.ToString()));
                    foreach (var ve in eve.ValidationErrors)
                    {
                        System.Console.WriteLine("- Property: \"{0}\", Error: \"{1}\"",
                            ve.PropertyName, ve.ErrorMessage);
                        context.Logs.Add(new Logs("Add project error", "A user added a new project", System.DateTime.Now, "error", ve.ErrorMessage.ToString()));

                    }
                    context.SaveChanges();
                }
                throw;
            }
            return RedirectToAction("Index");
        }


        [AccessControl(2)]
        public ActionResult ListProjects()
        {
            AppContext ctx = new AppContext();
            List<Projects> projects = ctx.Projects.ToList();
            foreach (var project in projects)
            {
                project.employee = ctx.Employees.Where(item => item.IDEmployee == project.IDEmployee).FirstOrDefault();
            }
            return View(projects);
        }
    #endregion

        #region Tasks

        [AccessControl(2)]
            public ActionResult ToDoList()
            {
                AppContext ctx = new AppContext();
                List<Task> tasks = ctx.Tasks.ToList();
                foreach (var task in tasks)
                {
                    task.difference = System.Convert.ToInt32((task.Deadline - System.DateTime.Now).TotalHours);
                    task.employee = ctx.Employees.Where(item => item.IDEmployee == task.IDEmployee).FirstOrDefault();
                }
                return View(tasks);
            }

            [HttpGet]
            [AccessControl(2)]
            public ActionResult AddTask()
            {
                Task task = new Task();
                return View(task);
            }


            [HttpPost]
            [AccessControl(2)]
            public ActionResult AddTask(Task task)
            {
                task.IDEmployee = ((Employee)Session["user"]).IDEmployee;
                AppContext context = new AppContext();
                Employee emp = (Employee)Session["user"];
                try
                {
                    context.Tasks.Add(task);
                    context.Logs.Add(new Logs("Add task", "A user added a new task", System.DateTime.Now, "informational", emp.LastName + " " + emp.FirstName));
                    context.SaveChanges();
                }
                catch (DbEntityValidationException e)
                {
                    foreach (var eve in e.EntityValidationErrors)
                    {
                        System.Console.WriteLine("Entity of type \"{0}\" in state \"{1}\" has the following validation errors:",
                            eve.Entry.Entity.GetType().Name, eve.Entry.State);
                        context.Logs.Add(new Logs("Add task error", "A user added a new task", System.DateTime.Now, "error", eve.Entry.State.ToString()));
                        context.SaveChanges();
                        foreach (var ve in eve.ValidationErrors)
                        {
                            System.Console.WriteLine("- Property: \"{0}\", Error: \"{1}\"",
                                ve.PropertyName, ve.ErrorMessage);
                            context.Logs.Add(new Logs("Add task error", "A user added a new task", System.DateTime.Now, "error", ve.ErrorMessage.ToString()));
                            context.SaveChanges();
                        }
                    }
                    throw;
                }
                return RedirectToAction("Index");
            }
            #endregion

        #region Export DOC
        private XWPFDocument CreateDocument(List<Employee> list) {
            XWPFDocument doc = new XWPFDocument();
            doc.CreateTable();
            XWPFTable table = doc.Tables[0];
            for (int celula = 0; celula < 10; celula++) {
                if(celula!=0)
                    table.Rows[0].AddNewTableCell();
                XWPFParagraph p = table.Rows[0].GetCell(celula).Paragraphs[0];
                XWPFRun run = p.CreateRun();
                run.FontFamily = "Times New Roman";
                run.FontSize = 10;
                switch (celula) {
                    case 0:
                        run.SetText("Departament");
                        break;
                    case 1:
                        run.SetText("First Name");
                        break;
                    case 2:
                        run.SetText("Last Name");
                        break;
                    case 3:
                        run.SetText("Email");
                        break;
                    case 4:
                        run.SetText("Right");
                        break;
                    case 5:
                        run.SetText("Birth Date");
                        break;
                    case 6:
                        run.SetText("Join Date");
                        break;
                    case 7:
                        run.SetText("Address");
                        break;
                    case 8:
                        run.SetText("Phone Number");
                        break;
                    default:
                        run.SetText("Function");
                        break;
                }
            }

            int i = 1;
            foreach (var item in list) {
                table.CreateRow();
                XWPFTableRow row = table.Rows[i];
                i++;
                for (int celula = 0; celula < 10; celula++) {
                    XWPFParagraph p = row.GetCell(celula).Paragraphs[0];
                    XWPFRun run = p.CreateRun();
                    run.FontFamily = "Times New Roman";
                    run.FontSize = 9;
                    switch (celula)
                    {
                        case 0:
                            run.SetText(item.department.DepartmentName);
                            break;
                        case 1:
                            run.SetText(item.FirstName);
                            break;
                        case 2:
                            run.SetText(item.LastName);
                            break;
                        case 3:
                            run.SetText(item.Email);
                            break;
                        case 4:
                            run.SetText(item.IntRight.ToString());
                            break;
                        case 5:
                            run.SetText(item.BirthDate.ToString("dd.MM.yyyy"));
                            break;
                        case 6:
                            run.SetText(item.DateOfJoin.ToString("dd.MM.yyyy"));
                            break;
                        case 7:
                            run.SetText(item.Address);
                            break;
                        case 8:
                            run.SetText(item.PhoneNo);
                            break;
                        default:
                            run.SetText(item.Function);
                            break;
                    }
                }

            }

           
            return doc;
        }

        public ActionResult ExportPDF()
        {
            AppContext ctx = new AppContext();
            Employee emp = (Employee)Session["user"];
            List<Employee> list = ctx.Employees.ToList();
            XWPFDocument document=CreateDocument(list);

            System.IO.FileStream out1 = new System.IO.FileStream(@"C:\Users\Ana\source\repos\EmployeeManagementPlatform\EmployeeManagementPlatform\Utils\document.docx", System.IO.FileMode.Create);
            document.Write(out1);
            out1.Close();
            byte[] fileBytes = System.IO.File.ReadAllBytes(@"C:\Users\Ana\source\repos\EmployeeManagementPlatform\EmployeeManagementPlatform\Utils\document.docx");
            string fileName = "extras.docx";

            string fullPath = Request.MapPath(@"~\Utils\document.docx");
            if (System.IO.File.Exists(fullPath))
            {
                System.IO.File.Delete(fullPath);
            }

            ctx.Logs.Add(new Logs("Exported doc", "User exported doc", System.DateTime.Now, "informational", emp.LastName + " " + emp.FirstName));
            ctx.SaveChanges();
            return File(fileBytes, System.Net.Mime.MediaTypeNames.Application.Octet, fileName);
        }
        #endregion

    }
}