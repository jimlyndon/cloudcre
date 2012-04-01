using System;
using NHibernate.Search.Attributes;
using Cloudcre.Model.Core;

namespace Cloudcre.Model
{
    [Indexed]
    public class Comment : EntityBase<Guid>
    {
        public virtual MultipleFamily Property { get; set; }

        [Field(Index.Tokenized, Store = Store.Yes)]
        public virtual string Text { get; set; }

        protected override void Validate()
        {
            throw new NotImplementedException();
        }
    }
}