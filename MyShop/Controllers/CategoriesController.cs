using AutoMapper;
using DTO;
using Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using Servicess;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace MyShop.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriesController : ControllerBase
    {
        ICategoryServicess servicess;
        IMapper _mapper;
        IMemoryCache memoryCache;
        public CategoriesController(ICategoryServicess servicess, IMapper mapper, IMemoryCache memoryCache)
        {
            this.servicess = servicess;
            _mapper = mapper;
            this.memoryCache = memoryCache;
        }
        // GET: api/<CategoriesController>
        [HttpGet]
        public async Task<ActionResult<List<CategoryDTO>>> Get()
        {
            var cacheKey = "myCategories";

            var cachedData = memoryCache.Get(cacheKey);

            if (cachedData == null)

            {
                List<Category> categories = await servicess.Get();
                List<CategoryDTO> categoriesDTO = _mapper.Map<List<Category>, List<CategoryDTO>>(categories);
                if (categoriesDTO != null)
                {
                    cachedData = categoriesDTO;
                    memoryCache.Set(cacheKey, cachedData, TimeSpan.FromMinutes(10));
                    return Ok(categoriesDTO);
                }

                return BadRequest();


            }
            return Ok(cachedData);


            //List<Category> categories = await servicess.Get();
            //if (categories != null)
            //    return Ok(_mapper.Map<List<Category>, List<CategoryDTO>>(categories));
            //return BadRequest();
        }


    }
}
