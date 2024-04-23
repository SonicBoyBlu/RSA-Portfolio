using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Acme.Methods;
using Acme.Models;
using Acme.Models.ExternalApplication;
using Acme.Services.Vendors.BambooHR;

namespace Acme.Controllers
{
    public class UsersController : Controller
    {
        private Methods.UserService _userService { get; set; }

        public UsersController()
        {
            _userService = new Methods.UserService();
        }

        // GET: Users
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public JsonNetResult FetchUsers()
        {
            var users = _userService.GetList();
            return new JsonNetResult(users);
        }

        [HttpGet]
        public async Task<JsonNetResult> FetchEscapiaOwner(int id)
        {
            Task<Escapia.Owner> owner = _userService.GetEscapiaOwner(id);
            return new JsonNetResult(owner);
        }
        [HttpGet]
        public JsonNetResult FetchUserJson(int UserID)
        {
            User user = _userService.GetUserById(UserID);
            return new JsonNetResult(user);
        }
        [HttpGet]
        public JsonNetResult FetchUserTypes()
        {
            List<ViewModels.Common.OptionPair> user = _userService.GetUserTypes();
            return new JsonNetResult(user);
        }


        public string FetchBambooUser(int id)
        {
            var api = new Acme.Services.Vendors.BambooHR.Api();
            EmployeeRecord employee = api.GetEmployeeBasic(id);
            return Newtonsoft.Json.JsonConvert.SerializeObject(employee);

        }
        public ActionResult FetchBambooUsersDropDown()
        {
            var api = new Acme.Services.Vendors.BambooHR.Api(); // Vendors.BambooHR.Api();
            List<ViewModels.Common.IdTitlePair> employees = api.GetEmployeesForDropDown();
            return PartialView(employees);

            // return Newtonsoft.Json.JsonConvert.SerializeObject(employees);
        }

        [HttpPost]
        public async Task<int> UpdateUser(Models.UserUpdate form)
        {
            form.ipaddress = Request.UserHostAddress;
            var user = _userService.Update(form);

            return user.UserID;

        }
        [HttpPost]
        public async Task<bool> UpdatePassword(Models.UserUpdate form)
        {
            form.ipaddress = Request.UserHostAddress;
            return _userService.UpdatePassword(form);

        }
        [HttpPost]
        public async Task<bool> UserActiveToggle(int UserID, bool active)
        {
            if (active) _userService.Deactivate(UserID);
            if (!active) _userService.Deactivate(UserID);

            return true;
        }

        [HttpPost]
        public async Task<JsonNetResult> CreateUser(Models.UserUpdate form)
        {
            form.ipaddress = Request.UserHostAddress;
            var user = _userService.AddUser(form);

            return new JsonNetResult(user);
        }

        public ActionResult UserModal()
        {
            return PartialView();
        }

        public ActionResult UserNewModal()
        {
            return PartialView();
        }
    }
}
