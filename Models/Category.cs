using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ToDoable.Models
{
    public class Category
    {
        public int Id{ get; set; }

        [MaxLength(50)]
        [Column(TypeName = "nvarchar(50)")]
        public string Name { get; set; }

        [Column(TypeName = "nvarchar(50)")]
        public string Description { get; set; }
        [MaxLength(50)]

        [Column(TypeName = "nvarchar(10)")]
        public string Color { get; set;}

        public virtual List<ToDoItem> TodoItems { get; set; }
    }
}
