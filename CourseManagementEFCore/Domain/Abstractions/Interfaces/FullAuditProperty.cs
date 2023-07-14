using CourseManagementEFCore.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourseManagementEFCore.Domain.Abstractions.Interfaces
{
    public class FullAuditProperty :BaseEntity<int>, ICreationAudited, IDeletionAudited, IModificationAudited
    {
        public long? LastModificatorID { get; set; }
        public DateTime? LastModificationDate { get; set; }
        public long? DeletedUserID { get; set; }
        public DateTime? DeletedDate { get; set; }
        public long? CreatorID { get; set; }
        public DateTime CreationDate { get; set; }
    }
}
