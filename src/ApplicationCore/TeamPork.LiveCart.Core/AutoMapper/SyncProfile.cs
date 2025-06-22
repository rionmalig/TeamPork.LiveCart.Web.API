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
            CreateMap<CustomerEntity, Customer>().ReverseMap();
            CreateMap<ItemEntity, Item>().ReverseMap();
            CreateMap<InvoiceEntity, Invoice>().ReverseMap();
            CreateMap<InvoiceItemEntity, InvoiceItem>().ReverseMap();
        }
    }
}
