using System;
using System.Collections.Generic;
using AutoMapper;
using MusicAppApi.ApiModels;
using MusicAppApi.Data.Models.Context;
using MusicAppApi.Data.ViewModels;

namespace MusicAppApi.AutoMapper
{
    public class MusicMappingProfile : Profile
    {
        public MusicMappingProfile() 
        {
            CreateMap<Genre, GenreAM>();
            CreateMap<Song, SongAM>();
            CreateMap<Artist, ArtistAM>();
            CreateMap<Album, AlbumAM>();
            CreateMap<AlbumListVm, AlbumAM>();
            CreateMap<RankSongCountVm,RankSongCountAM>();
            CreateMap<MusicCareer, MusicCareerAM>();
            CreateMap<GenrePopularityVm, GenrePopularityAM>();
            CreateMap<GenreArtistPopularityVm, GenreArtistPopularityAM>();
            CreateMap<SongAM, Song>();
        }
    }
}