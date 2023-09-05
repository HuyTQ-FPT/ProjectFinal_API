using FinalAPI.Models;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FinalAPI.DTO
{
    public class MapperProfile:Profile
    {
        public MapperProfile()
        {
            CreateMap<Comment, CommentDTO>();
            CreateMap<Account, AccountDTO>();
        }
    }
}
