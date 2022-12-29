﻿using Microsoft.EntityFrameworkCore;
using MusicAppApi.Data.Models.Context;
using MusicAppApi.Data.ViewModels;
using System.Linq;

namespace MusicAppApi.Data.Models
{
    public class DbSongRepo : ISongRepo
    {
        public AppDbContext context;
        public DbSongRepo(AppDbContext context)
        {
            this.context = context;
        }

        /*public bool Delete(int id)
        {
            throw new NotImplementedException();
        }*/

        public async Task<ServiceResult<Song>> Create(Song input)
        {
            context.Add(input);
            await context.SaveChangesAsync();
            return new ServiceResult<Song>(input);
        }

        public Song? Delete(int id)
        {
            Song? song = context.Song.Find(id);
            if(song != null)
            {
                context.Song.Remove(song);
                context.SaveChanges();
            }
            return song;
        }

        public IEnumerable<Song> GetAllSongs()
        {
            return context.Song;
        }

        public Song GetSongs(int Id)
        {
            return context.Song.Find(Id);
        }
        public IEnumerable<Artist> GetArtists()
        {
            return context.Artist;

        }

        public async Task<ServiceResult<Song>> Update(Song input)
        {
            var song = context.Song.Find(input.SongID);
            if (song == null)
            {
                return new ServiceResult<Song>("Song not found");
            }

            song.TrackLength = input.TrackLength;
            song.ReleaseDate = input.ReleaseDate;
            song.SongTitle = input.SongTitle;
            song.ArtistID = input.ArtistID;

            context.Update(song);
            await context.SaveChangesAsync();
            return new ServiceResult<Song>(song);
        }

        public async Task<IEnumerable<SearchSongVm>> Search(string query)
        {
            // LINQ
            //var song = from x in context.Song
            //           join y in context.Album on x.AlbumID equals y.AlbumID
            //           join z in context.Artist on x.ArtistID equals z.ArtistID
            //           where (x.SongTitle.Contains(sq) ||
            //            y.AlbumName.Contains(sq) ||
            //            z.ArtistName.Contains(sq))
            //           select x;


            // Lambda
            var song = await context.Song
                .Include(x => x.Album)
                .Include(x => x.Artist)
                .Where(x => x.Artist.ArtistName.Contains(query)
                    || x.Album.AlbumName.Contains(query)
                    || x.SongTitle.Contains(query))
                .Select(x=>new SearchSongVm()
                {
                    AlbumName = x.Album.AlbumName,
                    ArtistName = x.Artist.ArtistName,
                    SongID = x.SongID,
                    TrackLength = x.TrackLength,
                    SongTitle = x.SongTitle
                }).ToListAsync();

            return song;
        }
        public IEnumerable<Album> GetAlbum()
        {
            return context.Album;

        }

       
        public async Task<IEnumerable<AlbumListVm>> AllAlbums(int query)
        {
            var song = await context.Album
                .Where(x => x.ArtistID==query)
                .Select(x => new AlbumListVm()
                {
                    AlbumName = x.AlbumName
                }).ToListAsync();
            return song;
        }

