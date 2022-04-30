using System;
namespace TimsMaplestoryAPI.Models
{
    public class Class
    {
        public int ClassId { get; set; }
        public string ClassName { get; set; }
        public List<Player> Player { get; set; }
    }
}
