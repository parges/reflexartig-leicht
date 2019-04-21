using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using rl_contract.Models;
using rl_contract.Models.Review;

namespace kubaapi.Mapper.Profiles
{

    public class MapperProfiles : Profile
    {
        public MapperProfiles()
        {
            CreateMap<Testung, Testung>();
            CreateMap<TestungQuestion, TestungQuestion>();
            CreateMap<Anamnese, Anamnese>();
            CreateMap<Patient, Patient>();
            CreateMap<Review, Review>();
            CreateMap<TestungChapter, ReviewChapter>().ForMember(x => x.Id, opt => opt.Ignore()).ForMember(x => x.Questions, opt => opt.Ignore());
            CreateMap<TestungQuestion, ReviewQuestion>().ForMember(x => x.Id, opt => opt.Ignore());
        }
    }
}
