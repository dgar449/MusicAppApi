namespace MusicAppApi.Data.Models.Context
{
    public class Genre
    {
        public int GenreID { get; set; }
        public string? GenreType { get; set; }

        public IEnumerable<Song> Songs { get; set; }
    }
}
