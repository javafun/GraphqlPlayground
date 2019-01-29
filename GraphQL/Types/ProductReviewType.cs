using GraphQL.Types;
using GraphqlDemo.Data.Entities;

namespace GraphqlDemo.GraphQL.Types
{
    public class ProductReviewType : ObjectGraphType<ProductReview>
    {
        public ProductReviewType()
        {
            Field(x=>x.Id);
            Field(x=>x.Title);
            Field(x=>x.Review);
        }
    }
}