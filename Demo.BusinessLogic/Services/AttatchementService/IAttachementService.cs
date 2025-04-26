using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo.BusinessLogic.Services.AttatchementService
{
    public interface IAttachementService
    {

        //Upload Logic 
        public string Upload(IFormFile file ,string FolderName);

        //Delete 

        bool Delete(string filePath); 
    }
}
