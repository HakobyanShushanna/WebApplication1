namespace WebApplication1.Models
{
    public class ReviewModel
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public int Stars { get; set; }
        public UserModel User { get; set; }
        public BookModel Book { get; set; }
        public string Description { get; set; }
    }
}