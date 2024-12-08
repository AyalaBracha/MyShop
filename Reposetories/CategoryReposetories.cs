﻿using Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace Reposetories
{


    public class CategoryReposetories : ICategoryReposetories
    {
        MyShop214935017Context myShop;
        public CategoryReposetories(MyShop214935017Context myShop)
        {
            this.myShop = myShop;
        }
        public async Task<List<Category>> Get()
        {
            return await myShop.Categories.ToListAsync();
        }
        public async Task<Category> GetById(int id)
        {
            return await myShop.Categories.FirstOrDefaultAsync(currentCategory => currentCategory.Id == id);
        }
    }
}