using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo.BusinessLogic.Services.AttatchementService
{
    public  class AattachementService : IAttachementService
    {
        //the extention of file
        List<string> allowedExtention = [".png", ".jpg", ".jpeg"];
        //the size of file 
        const int maxsize = 2_097_152;

        public string? Upload(IFormFile file, string FolderName)
        {
            //1.Check Extention 
            var extention = Path.GetExtension(file.FileName);//.png
            if (!allowedExtention.Contains(extention)) return null;


            //2.Check size 
            if(file.Length==0||file.Length>maxsize) return null;



            //3.Get Located Path
            var folderPath = $"{Directory.GetCurrentDirectory()}\\wwwroot\\Files\\{FolderName}";
            //method more save 
            //var folderPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "Files", FolderName);

            // التأكد من وجود المجلد
            if (!Directory.Exists(folderPath))
                Directory.CreateDirectory(folderPath);

            //4.Make the Attachement Name Uniqe --Guid 
            var fileName = $"{Guid.NewGuid()}_{file.FileName}";


            //5.Get File Path
            
            var FilePath=Path.Combine(folderPath, fileName);//File Location 

            //6.create File Straem to copy file  [Unmanaged]
            using FileStream fs = new FileStream(FilePath, FileMode.Create);  //we now open stream 


            //7.Use stream to copy File 
            file.CopyTo(fs);


            //8.Return Filemname to store database 
            return fileName;

        }


        public bool Delete(string filePath)
        {
            if (!File.Exists(filePath)) return false; 
            else
            {
                File.Delete(filePath);
                return true;


            }
        }
          
        
    }
}
