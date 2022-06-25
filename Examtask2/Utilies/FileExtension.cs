using Microsoft.AspNetCore.Http;
using System;
using System.IO;
using System.Threading.Tasks;

namespace Examtask2.Utilies
{
    public static class FileExtension
    {
        public static async Task<string> SaveFileAsync(this IFormFile file,string savepath)
        {
            string fileName=Guid.NewGuid().ToString()+file.FileName;
            string path= Path.Combine(savepath,fileName);
            using(FileStream fs=new FileStream(path, FileMode.Create))
            {
                await fs.CopyToAsync(fs);
            }
            return fileName;
        }
        public static void DeleteFile(string path)
        {
            if (System.IO.File.Exists(path))
            {
                System.IO.File.Delete(path);
            }
        }
    }
}
