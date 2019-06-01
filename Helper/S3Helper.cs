using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using Amazon;
using Amazon.S3;
using Amazon.S3.Model;
using Amazon.S3.Transfer;
using DotNetBucketMvc.Common;

namespace DotNetBucketMvc.Helper
{
   public  static class S3Helper  
    {  
      
          public static bool sendMyFileToS3(HttpPostedFileBase file,  string fileNameInS3)  
        {
            try
            {
                               var S3Config = new AmazonS3Config
                {
                    RegionEndpoint = RegionEndpoint.USEast1, //its default rekeygion set by amazon 
                 };
            IAmazonS3 client= new AmazonS3Client(AppConfig.AWSAccessKey, AppConfig.AWSSecretKey,S3Config);
            TransferUtility utility = new TransferUtility(client);  
            TransferUtilityUploadRequest request = new TransferUtilityUploadRequest();  
 
            request.BucketName = AppConfig.AWSProfileName + @"/ProfilePics" ; 
            request.Key = fileNameInS3; //file name up in S3  
            request.InputStream = file.InputStream;  
            request.ContentType = file.ContentType;
             request.FilePath= Path.GetFileName(file.FileName);
            utility.Upload(request); //commensing the transfer  

            }
            catch (Exception ex)
            {

                throw;
            }
             
  
            return true; //indicate that the file was sent  
        } 
       public static string UploadToS3(HttpPostedFileBase file,string KeyInS3)
        {
            string key=string.Empty;
       if(file != null)
            {
                 try
            {
                var S3Config = new AmazonS3Config
                {
                    RegionEndpoint = RegionEndpoint.USEast1, //its default rekeygion set by amazon 
                 };

                        AmazonS3Client client;
             
                  key= $"UploadToS3/{file.FileName}";
                    key=KeyInS3;
                

                using (client =  new AmazonS3Client(AppConfig.AWSAccessKey, AppConfig.AWSSecretKey,S3Config))
                {
                    var request = new PutObjectRequest()
                    {
                      BucketName=  AppConfig.AWSProfileName ,
                        CannedACL = S3CannedACL.PublicRead,
                        Key = key, //file name up in S3  
                        InputStream = file.InputStream,
                           FilePath = file.FileName,
                           ContentType = file.ContentType
                    };

                    client.PutObject(request);
                }
              

                 // var url=$"{_baseUploadUrl}{key}";
                 // save database await _userService.SaveImageURL(userId, url
                  
            }
            catch (Exception ex)
            {
                

            }
            }
           
            return key; //RedirectToAction("Index");
        }
        public static string UploadFile(
            string bucketname,
            string bucketUrl,
            S3CannedACL permissions,
            S3StorageClass storageclass,
            HttpPostedFileBase file
            )
        {

            try
            {
                   var S3Config = new AmazonS3Config
                    {
                        RegionEndpoint = RegionEndpoint.USEast1, //its default region set by amazon 
                    };
                var s3Client =  new AmazonS3Client(AppConfig.AWSAccessKey, AppConfig.AWSSecretKey,S3Config);
                PutObjectRequest putRequest = new PutObjectRequest
                {
                    BucketName = bucketname,
                    Key = file.FileName,
                    StorageClass = storageclass,
                    CannedACL = permissions,
                    ContentType = file.ContentType
                };

                //putRequest.Metadata.Add("size", file.metadane.size.ToString());
                //putRequest.Metadata.Add("name", file.metadane.name);
                //putRequest.Metadata.Add("mime", file.metadane.mime);
                putRequest.InputStream = file.InputStream;

                PutObjectResponse response =  s3Client.PutObject(putRequest);

                return "ok";
            }
            catch (Exception)
            {

                throw;
            }
        }
        public static string GetFileObject( string objectKey)
        {
            GetPreSignedUrlRequest request = new GetPreSignedUrlRequest
            {
                BucketName = AppConfig.AWSProfileName,
                Key = objectKey,
                Expires = DateTime.Now.AddMinutes(5)
            };
              var S3Config = new AmazonS3Config
                {
                    RegionEndpoint = RegionEndpoint.USEast1, //its default rekeygion set by amazon 
                 };
             var  s3Client = new AmazonS3Client(S3Config);
          
            string url = s3Client.GetPreSignedURL(request); 
            return url;
        }
    }
}