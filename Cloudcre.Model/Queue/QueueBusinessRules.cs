using Cloudcre.Model.Core;

namespace Cloudcre.Model.Queue
{
    public class QueueBusinessRules
    {
        public static readonly BusinessRule ItemInvalid = new BusinessRule("Item", "A queue cannot have any invalid items.");
    }
}