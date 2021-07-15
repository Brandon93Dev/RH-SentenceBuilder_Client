using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Web.Services;
using SentenceClient.Models;

namespace SentenceClient.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            ViewBag.RenderIntro = true;
            Service svc = new Service();
            IndexModel idxm = new IndexModel();
            idxm.WordList = svc.GetAllWordLists();
            idxm.SentenceList = svc.GetAllSentences();
            return View("Index", idxm);
        }

        [HttpPost]
        [WebMethod]
        public bool StoreSentence(string fullSentence)
        {
            Service svc = new Service();
            bool didpost = svc.PostSentence(fullSentence);
            return didpost;
        }

    }
}