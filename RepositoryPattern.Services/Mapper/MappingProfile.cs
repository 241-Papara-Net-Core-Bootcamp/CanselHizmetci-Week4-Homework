using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using RepositoryPattern.Domain.Entities;
using RepositoryPattern.Services.DTOs;

namespace RepositoryPattern.Services.Mapper
{
    public class MappingProfile: Profile
    {
        public MappingProfile()
        {
            //CreateMap<BookDTO, Book>();
            CreateMap<AuthorDTO, Author>();
            CreateMap<Book, BookDTO>().ForMember(c => c.Author, c => c.MapFrom(c => c.Author.Name)).ReverseMap().ForPath(s => s.Author, opt => opt.Ignore());
            CreateMap<Author, AuthorDTO>().ReverseMap();

        }
    }
}
