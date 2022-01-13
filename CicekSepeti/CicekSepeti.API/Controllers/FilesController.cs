using AutoMapper;
using Core.Dtos.Request.File;
using Core.Dtos.Response.File;
using Core.Interfaces;
using Core.Utilities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using File = Core.Entities.File;

namespace CicekSepeti.API.Controllers
{
    /// <summary>
    /// 
    /// </summary>
    public class FilesController : BaseController
    {

        readonly IStringLocalizer<FileResource> l;

        readonly IStringLocalizer<ParameterResource> r;

        readonly IMinioService _minioService;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="_uow"></param>
        /// <param name="_mapper"></param>
        /// <param name="_localizer"></param>
        /// <param name="_l"></param>
        /// <param name="_r"></param>
        public FilesController(
            IUnitOfWork _uow,
            IMapper _mapper,
            IStringLocalizer<BaseResource> _localizer,
            IStringLocalizer<FileResource> _l,
            IStringLocalizer<ParameterResource> _r,
            IMinioService minioService
            )
            : base(_uow, _mapper, _localizer)
        {
            l = _l;
            r = _r;
            _minioService = minioService;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [HttpGet("list")]
        public IActionResult List()
        {
            ListFileResponse response = new ListFileResponse();
            var files = uow.File.List();
            if (!files.Success)
                return NotFound(response);
            response.Files = files.Data
                .OrderByDescending(f => f.CreationDate)
                .Select(f => mapper.Map<ListFileResponse.File>(f))
                .ToList();
            return Ok(response);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        [HttpPost("upload")]
        public IActionResult Upload([FromForm] UploadFileRequestDTO dto)
        {
            UploadResponse response = new UploadResponse();
            var parameterExists = uow.Parameter.FindByName(ParameterConstants.TaxRate);
            if (!parameterExists.Success)
                return NotFound(response, r[parameterExists.Message]);
            var taxParameter = parameterExists.Data;
            var currentUser = CurrentUser;
            using (MemoryStream ms = new MemoryStream())
            {
                var file = dto.File.FirstOrDefault();
                file.CopyTo(ms);
                var fileEntity = new File()
                {
                    Name = file.FileName,
                    TermStartDate = dto.TermStartDate,
                    TermEndDate = dto.TermEndDate,
                    CreatorId = currentUser.Id
                };
                var readSuccess = uow.File.ReadData(ms, fileEntity, taxParameter);
                if (!readSuccess.Success)
                    return NotFound(response);
                if (!uow.SaveChanges())
                    return NotFound(response);
                if (!uow.Commit())
                    return NotFound(response);
                response.Id = fileEntity.Id;
                return Ok(response);
            }
        }

        [HttpGet("download/{id}")]
        public async Task<IActionResult> Download(int id)
        {
            var result = await _minioService.GetLink(id.ToString());
            if (!result.Success)
                return NotFound(result);
            return Ok(result.Data);
        }
    }
}
