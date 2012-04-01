using System;
using System.Runtime.Serialization;

namespace Cloudcre.Web.Models
{
    [DataContract]
    public class QueuedItemViewModel
    {
        [DataMember]
        public Guid Id { get; set; }
        [DataMember]
        public string ParcelId { get; set; }
        [DataMember]
        public string Name { get; set; }
    }
}