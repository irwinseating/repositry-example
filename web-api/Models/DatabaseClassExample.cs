using System.ComponentModel.DataAnnotations;


namespace web_api.Models
{
    public class DatabaseClassExample
    {
        [Key]
        public int Id { get; set; }

        public string Name { get; set; }
    }
}