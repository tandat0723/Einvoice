using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using SenVang.Customer.Models.Request;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Mail;
using System.Text;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;
using System.Data;
using System.Data.SqlClient;
using System.Net.Mime;
using System.IO;
using System.Threading;
using SenVang.Dashboard.Models.Response;
using SenVang.Dashboard.Models.Request;
using RestSharp;

namespace SenVang.Customer.Controllers
{

    public class HomeController : BaseController
    {

        public ActionResult Home()
        {
            return View();
        }



        public ActionResult Index()
        {
            string url = ConfigurationManager.AppSettings["UrlApi"].ToString();

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(url);
                var responseCheck = client.GetAsync("loadbranchlist");
                responseCheck.Wait();
                var checkResult = responseCheck.Result;
                if (checkResult.IsSuccessStatusCode)
                {
                    var res = checkResult.Content.ReadAsAsync<Response>();
                    res.Wait();
                    ViewBag.BranchName = res.Result;
                    ViewBag.BranchId = res.Result;

                }
            }
            return View();
        }

        [HttpPost]
        public JsonResult ReCaptcha(string response)
        {
            string secretKey = "6Le52YEhAAAAAO2kol41-A2UizBjhGoCbLRNnuoT";
            var client = new WebClient();
            var res = client.DownloadString(string.Format("https://www.google.com/recaptcha/api/siteverify?secret={0}&response={1}", secretKey, response));
            var obj = JObject.Parse(res);
            var status = (bool)obj.SelectToken("success");
            ViewBag.Message = status ? "Xác minh thành công" : "Vui lòng xác minh";
            return Json(res);
        }


        [HttpPost]
        public JsonResult EinvoiceWaiting(string jpost)
        {
            string url = ConfigurationManager.AppSettings["UrlApi"].ToString();
            BillHeader header = JsonConvert.DeserializeObject<BillHeader>(jpost);
            Response response = null;
            BillRequest request = new BillRequest
            {
                BranchId = header.Header.BranchId,
                TicketCode = header.Header.TicketCode,
                CompanyName = header.Header.CompanyName,
                CompanyAddress = header.Header.CompanyAddress,
                CompanyTax = header.Header.CompanyTax,
                Email = header.Header.Email,
                Phone = header.Header.Phone
            };

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(url);
                var responseTask = client.PostAsJsonAsync("einvoicewaiting", request);
                responseTask.Wait();
                var checkResult = responseTask.Result;

                if (checkResult.IsSuccessStatusCode)
                {
                    var res = checkResult.Content.ReadAsAsync<Response>();
                    res.Wait();
                    response = res.Result;
                    ViewBag.MessageErrors = response.MessageErrors;
                    ViewBag.Success = response.Success;
                }
            }
            return Json(response, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult AjaxMethod(string response)
        {
            //string secretKey = "6Le52YEhAAAAAH33NAkWmzgiEXAvgR60o_IM3Dgv";
            //var client = new WebClient();
            //var res = client.DownloadString(string.Format("https://www.google.com/recaptcha/api/siteverify?secret={0}&response={1}", secretKey, response));
            //var obj = JObject.Parse(res);
            //var status = (bool)obj.SelectToken("success");
            //ViewBag.Message = status ? "Xác minh thành công" : "Vui lòng xác minh";
            return Json(response);
        }

        public JsonResult CheckLimiteInvoice(string TaxCode)
        {
            try
            {
                string url = ConfigurationManager.AppSettings["UrlApi"].ToString();
                Response response = null;

                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(url);

                    var responseTask = client.PostAsJsonAsync("checklimiteinvoice", new { TaxCode = TaxCode });
                    responseTask.Wait();

                    var result = responseTask.Result;

                    if (result.IsSuccessStatusCode)
                    {
                        var res = result.Content.ReadAsAsync<Response>();
                        res.Wait();
                        response = res.Result;
                        ViewBag.MessageErrors = response.MessageErrors;
                        ViewBag.Success = response.Success;
                    }
                }

                return Json(response, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(ex.ToString(), JsonRequestBehavior.AllowGet);
            }
        }
        public JsonResult CheckTaxCode(string taxcode)
        {
            try
            {
                if (string.IsNullOrEmpty(taxcode)) return Json("-1", JsonRequestBehavior.AllowGet);   
                if(taxcode.Length<5) return Json("-1", JsonRequestBehavior.AllowGet);
                string url = "http://mst.minvoice.com.vn/api/System/SearchTaxCode?tax=" + taxcode; 
                var client = new  RestClient(url);
                var request = new RestRequest();
                request.Method = Method.Get; 
                var response = client.Execute(request);
                var str = response.Content; // raw content as string    
                TaxCustInfo result = JsonConvert.DeserializeObject<TaxCustInfo>(str);
                if(string.IsNullOrEmpty(result.ma_so_thue)) return Json("-1", JsonRequestBehavior.AllowGet);
                return Json(result, JsonRequestBehavior.AllowGet);
            }
            catch(Exception ex)
            {
                return Json("-1", JsonRequestBehavior.AllowGet);
            }
        }

    }
}