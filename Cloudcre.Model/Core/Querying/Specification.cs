using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace Cloudcre.Model.Core.Querying
{
    public abstract class Specification<T>
    {
        public abstract Expression<Func<T, bool>> IsSatisfied();
    }

    public static class SpecificationExtensions
    {
        public static Specification<T> And<T>(this Specification<T> spec1, Specification<T> spec2)
        {
            return new AndSpecification<T>(spec1, spec2);
        }

        public static Specification<T> Or<T>(this Specification<T> spec1, Specification<T> spec2)
        {
            return new OrSpecification<T>(spec1, spec2);
        }

        public static Specification<T> Not<T>(this Specification<T> spec1)
        {
            return new NotSpecification<T>(spec1);
        }
    }

    /// <summary>
    /// See David DeWinter's blog: http://blogs.rev-net.com/ddewinter/2009/05/31/linq-expression-trees-and-the-specification-pattern/
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class AndSpecification<T> : Specification<T>
    {
        private Specification<T> spec1;
        private Specification<T> spec2;

        public AndSpecification(Specification<T> spec1, Specification<T> spec2)
        {
            this.spec1 = spec1;
            this.spec2 = spec2;
        }

        public override Expression<Func<T, bool>> IsSatisfied()
        {
            return this.spec1.IsSatisfied().And(this.spec2.IsSatisfied());
        }
    }

    public class OrSpecification<T> : Specification<T>
    {
        private Specification<T> spec1;
        private Specification<T> spec2;

        public OrSpecification(Specification<T> spec1, Specification<T> spec2)
        {
            this.spec1 = spec1;
            this.spec2 = spec2;
        }

        public override Expression<Func<T, bool>> IsSatisfied()
        {
            return this.spec1.IsSatisfied().Or(this.spec2.IsSatisfied());
        }
    }

    public class NotSpecification<T> : Specification<T>
    {
        private Specification<T> originalSpec;

        public NotSpecification(Specification<T> originalSpec)
        {
            this.originalSpec = originalSpec;
        }

        public override Expression<Func<T, bool>> IsSatisfied()
        {
            Expression<Func<T, bool>> originalTree = this.originalSpec.IsSatisfied();
            return Expression.Lambda<Func<T, bool>>(
                Expression.Not(originalTree.Body),
                originalTree.Parameters.Single()
            );
        }
    }

    /// <summary>
    /// See Colin Meek's blog: http://blogs.msdn.com/b/meek/archive/2008/05/02/linq-to-entities-combining-predicates.aspx
    /// </summary>
    public static class ExpressionExtensions
    {
        public static Expression<T> Compose<T>(this Expression<T> first, Expression<T> second, Func<Expression, Expression, Expression> merge)
        {
            // build parameter map (from parameters of second to parameters of first)
            var map = first.Parameters.Select((f, i) => new { f, s = second.Parameters[i] }).ToDictionary(p => p.s, p => p.f);

            // replace parameters in the second lambda expression with parameters from the first
            var secondBody = ParameterRebinder.ReplaceParameters(map, second.Body);

            // apply composition of lambda expression bodies to parameters from the first expression 
            return Expression.Lambda<T>(merge(first.Body, secondBody), first.Parameters);
        }

        public static Expression<Func<T, bool>> And<T>(this Expression<Func<T, bool>> first, Expression<Func<T, bool>> second)
        {
            return first.Compose(second, Expression.And);
        }

        public static Expression<Func<T, bool>> Or<T>(this Expression<Func<T, bool>> first, Expression<Func<T, bool>> second)
        {
            return first.Compose(second, Expression.Or);
        }

        public class ParameterRebinder : ExpressionVisitor
        {
            private readonly Dictionary<ParameterExpression, ParameterExpression> map;

            public ParameterRebinder(Dictionary<ParameterExpression, ParameterExpression> map)
            {
                this.map = map ?? new Dictionary<ParameterExpression, ParameterExpression>();
            }

            public static Expression ReplaceParameters(Dictionary<ParameterExpression, ParameterExpression> map, Expression exp)
            {
                return new ParameterRebinder(map).Visit(exp);
            }

            protected override Expression VisitParameter(ParameterExpression p)
            {
                ParameterExpression replacement;
                if (map.TryGetValue(p, out replacement))
                {
                    p = replacement;
                }
                return base.VisitParameter(p);
            }
        }
    }
}