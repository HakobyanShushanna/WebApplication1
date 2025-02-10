namespace WebApplication1.Models
{
    public class AuthorModel:Person
    {
        public List<BookModel> Books { get; set; }
        public List<AwardModel> Awards { get; set; }
        public double AverageScore { get; set; }
    }
}
