namespace MusicAppApi.Data.Models.Context
{
    public class Artist
    {
        public int ArtistID { get; set; }
        public string? ArtistName { get; set; }
        public IEnumerable<Song> Songs { get; set; }
    }
}
