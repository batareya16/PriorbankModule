using System;
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

            Mapper.CreateMap<PriorbankLockedTransaction, PriorbankTransaction>()
                .ForMember(x => x.Amount, opt => opt.MapFrom(e => e.AAmount))
                .ForMember(x => x.TransCurr, opt => opt.MapFrom(e => e.ATransCurr))
                .ForMember(x => x.TransDate, opt => opt.MapFrom(e => e.ATransDate))
                .ForMember(x => x.TransTime, opt => opt.MapFrom(e => e.ATransTime))
                .ForMember(x => x.AccountAmount, opt => opt.MapFrom(e => e.ATransAmount))
                .ForMember(x => x.AmountString, opt => opt.MapFrom(e => e.AAmountString))
                .ForMember(x => x.TransDetails, opt => opt.MapFrom(e => e.ATransDetails))
                .ForMember(x => x.AccountAmountString, opt => opt.MapFrom(e => e.ATransAmountString));
        }
    }
}
