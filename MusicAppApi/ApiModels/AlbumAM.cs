namespace MusicAppApi.ApiModels
{
    public class AlbumAM
    {
        public int AlbumID { get; set; }
        public string? AlbumName { get; set; }
        public string? ReleaseDate { get; set; }
        public string? Length { get; set; }
        public int ArtistID { get; set; }
    }
}
