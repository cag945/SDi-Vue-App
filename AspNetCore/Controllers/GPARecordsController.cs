using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AspNetCore.Controllers
{
    [Route("api/[controller]")]
    [Authorize]
    [ApiController]
    public class GPARecordsController : ControllerBase
    {
        private readonly ApplicationDbContext _dbContext;

        public GPARecordsController(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        // GET api/gparecords
        [HttpGet]
        public async Task<ActionResult<List<GPARecord>>> Get()
        {
            return await _dbContext.GPARecords.ToListAsync();
        }

        // GET api/gparecords/5
        [HttpGet("{id}")]
        public async Task<ActionResult<GPARecord>> Get(string id)
        {
            return await _dbContext.GPARecords.FindAsync(id);
        }

        // POST api/gparecords
        [HttpPost]
        public async Task Post(GPARecord model)
        {
            await _dbContext.AddAsync(model);

            await _dbContext.SaveChangesAsync();
        }

        // PUT api/gparecords/5
        [HttpPut("{id}")]
        public async Task<ActionResult> Put(string id, GPARecord model)
        {
            var exists = await _dbContext.GPARecords.AnyAsync(f => f.Id == id);
            if (!exists)
            {
                return NotFound();
            }

            _dbContext.GPARecords.Update(model);

            await _dbContext.SaveChangesAsync();

            return Ok();

        }

        // DELETE api/gparecords/5
        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(string id)
        {
            var entity = await _dbContext.GPARecords.FindAsync(id);

            _dbContext.GPARecords.Remove(entity);

            await _dbContext.SaveChangesAsync();
            
            return Ok();
        }
    }
}