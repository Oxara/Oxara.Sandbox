using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ETag.Delta
{
    [ApiController]
    [Route("[controller]")]
    public class DummyEntityController(IServiceProvider serviceProvider) : ControllerBase
    {
        private EF_Context _context = serviceProvider.GetRequiredService<EF_Context>();

        [HttpGet(Name = "Get")]
        public async Task<IEnumerable<DummyEntity>> Get(CancellationToken cancellationToken)
        {
            var result = await _context.Set<DummyEntity>()
                .Include(e => e.Relations)  // iliþkileri dahil et
                .Where(p => p.UserName == "User 50")
                .Select(e => new DummyEntity
                {
                    UUID = e.UUID,
                    UserName = e.UserName,
                    UserEmail = e.UserEmail,
                    Relations = e.Relations.Select(r => new DummyEntityRelation
                    {
                        UUID = r.UUID,
                        UserName = r.UserName,
                        UserEmail = r.UserEmail,
                    }).ToList()
                })
                .ToListAsync(cancellationToken);

            return result;
        }
    }
}
