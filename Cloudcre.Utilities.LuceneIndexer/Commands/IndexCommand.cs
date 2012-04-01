using Cloudcre.Utilities.Console;
using Cloudcre.Utilities.LuceneIndexer.Indexers;

namespace Cloudcre.Utilities.LuceneIndexer.Commands
{
    public class IndexCommand : EnvironmentCommand
    {
        /// <summary>
        /// Command to index all persisted model objects into lucene
        /// </summary>
        /// <example>
        /// Example usage:
        /// 
        /// Cloudcre.Utilities.LuceneIndexer.exe index -e development
        ///
        /// </example>
        public IndexCommand()
        {
            IsCommand("index", "Index all persisted model objects into lucene");
        }

        public override void Execute(string[] remainingArguments)
        {
            var indexer = new Indexer(EnvironmentType);

            indexer.Run();
        }
    }
}