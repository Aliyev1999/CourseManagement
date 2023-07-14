using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourseManagementEFCore.Domain.Abstractions
{
    public interface IAudited
    {

    }
    public interface ICreationTime : IAudited
    {
        public DateTime CreationDate { get; set; }
    }
    public interface ICreationAudited : ICreationTime
    {
        public long? CreatorID { get; set; }
    }

    public interface IModificationTime : IAudited
    {
        public DateTime? LastModificationDate { get; set; }
    }
    public interface IModificationAudited : IModificationTime
    {
        public long? LastModificatorID { get; set; }
    }
    public interface IDeletionTime : IAudited
    {
        DateTime? DeletedDate { get; set; }
    }
    public interface IDeletionAudited : IDeletionTime
    {
        long? DeletedUserID { get; set; }
    }
}

