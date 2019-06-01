using DotNetBucketMvc.Common;
using DotNetBucketMvc.Helper;
using DotNetBucketMvc.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DotNetBucketMvc.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        public string GetPhotos() 
        { 
            var userId=User.Identity.GetUserId();
            var fileName = HttpContext.Server.MapPath(@"~/content/images/noprofile.png"); 
            if (userId != null) 
            { 

                if (User.Identity.IsAuthenticated) 
                {
                    var bdUsers = HttpContext.GetOwinContext().Get<ApplicationDbContext>(); 
                    var user = bdUsers.Users.Where(x => x.Id == userId).FirstOrDefault(); 

            if (user !=null && AppConfig.S3Enable)
            {
                   
                var key= S3Helper.GetFileObject(user.ProfileKey);
            return  key; 
                    
            }
                }
            }
            return fileName;
                
            

        }
        public FileContentResult UserPhotos() 
        { 
              var userId=User.Identity.GetUserId();
              var fileName = HttpContext.Server.MapPath(@"~/content/images/noprofile.png"); 
               if (userId == null) 
                { 
                    
 
                    byte[] imageData = null; 
                    FileInfo fileInfo = new FileInfo(fileName); 
                    long imageFileLength = fileInfo.Length; 
                    FileStream fs = new FileStream(fileName, FileMode.Open, FileAccess.Read); 
                    BinaryReader br = new BinaryReader(fs); 
                    imageData = br.ReadBytes((int)imageFileLength); 
                     
                    return File(imageData, "image/png"); 
 
                } 
                
             if (User.Identity.IsAuthenticated) 
              {
                 var bdUsers = HttpContext.GetOwinContext().Get<ApplicationDbContext>(); 
                 //var userImage = bdUsers.Users.Where(x => x.Id == userId).FirstOrDefault(); 
                 var user = bdUsers.Users.Where(x => x.Id == userId).FirstOrDefault(); 

               
                
                      return new FileContentResult(user.UserPhoto, "image/jpeg"); 
             
            }
             else 
             { 
               
                byte[] imageData = null; 
                FileInfo fileInfo = new FileInfo(fileName); 
                long imageFileLength = fileInfo.Length; 
                FileStream fs = new FileStream(fileName, FileMode.Open, FileAccess.Read); 
                BinaryReader br = new BinaryReader(fs); 
                imageData = br.ReadBytes((int)imageFileLength);                  
                return File(imageData, "image/png"); 
                
            }

              
             

          
        } 
    }

}