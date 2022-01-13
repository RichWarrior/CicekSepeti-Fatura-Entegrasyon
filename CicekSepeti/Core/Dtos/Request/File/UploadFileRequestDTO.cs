using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;

namespace Core.Dtos.Request.File
{
    public class UploadFileRequestDTO
    {
        public List<IFormFile> File { get; set; }
        public DateTime TermStartDate { get; set; }
        public DateTime TermEndDate { get; set; }
    }
}
