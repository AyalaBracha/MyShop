﻿using Entities;

namespace Servicess
{
    public interface ICategoryServicess
    {
        Task<List<Category>> Get();
        
    }
}