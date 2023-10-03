using MaxiShop.Domain.Models;
using MaxiShop.Infrastructure.DbContexts;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace MaxiShop.web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly ApplicationDbContext _dbContext;

        public CategoryController(ApplicationDbContext applicationDbContext)
        {
            this._dbContext = applicationDbContext;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public ActionResult Get()
        {
            var categories = _dbContext.Categories.ToList();
            return Ok(categories);
        }

        [HttpGet]
        [Route("Details")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public ActionResult Get(int id)
        {
            var category = _dbContext.Categories.FirstOrDefault(c => c.Id==id);
            if(category is null)
            {
                return NotFound($"Category not found for Id - {id}");
            }
            return Ok(category);
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public ActionResult Create([FromBody] Category category)
        {
            _dbContext.Categories.Add(category);
            _dbContext.SaveChanges();
            return Ok();
        }

        [HttpPut]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public ActionResult Update([FromBody] Category category)
        {
            _dbContext.Categories.Update(category);
            _dbContext.SaveChanges();
            return NoContent();
        }


        [HttpDelete]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult Delete(int id)
        {
            if (id <= 0)
                return BadRequest();
            var category = _dbContext.Categories.FirstOrDefault(c => c.Id==id);
            if(category is null)
            {
                return NotFound();
            }
            _dbContext.Categories.Remove(category);
            _dbContext.SaveChanges();
            return NoContent();
        }
    }
}
