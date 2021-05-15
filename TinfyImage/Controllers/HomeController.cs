using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TinifyAPI;
using System.Threading.Tasks;
using System.IO;
using System.Text;
using System.Net;

namespace TinfyImage.Controllers
{
    public class HomeController : Controller
    {

        public ActionResult Index()
        {
            return View();
        }
        // Save original and optimized image
        public async Task<ActionResult> OptimizeAsync(HttpPostedFileBase imag)
        {
            var urlPath = Path.Combine(Server.MapPath("~/Images/Orginal"), imag.FileName);
            imag.SaveAs(urlPath);

            Tinify.Key = "nnpQB5zB0HS0czxmS7YTFwTrw35CQwJd";
            var source = Tinify.FromFile(urlPath);
            await source.ToFile(Path.Combine(Server.MapPath("~/Images/Optimized"), "optimized_" + imag.FileName));
            return RedirectToAction("Index");
        }
        //Save one image
        public async Task<ActionResult> OptimizeAsync2(HttpPostedFileBase imag)
        {
            try
            {
                Tinify.Key = "jpR72pbwbnRvxWvKfVjrvxZVDXxD5vR4";
                var urlPath = Path.Combine(Server.MapPath("~/Images"), imag.FileName);
                imag.SaveAs(urlPath);

                var source = Tinify.FromFile(urlPath);
                await source.ToFile(urlPath);
                TempData["Url"] = imag.FileName;
            }
            catch
            {
                TempData["Error"] = "EROORR EROR";
            }
            return RedirectToAction("Index");
        }
        //Save multi image
        public async Task<ActionResult> OptimizeAsync3(HttpPostedFileBase[] imag)
        {
            List<string> forDownload = new List<string>();
            
                Tinify.Key = "jpR72pbwbnRvxWvKfVjrvxZVDXxD5vR4";
                foreach (HttpPostedFileBase item in imag)
                {
                    var urlPath = Path.Combine(Server.MapPath("~/Images"), item.FileName);
                    item.SaveAs(urlPath);

                    var source = Tinify.FromFile(urlPath);
                    await source.ToFile(urlPath);
                    forDownload.Add(item.FileName);
                    TempData["for"] = forDownload;
                }
            return RedirectToAction("Index");
        }
        
    }
}