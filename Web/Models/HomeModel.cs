using System.Collections.Generic;
using Web.Entities;

namespace Web.Models
{
    public class HomeModel
    {
        public IEnumerable<Task> Tasks { get; set; } 
    }
}