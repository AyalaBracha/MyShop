﻿using Entities;
using Reposetories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Servicess
{
    public class RatingServicess : IRatingServicess
    {
        IRatingReposetories reposetory;
        public RatingServicess(IRatingReposetories reposetory)
        {
            this.reposetory = reposetory;

        }
        public Task<Rating> Post(Rating rating)
        {
            return reposetory.Post(rating);
        }
    }
}
