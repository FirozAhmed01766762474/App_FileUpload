using Microsoft.AspNetCore.Mvc;

namespace App_FileUpload.Controllers
{
    public class FileController : Controller
    {

        private readonly IWebHostEnvironment _whe;

        public FileController( IWebHostEnvironment whe)
        {
                _whe = whe;
        }
        public IActionResult Index()
        {
            string msg = "";

            if (TempData["msg"] != null)
            {
                msg = TempData["msg"].ToString();
            }
            ViewBag.msg = msg;
            return View();
        }
        //[HttpPost]
        //public async Task<IActionResult> UploadFile(IFormFile filetoupload)
        //{
        //    string msg = "";
        //    if(filetoupload != null)
        //    {
        //        if(filetoupload.Length > 0)
        //        {
        //            string ext = Path.GetExtension(filetoupload.FileName);

        //            if(ext == ".pdf" || ext ==".jpg"||  ext ==".doc" ||ext==".png"||ext =="gif")
        //            {

        //                string folder = "myFolder";
        //                string webroot = _whe.WebRootPath;
        //                string fileName = Path.GetFileName(filetoupload.FileName);

        //                string fs = Path.Combine(webroot, folder, fileName);

        //                if(System.IO.File.Exists(fs))
        //                {
        //                    string uf = DateTime.Now.ToString("yyyy-MM-dd-hh-mm-ss");
        //                    uf += Path.GetExtension (filetoupload.FileName);
        //                    fs = Path.Combine(webroot, folder, uf);

        //                }

        //                using (var strm = new FileStream(fs, FileMode.Create))
        //                {
        //                    await filetoupload.CopyToAsync(strm);
        //                    msg = "File Uploded Successfullay";
        //                }



        //            }
        //            else
        //            {
        //                msg = "Please select valid file";
        //            }

        //        }
        //        else
        //        {
        //            msg = "File size must be grater then zero";
        //        }

        //    }
        //    else
        //    {
        //        msg = "Please select a file for Upload";
        //    }

        //    TempData["msg"] = msg;

        //    return RedirectToAction("Index");
        //} 

        [HttpPost]
        public async Task<IActionResult> UploadFile(IFormFile filetoupload)
        {
            string msg = "";
            if (filetoupload != null)
            {
                if (filetoupload.Length > 0)
                {
                    string ext = Path.GetExtension(filetoupload.FileName);
                    if (ext == ".jpg" || ext == ".png" || ext == ".gif" || ext == ".doc" || ext == ".pdf")
                    {
                        string folder = "myfolder";
                        string webroot = _whe.WebRootPath;
                        string filename = Path.GetFileName(filetoupload.FileName);

                        string fs = Path.Combine(webroot, folder, filename);

                        if (System.IO.File.Exists(fs))
                        {
                            string uf = DateTime.Now.ToString("yyyy-MM-dd-hh-mm-ss");
                            uf += Path.GetExtension(filetoupload.FileName);
                            fs = Path.Combine(webroot, folder, uf);
                        }

                        using (var stream = new FileStream(fs, FileMode.Create)) 
                        {
                            await filetoupload.CopyToAsync(stream);
                            msg = "File has been uploaded successfully";
                        }
                    }
                    else
                    {
                        msg = "File Extension is not allowed to upload";
                    }
                }
                else
                {
                    msg = "File has no content";
                }
            }
            else
            {
                msg = "Please select a file to upload";
            }
            TempData["msg"] = msg;
            return RedirectToAction("Index");
        }
        public IActionResult ShowFiles()
        {

            string msg = "";
            string folder = "myFolder";
            string webroot = _whe.WebRootPath;
            string mf = Path.Combine(webroot, folder);
            string[] files = Directory.GetFiles(mf);

            ViewBag.files = files;

            return View();
        }
    }
}
