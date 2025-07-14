using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Security.Cryptography;
using System.Text;
using BambooHrClient;
using Newtonsoft.Json;

namespace Acme.Services.Vendors.BambooHR
{
    public interface IBambooHrAPI
    {
        //EmployeeList GetEmployees();
    }

    public class Api : IBambooHrAPI
    {
        private HttpWebRequest _api;
        public Api()
        {
        }

        private string getApi(string view, RequestParameters parameters = null, string method = "get")
        {
            //string view = "employees/directory";
            string body = string.Empty;

            string url = string.Format(GlobalSettings.BambooHR.Credentials.BambooApiUrl, view);
            if (view.Contains("https://"))
            {
                url = view;
            }
            else
            {
                url += "?format=JSON";

                if (parameters != null)
                {
                    foreach (var p in parameters)
                    {
                        url += string.Format("&{0}={1}", p.Key, p.Value);
                    }
                }
            }
            //string url = string.Format(GlobalSettings.BambooHR.Credentials.BambooApiUrl, GlobalSettings.BambooHR.Credentials.BambooApiKey, view);

            //*
            try
            {
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
                request.Method = method;
                //request.Headers["x"] = GlobalSettings.BambooHR.Credentials.BambooApiKey;
                request.Headers.Add("Authorization", "Basic " + Convert.ToBase64String(Encoding.GetEncoding("ISO-8859-1").GetBytes(GlobalSettings.BambooHR.Credentials.BambooApiKey + ":x")));
                request.ContentType = "application/json";
                request.Accept = "application/json";
                request.UserAgent = "Acme Portal v1.0";
                //String test = String.Empty;
                ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls11 | SecurityProtocolType.Tls12;

                _api = request;

                //*
                using (HttpWebResponse response = (HttpWebResponse)request.GetResponse())
                {
                    Stream dataStream = response.GetResponseStream();
                    StreamReader reader = new StreamReader(dataStream);
                    //test = reader.ReadToEnd();
                    body = reader.ReadToEnd();
                    reader.Close();
                    dataStream.Close();
                }
                //var json = Newtonsoft.Json.JsonConvert.DeserializeObject<BambooHrEmployees>(test);
                //*/
            }
            catch (Exception ex)
            {

            }
            //*/
            return body;
        }

        public EmployeeRecord GetEmployee(string Username, string Fields = null, bool IncludePhoto = true)
        {
            var emp = GetEmployees(false).Employees.Where(e => e.WorkEmail == Username).FirstOrDefault();
            try
            {
                return GetEmployee(emp.Id, Fields, IncludePhoto);
            }
            catch (Exception ex)
            {
                return new EmployeeRecord();
            }
        }
        public EmployeeRecord GetEmployee(int EmployeeID, string Fields = null, bool IncludePhoto = true)
        {
            EmployeeRecord data = new EmployeeRecord();
            RequestParameters fields = new RequestParameters();
            if (!string.IsNullOrEmpty(Fields))
            {
                //fields = new RequestParameters();
                fields.Add("fields", Fields);
            }
            else
            {
                // get our default fields
                fields.Add("fields", "id,employeeNumber,firstName,lastName,department,jobTitle,bestEmail,workemail,hireDate,lastChanged,mobilePhone,supervisor,supervisorId,isPhotoUploaded");
            }

            string json = getApi("employees/" + EmployeeID, fields);
            if (json.IsNullOrEmpty())
                return new EmployeeRecord();

            data = JsonConvert.DeserializeObject<EmployeeRecord>(json);
            if (data.IsNull())
                return new EmployeeRecord();

            if (IncludePhoto == true)
                data.PhotoURL = GetEmployeePhotoUrl(data.WorkEmail.ToStringOrDefault());
            return data;
        }

        public EmployeeRecord GetEmployeeBasic(int EmployeeID)
        {
            var data = (GetEmployee(EmployeeID));

            // Strip out data from json view
            EmployeeRecord employee = new EmployeeRecord();
            employee.LastName = data.LastName;
            employee.FirstName = data.FirstName;
            employee.WorkEmail = data.WorkEmail;
            employee.Id = data.Id;

            return employee;

        }

