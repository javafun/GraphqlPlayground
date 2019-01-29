using GraphQL.Types;

namespace GraphqlDemo.GraphQL.Types
{
    public class ProductTypeEnumType:EnumerationGraphType<Data.ProductType>
    {
        public ProductTypeEnumType()
        {
            Name = "Type";
            Description = "The type of product";
        }
    }
}
