namespace Uku.Database.Model
{
    public class Album
    {
        public int? AlbumId { get; set; }
        public string Title { get; set; }
        public string Barcode { get; set; }
        public int? Year { get; set; }
        public int? Top3kPosition { get; set; }
    }
}