        public EmployeeList GetEmployees(bool IncludePhoto = true)
        {
            EmployeeList data = new EmployeeList();
            string json = getApi("employees/directory");
            data = JsonConvert.DeserializeObject<EmployeeList>(json);
            if (IncludePhoto)
            {
                foreach (var e in data.Employees)
                {
                    //if (string.IsNullOrEmpty(e.PhotoURL))
                        e.PhotoURL = GetEmployeePhotoUrl(e.WorkEmail);
                }
            }
            return data;
        }
        public List<ViewModels.Common.IdTitlePair> GetEmployeesForDropDown()
        {
            EmployeeList data = new EmployeeList();
            string json = getApi("employees/directory");
            data = JsonConvert.DeserializeObject<EmployeeList>(json);

            // Get Users already in Bamboo
            var extAppService = new Methods.ExternalApplicationService();
            var ExternalApplications = extAppService.ByExternalApplicationId(GlobalSettings.ExternalApplication.BambooHR);

            var userIds = (from e in ExternalApplications
                    where e.ConfigurationMeta.IsNotNullOrEmpty()
                    select e.ConfigurationMeta.ToInt32OrDefault()
                ).ToList();

            List<ViewModels.Common.IdTitlePair> ddEmployee = new List<ViewModels.Common.IdTitlePair>();

            if (data.IsNull())
                return ddEmployee;


            foreach (var item in data.Employees)
            {
                var employee = new ViewModels.Common.IdTitlePair();
                employee.Title = item.LastName + ", " + item.FirstName;
                
                // Set employeeId = 0 if already BambooUser is AcmePortal User
                employee.Id = (userIds.Contains(item.Id) ? 0 : item.Id) ;
                
                ddEmployee.Add(employee);
            }

            return ddEmployee;
        }


        public EmployeeList GetPayrollHours()
        {
            EmployeeList data = new EmployeeList();
            RequestParameters req = new RequestParameters();
            req.Add("payIntervalFilter%5BpayScheduleFilter%5D", "7");
            req.Add("payIntervalFilter%5BpayPeriodId%5D", "18");
            string json = getApi("https://acmehouseco.bamboohr.com/reports/payroll-hours?format=json&payIntervalFilter%5BpayScheduleFilter%5D=7&payIntervalFilter%5BpayPeriodId%5D=18");
            //string json = getApi("/reports/payroll-hours", req, "post");
            return data;
        }

        public string GetEmployeePhotoUrl(int EmployeeID)
        {
            try
            {
                var emp = GetEmployees(false).Employees.Where(e => e.Id == EmployeeID).FirstOrDefault();
                return GetEmployeePhotoUrl(emp.WorkEmail);
                /*
                var hashedEmail = Utilities.Hash(emp.WorkEmail);
                var url = string.Format(GlobalSettings.BambooHR.Credentials.BambooCompanyUrl + "/employees/photos/?h={0}", hashedEmail);

                return url;
                */
            }
            catch { return string.Empty; }
        }
        public string GetEmployeePhotoUrl(string email)
        {
            int employeeid = 0;
            int.TryParse(email, out employeeid);
            if (employeeid > 0)
                return GetEmployeePhotoUrl(employeeid);
            else
            {
                var hashedEmail = Utilities.Hash(email);
                var url = string.Format(GlobalSettings.BambooHR.Credentials.BambooCompanyUrl + "/employees/photos/?h={0}", hashedEmail);

                return url;
            }
        }

        public EmployeesTimeOff GetEmployeesTimeOff(bool IncludePhoto = true)
        {
            return GetEmployeesTimeOff(DateTime.MinValue, DateTime.MinValue);
        }
        public EmployeesTimeOff GetEmployeesTimeOff(DateTime DateStart, DateTime DateEnd, bool IncludePhoto = true)
        {
            EmployeesTimeOff data = new EmployeesTimeOff();
            var p = new RequestParameters();
            if (DateStart != DateTime.MinValue)
                p.Add("start", DateStart.ToString("yyyy-MM-dd"));
            if (DateEnd != DateTime.MinValue)
                p.Add("end", DateEnd.ToString("yyyy-MM-dd"));
            string json = getApi("time_off/whos_out", p);
            data = JsonConvert.DeserializeObject<EmployeesTimeOff>(json);
            if (IncludePhoto && data != null)
            {
                foreach (var e in data)
                {
                    e.PhotoURL = GetEmployeePhotoUrl(e.EmployeeId);
                }
            }

            return data;
        }
        private class Utilities
        {
            /// <summary>
            /// Mostly from Stack Overflow post: http://stackoverflow.com/a/24031467/57698
            /// </summary>
            /// <param name="input"></param>
            /// <returns></returns>
            internal static string Hash(string input)
            {
                if (!string.IsNullOrEmpty(input))
                {
                    try
                    {
                        var asciiBytes = Encoding.ASCII.GetBytes(input.Trim().ToLower());
                        var hashedBytes = MD5.Create().ComputeHash(asciiBytes);
                        var hashedString = BitConverter.ToString(hashedBytes).Replace("-", "").ToLower();

                        return hashedString;
                    }
                    catch { return string.Empty; }
                }
                else return string.Empty;
            }
        }
    }

    public class EmployeeList : BambooHrResponse
    {
        public List<EmployeeRecord> Employees { get; set; }
    }

    public class EmployeeRecord : BambooHrClient.Models.BambooHrEmployee
    {
        public string PhotoURL { get; set; }
    }

    public class EmployeesTimeOff : List<EmployeeOffDetail>
    {

    }


    public class EmployeeOffDetail : BambooHrClient.Models.BambooHrWhosOutInfo
    {
        public string PhotoURL { get; set; }
    }

    public class BambooHrResponse
    {
        private object fields { get; set; }
    }

    public class RequestParameters : Dictionary<string, string>
    {
    }
}