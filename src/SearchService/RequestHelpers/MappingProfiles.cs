using AutoMapper;
using Contracts;
using SearchService.Models;

namespace SearchService.RequestHelpers;

public class MappingProfiles : Profile
{
    public MappingProfiles()
    {
        // Mapping to AuctionCreated type to Item type
         CreateMap<AuctionCreated, Item>();
    }
}
