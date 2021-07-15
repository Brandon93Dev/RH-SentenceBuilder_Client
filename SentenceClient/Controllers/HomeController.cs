using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using SentenceClient.Models;

namespace SentenceClient.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            ViewBag.RenderIntro = true;
            Service svc = new Service();
            WordList wl = svc.GetAllWordLists();
            return View("Index",wl);
        }

    }
}