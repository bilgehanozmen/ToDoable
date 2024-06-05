using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ToDoable.Models
{
    public class ToDoItem
    {
        
        public int ToDoItemId { get; set; }

        [Required(ErrorMessage = "Please, enter a title for todo item")]
        [MaxLength(200)]

        public string Title { get; set; }

        [MaxLength(1500)]
        [DataType(DataType.MultilineText)]
        public string Description { get; set; }


        [Display(Name = "Due Date")]
        public DateTime DueDate { get; set; }

        
        public int RemainingHour
        {
            get
            {
                var remaingTime = (DueDate - DateTime.Now);
                return (int)remaingTime.TotalHours;
            }
        }

        public int CategoryId { get; set; }

        public virtual Category? Category { get; set; }
        /*
         * 
         select * from Todos t inner join Categories c on t.CategoryId=c.Id
         * 
         */

        public string ToDoableUserId{ get; set; }
        public virtual ToDoableUser ToDoableUser { get; set; }



    }
}