         public async Task<IEnumerable<RankSongCountVm>> RankSongsTotal()
         {

            //Groups By Count of ArtistID in the Song Table
            var song = await context.Song
                    .GroupBy(x => x.ArtistID, (key, value) => new RankSongCountVm()
                    {
                        ArtistID = key,
                        SongCount = value.Count()
                    }).ToListAsync();
            //Uses the results from the var song and adds artist name to it using FK ArtistID found in Song table and Artist table
            var artistList = new List<Artist>();
            foreach (var s in song)
            {
                var artist = await context.Artist.FirstOrDefaultAsync(x => x.ArtistID == s.ArtistID);
                if (artist != null)
                {
                    artistList.Add(artist);
                }
            }
            var artists = artistList
                .Select(x => new RankSongCountVm()
                {
                    ArtistID = x.ArtistID,
                    ArtistName = x.ArtistName,
                    SongCount = song.FirstOrDefault(y => y.ArtistID == x.ArtistID).SongCount
                }).ToList();
            //Ranks the results of var artist based on descending order and adds a rank to it (foreach ish)
            var rankings = artists.OrderByDescending(x => x.SongCount).GroupBy(x => x.SongCount)
                .SelectMany((g, i) => g.Select(e => new RankSongCountVm()
                {
                    ArtistName = e.ArtistName,
                    SongCount = e.SongCount,
                    Rank = i + 1
                }));

            return rankings;
        }
        public async Task<IEnumerable<MusicCareer>> CareerAge()
        {
            //
            var song = await context.Song
                .GroupBy(x => x.ArtistID, (key, value) => new MusicCareer()
                {
                    ArtistID = key,
                    Oldest = (DateTime)value.Min(y=> y.ReleaseDate)
                }).ToListAsync();
            //
            var artistList = new List<Artist>();
            foreach (var s in song)
            {
                var artist = await context.Artist.FirstOrDefaultAsync(x => x.ArtistID == s.ArtistID);
                if (artist != null)
                {
                    artistList.Add(artist);
                }
            }
            var artists = artistList
                .Select(x => new MusicCareer()
                {
                    ArtistID = x.ArtistID,
                    ArtistName = x.ArtistName,
                    ReleaseDate = song.FirstOrDefault(y => y.ArtistID == x.ArtistID).Oldest.GetValueOrDefault()
                });

            var careerAge = artists.OrderBy(x => x.ReleaseDate).GroupBy(x => x.ReleaseDate)
             .SelectMany((g, i) => g.Select(e => new MusicCareer()
             {
                 ArtistName = e.ArtistName,
                 ReleaseDate = e.ReleaseDate,
                 Rank = i + 1
             }));
            return careerAge;
        }
        public async Task<IEnumerable<MusicCareer>> CareerDuration()
        {
            //
            var song = await context.Song
                .GroupBy(x => x.ArtistID, (key, value) => new MusicCareer()
                {
                    ArtistID = key,
                    Oldest = (DateTime)value.Min(y => y.ReleaseDate),
                    Newest = (DateTime)value.Max(y => y.ReleaseDate)
                }).ToListAsync();
            //
            var artistList = new List<Artist>();
            foreach (var s in song)
            {
                var artist = await context.Artist.FirstOrDefaultAsync(x => x.ArtistID == s.ArtistID);
                if (artist != null)
                {
                    artistList.Add(artist);
                }
            }
            var artists = artistList
                .Select(x => new MusicCareer()
                {
                    ArtistID = x.ArtistID,
                    ArtistName = x.ArtistName,
                    CareerLength = song.FirstOrDefault(y => y.ArtistID == x.ArtistID).Newest.GetValueOrDefault().Date - (song.FirstOrDefault(z => z.ArtistID == x.ArtistID).Oldest).GetValueOrDefault().Date
                });

            var careerAge = artists.OrderByDescending(x => x.CareerLength).GroupBy(x => x.CareerLength)
             .SelectMany((g, i) => g.Select(e => new MusicCareer()
             {
                 ArtistName = e.ArtistName,
                 CareerDays = e.CareerLength.TotalDays,
                 Rank = i + 1
             }));
            return careerAge;
        }
        public async Task<IEnumerable<MusicCareer>> CareerAverage()
        {
            //
            var song = await context.Song
                .GroupBy(x => x.ArtistID, (key, value) => new MusicCareer()
                {
                    ArtistID = key,
                    Oldest = value.Min(y => y.ReleaseDate),
                    Newest = value.Max(y => y.ReleaseDate),
                    SongCount = value.Count()
                }).ToListAsync();
            //
            var artistList = new List<Artist>();
            foreach (var s in song)
            {
                var artist = await context.Artist.FirstOrDefaultAsync(x => x.ArtistID == s.ArtistID);
                if (artist != null)
                {
                    artistList.Add(artist);
                }
            }
            var artists = artistList
                .Select(x => new MusicCareer()
                {
                    ArtistID = x.ArtistID,
                    ArtistName = x.ArtistName,
                    CareerLengthYears = song.FirstOrDefault(y => y.ArtistID == x.ArtistID).Newest.GetValueOrDefault().Date.Year
                    - (song.FirstOrDefault(z => z.ArtistID == x.ArtistID).Oldest).GetValueOrDefault().Date.Year,
                    SongCount = song.FirstOrDefault(a => a.ArtistID == x.ArtistID).SongCount,
                });

            var career = artists.Where(x => artists.Any(y => y.ArtistID == x.ArtistID))
                .Select(x => new MusicCareer()
                {
                    ArtistID = x.ArtistID,
                    ArtistName = x.ArtistName,
                    CareerLengthYears = x.CareerLengthYears,
                    CareerDays = x.CareerLengthYears/x.SongCount
                }).ToList();

            var rank = career.OrderBy(x => x.CareerDays).GroupBy(x => x.CareerDays)
             .SelectMany((g, i) => g.Select(e => new MusicCareer()
             {
                 ArtistName = e.ArtistName,
                 CareerLengthYears =e.CareerLengthYears,
                 CareerDays= e.CareerDays,
                 Rank = i + 1
             }));
            return rank;
        }
        public async Task<IEnumerable<GenrePopularityVm>> GenrePopularity()
        {
            //
            var song = await context.Song
                .GroupBy(x => x.GenreID, (key, value) => new GenrePopularityVm()
                {
                    GenreID = key,
                    GenreCount = value.Count()
                }).ToListAsync();
            //

            var genreList = new List<Genre>();
            foreach (var s in song)
            {
                var genre = await context.Genre.FirstOrDefaultAsync(x => x.GenreID == s.GenreID);
                if (genre != null)
                {
                    genreList.Add(genre);
                }
            }
            var genres = genreList           
                .Select(x => new GenrePopularityVm()
                {
                    GenreID = x.GenreID,
                    GenreType = x.GenreType,
                    GenreCount = song.FirstOrDefault(y => y.GenreID == x.GenreID).GenreCount
                });
            //
            var rankings = genres.OrderByDescending(x => x.GenreCount).GroupBy(x => x.GenreCount)
                .SelectMany((g, i) => g.Select(e => new GenrePopularityVm()
                {
                    GenreType = e.GenreType,
                    GenreCount = e.GenreCount,
                    Rank = i + 1
                }));
            return rankings;
        }
        public async Task<IEnumerable<GenreArtistPopularityVm>> GenreArtistPopularity()
        {
            //
            var song = await context.Song
                .GroupBy(x => new { x.GenreID, x.ArtistID }, (key, value) => new  
                {
                    GenreID = key.GenreID,
                    ArtistID = key.ArtistID,
                    GenreCount = value.Count()
                }).ToListAsync();        

            var top2PerGenre = song.GroupBy(x => x.GenreID)
                .SelectMany(x => x.OrderByDescending(x => x.GenreCount)
                    .Take(2));

            var results = new List<GenreArtistPopularityVm>();
            foreach(var obj in top2PerGenre)
            {
                var result = new GenreArtistPopularityVm
                {
                    GenreType = context.Genre.FirstOrDefault(x => x.GenreID == obj.GenreID).GenreType,
                    ArtistName = context.Artist.FirstOrDefault(x => x.ArtistID == obj.ArtistID).ArtistName,
                    GenreCount = obj.GenreCount
                };

                results.Add(result);
            }
            return results;
        }
        public IEnumerable<Genre> GetAllGenre()
        {
            return context.Genre;
        }
    }
}


/*
 * albumCount
 * ArtistID     Count
 * 1            2
 * 2            2
 * 3            2
 * 
 * 
 * Artist table
 * ArtistID     ArtistName      WHERE(x => albumCount.Any(y => y.ArtistID == x.ArtistID))
 * 1            asldfhj         true
 * 2            sdfjh           false true
 * 3            sdfkj           false false true
 * 
 * 
 */