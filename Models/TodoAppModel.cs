using System.ComponentModel.DataAnnotations;

namespace TodoAppRil.Models
{
    public class TodoAppModel
    {
        public int Id { get; set; }
        [Required]
        public required string Title { get; set; }
        //public string Description { get; set; }
        public required string Category { get; set; }
        [Range(1, 5)]
        public int Priority { get; set; } //1 means low priority 
        [DataType(DataType.Date)]
        public DateTime? DueDate { get; set; }
    }
}
