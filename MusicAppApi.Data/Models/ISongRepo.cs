using MusicAppApi.Data.Models.Context;
using MusicAppApi.Data.ViewModels;

namespace MusicAppApi.Data.Models
{
    public interface ISongRepo
    {
        IEnumerable<Song> GetAllSongs();
        Song GetSongs(int Id);
        Task<ServiceResult<Song>> Create(Song input);
        Task<ServiceResult<Song>> Update(Song input);

        Song? Delete(int id);
        Task<IEnumerable<SearchSongVm>> Search(string sq);

        Task<IEnumerable<RankSongCountVm>> RankSongsTotal();
        Task<IEnumerable<MusicCareer>> CareerAge();
        Task<IEnumerable<MusicCareer>> CareerDuration();
        Task<IEnumerable<MusicCareer>> CareerAverage();
        Task<IEnumerable<GenrePopularityVm>> GenrePopularity();

        Task<IEnumerable<GenreArtistPopularityVm>> GenreArtistPopularity();
        Task<IEnumerable<AlbumListVm>> AllAlbums(int sq);

        IEnumerable<Artist> GetArtists();
        IEnumerable<Album> GetAlbum();
        IEnumerable<Genre> GetAllGenre();

        //  AppDbContext ArtistList();
        // bool Update(int id, string s, string r);
        // bool Delete(int id);
    }
}
