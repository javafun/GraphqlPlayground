using GraphQL;
using GraphQL.Types;

namespace GraphqlDemo.GraphQL
{
    public class GraphqlDemoSchema : Schema
    {
        public GraphqlDemoSchema(IDependencyResolver resolver) : base(resolver)
        {
            Query = resolver.Resolve<GraphqlDemoQuery>();
        }
    }
}
