using Cloudcre.Model.Core;

namespace Cloudcre.Model.Queue
{
    public class QueuedItem : EntityBase<int>
    {
        public QueuedItem()
        {

        }

        public QueuedItem(MultipleFamily property, Queue queue)
        {
            Property = property;
            Queue = queue;
        }

        public MultipleFamily Property { get; private set; }

        public Queue Queue { get; private set; }

        public bool Contains(MultipleFamily property)
        {
            return Property == property;
        }

        protected override void Validate()
        {
            if(Queue==null)
                base.AddBrokenRule(QueueItemBusinessRules.QueueRequired);
        }
    }
}