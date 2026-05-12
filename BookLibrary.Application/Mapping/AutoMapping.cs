using AutoMapper;
using BookLibrary.Application.Dtos;
using BookLibrary.Application.Dtos.BookDtos;
using BookLibrary.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookLibrary.Application.Mapping
{
    public class AutoMapping:Profile
    {
        public AutoMapping()
        {
            CreateMap<Book,ResultBookDto>()
                .ForMember(a=>a.UserName,dest=>dest.MapFrom(src=>src.AppUser.UserName)).ReverseMap();
            CreateMap<Book,CreateBookDto>().ReverseMap();

        }
    }
}
