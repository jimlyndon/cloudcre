namespace Cloudcre.Model.Queue
{
    public static class QueuedItemFactory
    {
        public static QueuedItem CreateItemFor(MultipleFamily property, Queue queue)
        {
            return new QueuedItem(property, queue);
        }
    }
}