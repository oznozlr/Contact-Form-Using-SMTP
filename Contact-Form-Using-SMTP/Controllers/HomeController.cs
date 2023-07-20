using Contact_Form_Using_SMTP.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using System.Net.Mail;
using System.Net;

namespace Contact_Form_Using_SMTP.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }
        public IActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Index(Email model)
        {

            MailMessage mailgonder = new MailMessage();
            mailgonder.To.Add("ozlrozn@hotmail.com");
            mailgonder.From = new MailAddress("ozlrozn@hotmail.com");
            mailgonder.Subject = "Sayfadan mesajınız var." + model.Baslik;
            mailgonder.Body = model.AdSoyad + " kişisinden gelen mesajın içeriği <br /> " + model.Icerik;
            mailgonder.IsBodyHtml = true;
            SmtpClient smtp = new SmtpClient();
            smtp.Credentials = new NetworkCredential("ozlrozn@hotmail.com", "Ozan_370");
            smtp.EnableSsl = true;
            smtp.Port = 587;
            smtp.Host = "smtp.office365.com";




            try
            {
                smtp.Send(mailgonder);
                TempData["Message"] = "Mesaj İletildi.";

            }
            catch (Exception ex)
            {

                TempData["Message"] = "Mesaj İletilemedi." + ex.Message;
            }


            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
