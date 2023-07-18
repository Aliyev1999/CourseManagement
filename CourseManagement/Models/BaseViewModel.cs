using System;
using System.ComponentModel.DataAnnotations;

namespace CourseManagement.Models
{
    public class BaseViewModel
    {
        public int ID { get; set; }
        public long? LastModificatorID { get; set; }
        public DateTime? LastModificationDate { get; set; }
        public long? DeletedUserID { get; set; }
        public DateTime? DeletedDate { get; set; }
        public long? CreatorID { get; set; }
        public DateTime CreationDate { get; set; }
    }
}
