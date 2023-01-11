using System.ComponentModel.DataAnnotations;

namespace MvcDemoProject.Models
{
    public class BaseModel
    {
        [Required(ErrorMessage = "Created by is required")]
        public long createdBy { get; set; }
        [Required(ErrorMessage = "Modified by is required")]
        public long modifiedBy { get; set; }
        public DateTime createdDate { get; set; }
        public DateTime modifiedDate { get; set; }
        public bool isDeleted { get; set; }
       
       
        public class DeleteObj
        {
            public int Id { get; set; }
            public int modifiedBy { get; set; }
            public DateTime modifiedDate { get; set; }

        }
    }
}
