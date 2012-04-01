using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Cloudcre.Web.Models
{
    [DataContract]
    public class QueueViewModel
    {
        [DataMember]
        public IEnumerable<QueuedItemViewModel> QueuedItems { get; set; }
    }
}