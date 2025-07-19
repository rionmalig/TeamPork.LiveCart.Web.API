using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeamPork.LiveCart.Infrastructure.Data.Entities.LiveCart;
using TeamPork.LiveCart.Infrastructure.Data.Entities.LiveCart.App;
using TeamPork.LiveCart.Model.LiveCart;
using TeamPork.LiveCart.Model.LiveCart.App;

namespace TeamPork.LiveCart.Core.AutoMapper
{
    public class SyncProfile : Profile
    {
        public SyncProfile()
        {
            CreateMap<CustomerEntity, Customer>()
                .ForMember(dest => dest.FromBusiness,
                    opt => opt.MapFrom(src => src.UserSeqId == null && src.BusinessSeqId != null))
                .ReverseMap();
            CreateMap<ItemEntity, Item>()
                .ForMember(dest => dest.FromBusiness,
                    opt => opt.MapFrom(src => src.UserSeqId == null && src.BusinessSeqId != null))
                .ReverseMap();
            CreateMap<InvoiceEntity, Invoice>()
                .ForMember(dest => dest.FromBusiness,
                    opt => opt.MapFrom(src => src.UserSeqId == null && src.BusinessSeqId != null))
                .ReverseMap();
            CreateMap<InvoiceAdjustmentEntity, InvoiceAdjustment>()
                .ForMember(dest => dest.FromBusiness,
                    opt => opt.MapFrom(src => src.UserSeqId == null && src.BusinessSeqId != null))
                .ReverseMap();
            CreateMap<InvoiceItemEntity, InvoiceItem>()
                .ForMember(dest => dest.FromBusiness,
                    opt => opt.MapFrom(src => src.UserSeqId == null && src.BusinessSeqId != null))
                .ReverseMap();
        }
    }
}
