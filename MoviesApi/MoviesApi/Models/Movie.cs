namespace MoviesApi.Models
{
    public class Movie
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int ReleaseYear { get; set; }
        public bool AcademyAward { get; set; }
        public int? DirectorId { get; set; }
    }
}
