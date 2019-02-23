using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AutoMapper;
using PriorbankModule.Entities;

namespace PriorbankModule.Common
{
    class MappingProfile : Profile
    {
        public MappingProfile()
        {
            Mapper.CreateMap<PriorbankTransaction, Income>()
                .ForMember(x => x.Description, opt => opt.MapFrom(e => e.TransDetails))
                .ForMember(x => x.DateAndTime, opt => opt.MapFrom(e => 
                    new DateTime(
                        e.TransDate.Year,
                        e.TransDate.Month,
                        e.TransDate.Day,
                        e.TransTime.Hour,
                        e.TransTime.Minute,
                        e.TransTime.Second)))
                .ForMember(x => x.Summ, opt => opt.MapFrom(e =>
                    Convert.ToDouble(e.AccountAmountString.Replace("  ", string.Empty).Replace('.', ','))));
        }
    }
}
