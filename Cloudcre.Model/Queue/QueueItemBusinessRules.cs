using Cloudcre.Model.Core;

namespace Cloudcre.Model.Queue
{
    public class QueueItemBusinessRules
    {
        public static readonly BusinessRule QueueRequired = new BusinessRule("Queue", "A queued item must be related to an existing queue");
    }
}