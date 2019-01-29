using GraphQL.Types;
using GraphqlDemo.Data;

namespace GraphqlDemo.GraphQL.Types
{
    public class ProductTypeEnumType:EnumerationGraphType<ProductTypeEnum>
    {
        public ProductTypeEnumType()
        {
            Name = "Type";
            Description = "The type of product";
        }
    }
}
