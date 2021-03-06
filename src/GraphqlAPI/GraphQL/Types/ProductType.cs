﻿using GraphQL.DataLoader;
using GraphQL.Types;
using GraphqlDemo.Data.Entities;
using GraphqlDemo.Repositories;


namespace GraphqlDemo.GraphQL.Types
{
    public class ProductType: ObjectGraphType<Product>
    {
        public ProductType(ProductReviewRepository reviewRepository, 
                IDataLoaderContextAccessor dataLoaderAccessor)
        {
            Field(x=>x.Id);
            Field(x=>x.Name);
            Field(x=>x.Description).Description("The name of the product");
            Field(t => t.IntroducedAt).Description("When the product was first introduced in the catalog");
            Field(t => t.PhotoFileName).Description("The file name of the photo so the client can render it");
            Field(t => t.Price);
            Field(t => t.Rating).Description("The (max 5) star customer rating");
            Field(t => t.Stock);
            Field<ProductTypeEnumType>("Type", "The type of product");

            Field<ListGraphType<ProductReviewType>>(
                "reviews",
                resolve: ctx => {
                    var loader = dataLoaderAccessor.Context.GetOrAddCollectionBatchLoader<int,ProductReview>(
                        "GetReviewsByProductId",reviewRepository.GetForProducts);                    
                    return loader.LoadAsync(ctx.Source.Id);
                }
            );
        }
    }
}
