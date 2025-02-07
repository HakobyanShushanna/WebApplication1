namespace DemoBookStore.Models
{
    public class UserModel:Person
    {
        public List<ReviewModel> Reviews { get; set; }
        public int Age { get; set; }
        public string Address { get; set; }
    }
}