﻿using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace goods_server.Service.InterfaceService
{
    public interface IAzureBlobStorage
    {
        Task<string> UploadFileAsync(string containerName, IFormFile file);
    }
}
