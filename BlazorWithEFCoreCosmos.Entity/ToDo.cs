using System;

namespace BlazorWithEFCoreCosmos.Entity
{
    public class ToDo
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public string TaskName { get; set; }
        public bool Done { get; set; } = false;
        public DateTime CreatedDate { get; set; } = DateTime.Now;
        public DateTime UpdatedDate { get; set; } = DateTime.Now;
        public DateTime DueDate { get; set; }
        public DateTime DoneDate { get; set; }
    }
}
