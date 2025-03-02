﻿using Entities;

namespace Reposetories
{
    public interface IProductReposetories
    {
        Task<List<Product>> Get(string? desc, int? minPrice, int? maxPrice, int?[] categoryIds);
        Product GetById(int id);
    }
}