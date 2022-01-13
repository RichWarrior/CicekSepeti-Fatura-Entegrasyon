using Core.Dtos.Response.File;
using Core.Interfaces;
using Core.Utilities;
using Core.Utilities.Result;
using Minio;
using Minio.Exceptions;
using System;
using System.Threading.Tasks;

namespace Service.Repositories
{
    public class MinioService : IMinioService
    {
        private MinioClient _minioClient;
        private Lazy<MinioClient> _minio = null;
        private ConnectionInfo _connectionInfo = ConnectionInfo.Instance;

        public MinioService()
        {
            _minio = new Lazy<MinioClient>(() =>
            {
                if (_minioClient == null)
                    _minioClient = new MinioClient(_connectionInfo.MinioIp, _connectionInfo.MinioUserName, _connectionInfo.MinioPassword);
                return _minioClient;
            });
        }       

        public async Task<bool> Upload(string fileName, string filePath, string contentType)
        {
            try
            {
                bool existsFile = false;
                try
                {
                    await _minio.Value.GetObjectAsync(_connectionInfo.CompanyName, fileName, (stream) =>
                    {
                        existsFile = true;
                    });
                }catch(Exception e)
                {
                    Console.WriteLine(e.Message);
                }             

                if (existsFile)
                {
                    await _minio.Value.RemoveObjectAsync(_connectionInfo.CompanyName, fileName);
                }

                await _minio.Value.PutObjectAsync(_connectionInfo.CompanyName, fileName, filePath, contentType);
                return true;
            }catch(MinioException e)
            {
                Console.WriteLine(e.Message);
                return false;
            }            
        }

        public async Task<IDataResult<DownloadResponse>> GetLink(string id)
        {
            try
            {
                var link = await _minio.Value.PresignedGetObjectAsync(_connectionInfo.CompanyName, $"{id}.zip", (int)TimeSpan.FromDays(1).TotalSeconds);
                if (string.IsNullOrEmpty(link))
                    return new ErrorDataResult<DownloadResponse>(null, "İndirme Linki Bulunamadı");
                return new SuccessDataResult<DownloadResponse>(new DownloadResponse()
                {
                    Url = link
                });
            }
            catch (Exception)
            {
                return new ErrorDataResult<DownloadResponse>(null, "İndirme Linki Bulunamadı");
            }
        }
    }
}
