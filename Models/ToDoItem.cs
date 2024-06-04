using System.ComponentModel.DataAnnotations;

namespace ToDoable.Models
{
    public class ToDoItem
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Please, enter a title for todo item")]
        [MaxLength(200)]

        public string Title { get; set; }

        [MaxLength(1500)]
        [DataType(DataType.MultilineText)]
        public string Description { get; set; }

        [Display(Name = "Is Completed")]
        public bool IsCompleted { get; set; }

        [Display(Name = "Due Date")]
        public DateTime DueDate { get; set; }

        [ScaffoldColumn(false)]
        public DateTime CreatedDate { get; set; } = DateTime.Now;

        [ScaffoldColumn(false)]
        public DateTime CompletedDate { get; set; }

        public int CategoryId { get; set; }

        public virtual Category Category { get; set; }

        public string ToDoableUserId { get; set; }
        public virtual ToDoableUser ToDoableUser { get; set; }

        public int RemainingHour
        {
            get
            {
                var remaingTime = (DueDate - DateTime.Now);
                return (int)remaingTime.TotalHours;
            }
        }

    }
}
