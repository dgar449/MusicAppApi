namespace MusicAppApi.ApiModels
{
    public class SongAM
    {
        public int SongID { get; set; }
        public string? SongTitle { get; set; }
        public int ArtistID { get; set; }

        public int AlbumID { get; set; }

        public int GenreID { get; set; }

        public DateTime? ReleaseDate { get; set; }
        public int TrackLength { get; set; }
    }
}
