using GraphQL.Types;
using GraphqlDemo.GraphQL.Types;
using GraphqlDemo.Repositories;

namespace GraphqlDemo.GraphQL
{
    public class GraphqlDemoQuery : ObjectGraphType
    {

        public GraphqlDemoQuery(ProductRepository productRepository)
        {
            Field<ListGraphType<ProductType>>(
                "product",
                resolve: context => productRepository.GetAll()
             );
        }
    }
}
