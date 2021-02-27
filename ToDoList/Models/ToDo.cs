using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ToDoList.Models
{
    /// <summary>
    /// TODO Class
    /// </summary>
    public class ToDo
    {
        /// <summary>
        /// Id
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// Description of the task
        /// </summary>
        public string Description { get; set; }
        /// <summary>
        /// Check of the done task
        /// </summary>
        public bool IsDone { get; set; }
        /// <summary>
        /// Application user
        /// </summary>
        public virtual ApplicationUser User { get; set; }
    }
}