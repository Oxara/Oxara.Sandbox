using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Tryout.ETag.Delta.EFContext;

namespace Tryout.ETag.Delta.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class DummyEntityController(IServiceProvider serviceProvider) : ControllerBase
    {
        private EF_Context _context = serviceProvider.GetRequiredService<EF_Context>();

        [HttpGet(Name = "Get")]
        public async Task<IEnumerable<DummyEntity>> Get(CancellationToken cancellationToken)
        {
            var result = await _context.Set<DummyEntity>().Where(p => p.UserEmail == "erdemozkara@hotmail.com.tr").ToListAsync(cancellationToken);
            return result;
        }
    }
}
