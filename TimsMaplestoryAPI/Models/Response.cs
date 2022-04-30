namespace TimsMaplestoryAPI.Models
{
    public class Response
    {
        public int statusCode { get; set; }
        public string statusDescription { get; set; }
        public List<Class> classes { get; set; } = new();
        public List<Player> players { get; set; } = new();
    }
}
