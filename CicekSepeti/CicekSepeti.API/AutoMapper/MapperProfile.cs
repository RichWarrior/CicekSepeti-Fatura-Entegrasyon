using AutoMapper;
using Core.Dtos.Response.Auth;
using Core.Dtos.Response.File;
using Core.Dtos.Response.Invoice;
using Core.Entities;

namespace CicekSepeti.API.AutoMapper
{
    /// <summary>
    /// 
    /// </summary>
    public class MapperProfile : Profile
    {
        /// <summary>
        /// 
        /// </summary>
        public MapperProfile()
        {
            CreateMap<User, LogInResponse>();
            CreateMap<File, ListFileResponse.File>();
            CreateMap<Invoice, ListInvoiceResponse.Invoice>()
                .ForMember(dest => dest.InvoiceStatus, opt => opt.MapFrom(src => src.InvoiceStatus.Name));
        }
    }
}
