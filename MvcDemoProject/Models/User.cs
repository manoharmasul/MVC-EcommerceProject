using System.ComponentModel.DataAnnotations;

namespace MvcDemoProject.Models
{
    public class User : BaseModel
    {
        //Id,userName,userEmail,password,role,createdBy,createdDate,modifiedBy,modifiedDate,isDeleted
        [Key]
        [ScaffoldColumn(true)]
        public int Id { get; set; }
        [Required]
        public string userName { get; set; }
        [Required]
        [DataType(DataType.EmailAddress)]
        public string userEmail { get; set; }
        [Required]
        [DataType(DataType.Password)]

        public string password { get; set; }
        public string role { get; set; }
    }
    public class logUserModel
    {
        [Required]
        public string userName { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string password { get; set; }

    }
    public class UsertRegistrationModel
    {

        public string userName { get; set; }
        [Required]
        [DataType(DataType.EmailAddress)]
        public string userEmail { get; set; }
        [Required]
        [DataType(DataType.Password)]
        public string password { get; set; }

        public double wallet { get; set; }


    }
    public class AddMoneyModel
    {
        public double wallet { get; set; }
    }
}
