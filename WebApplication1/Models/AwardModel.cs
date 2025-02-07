namespace DemoBookStore.Models
{
    public class AwardModel
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public List<UserModel> Funders { get; set; }
        public AuthorModel Author { get; set; }
        public DateTime Date { get; set; }
    }
}
