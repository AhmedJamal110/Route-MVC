using Microsoft.AspNetCore.Http;
using System;
using System.IO;

namespace Demo.PL.Helper
{
    public static class DocumentSetting
    {

        public static string UploadFile(IFormFile file, string FolderName)
        {

            // 1- folderPath
            var folderPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\fils", FolderName);

            // 2- file Name and make it unique

            var failName = $"{Guid.NewGuid()}{file.FileName}";

            // 3- file path

            var failPath = Path.Combine(folderPath, failName);

            // save file as streem  

            using (
                    var fs = new FileStream(failPath, FileMode.Create)
                )
                file.CopyTo(fs);
            

            return failName;
        }


        public static void DelelteFile(string FileName , string FolderName)
        {
            var FilePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\fils", FolderName, FileName);

            if (File.Exists(FilePath))
                File.Delete(FilePath);

        }
    
    }
}
