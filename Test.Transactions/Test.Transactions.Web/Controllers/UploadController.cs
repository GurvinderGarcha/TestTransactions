using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Test.Transactions.Core;
using WebApplication2.Models;

namespace WebApplication2.Controllers
{
    public class UploadController : Controller
    {
        private readonly IParserService _parserService;
        private readonly List<string> _allowedExtensions = new List<string>() { ".csv" };

        public UploadController(IParserService service)
        {
            _parserService = service;
        }

        [HttpGet]
        public ActionResult Upload()
        {
            return View(new UploadFileViewModel());
        }

        [HttpPost]
        public async Task<ActionResult> Upload(HttpPostedFileBase file)
        {
            var model = new UploadFileViewModel();
            if (file == null || file.ContentLength <= 0)
            {
                ModelState.AddModelError("File", "Invalid File request received ");
                return View(model);
            }

            if (!_allowedExtensions.Contains(file.FileName.Substring(file.FileName.LastIndexOf('.'))))
            {
                ModelState.AddModelError("File", "Please file of type: " + string.Join(", ", _allowedExtensions));
                return View(model);
            }


            try
            {
                var path = Path.Combine(Path.GetTempPath(), Path.GetFileName(file.FileName));
                file.SaveAs(path);
                var results = Task.Run(() => _parserService.ParseFile(path));
                model = new UploadFileViewModel() { Result = results.Result };
                ViewBag.Message = "File uploaded successfully";
            }
            catch (Exception ex)
            {
                ViewBag.Message = "ERROR:" + ex.Message.ToString();
            }

            return View(model);
        }
    }
}