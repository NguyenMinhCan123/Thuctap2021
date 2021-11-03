using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.IO;
using Microsoft.AspNet.Identity;
using System.Net;
using TTQK7;

namespace TTQK7.Controllers
{  
    public class UploadController: Controller
    {
        private TTQK7Entities db = new TTQK7Entities();
        // GET: Upload  
        public ActionResult Index()
        {
            
            return View();
        }
        [HttpGet]
        public ActionResult UploadFile()
        {
            var path = Server.MapPath("~/UploadedFiles/");

            var dir = new DirectoryInfo(path);

            var files = dir.EnumerateFiles().Select(f => f.Name);
            ViewBag.idDonVi = new SelectList(db.tblDonVi, "DonViID", "TenDonVi");
            ViewBag.UpFiles = db.tblUploadFile.ToList();
            return View(files);
        }
        [HttpPost]
        public ActionResult UploadFile(HttpPostedFileBase file)
        {
            //try
            //{
            //    if (file.ContentLength > 0)
            //    {
            //        string _FileName = Path.GetFileName(file.FileName);
            //        string _path = Path.Combine(Server.MapPath("~/UploadedFiles"), _FileName);
            //        file.SaveAs(_path);
            //    }
            //    ViewBag.Message = "File Uploaded Successfully!!";
            //    return View();
            //}
            //catch
            //{
            //    ViewBag.Message = "File upload failed!!";
            //    return View();
            //}
            //Kiểm tra nếu trùng tên file thì phải tạo ra tên khác
           
             
        
              
            var relativePath = "~/UploadedFiles/" + file.FileName;
            var absolutePath = HttpContext.Server.MapPath(relativePath);
            if (!System.IO.File.Exists(absolutePath))
            {
                var path = Path.Combine(Server.MapPath("~/UploadedFiles/"), file.FileName);
                var data = new byte[file.ContentLength];
                file.InputStream.Read(data, 0, file.ContentLength);

                using (var sw = new FileStream(path, FileMode.Create))
                {
                    sw.Write(data, 0, data.Length);
                }
            }else
            {
                Random number = new Random(1000);
                int num = number.Next();
                var path = Path.Combine(Server.MapPath("~/UploadedFiles/"), DateTime.Now.ToString() +"_"+ num.ToString() + "_" + file.FileName);
                var data = new byte[file.ContentLength];
                file.InputStream.Read(data, 0, file.ContentLength);

                using (var sw = new FileStream(path, FileMode.Create))
                {
                    sw.Write(data, 0, data.Length);
                }
            }    
                   
            //Sau khi upload file thành công sẽ lưu vào bảng Upload
            tblUploadFile obj = new tblUploadFile();
            obj.DonViID = 1;
           
            return RedirectToAction("UploadFile");
        }
    }
}