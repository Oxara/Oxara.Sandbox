using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ETag.Delta
{
    [ApiController]
    [Route("[controller]")]
    public class DummyEntityController(IServiceProvider serviceProvider) : ControllerBase
    {
        private EF_Context _context = serviceProvider.GetRequiredService<EF_Context>();

        [HttpGet("Get")]
        public async Task<IEnumerable<User>> Get(CancellationToken cancellationToken)
        {
            var result = await _context.Set<User>()
                .Include(e => e.UserContacts)  // iliþkileri dahil et
                .Where(p => p.UserName == "User 50")
                .Select(e => new User
                {
                    UUID = e.UUID,
                    UserName = e.UserName,
                    UserEmail = e.UserEmail,
                    UserContacts = e.UserContacts.Select(r => new UserContact
                    {
                        UUID = r.UUID,
                        UserName = r.UserName,
                        UserEmail = r.UserEmail,
                    }).ToList()
                })
                .ToListAsync(cancellationToken);

            return result;
        }

        [HttpGet("SearchByUserName")]
        public async Task<IEnumerable<User>> SearchByUserName(string keyword, CancellationToken cancellationToken)
        {
            if (string.IsNullOrWhiteSpace(keyword)) return Enumerable.Empty<User>();

            var result = await _context.Set<User>()
                .Include(e => e.UserContacts)
                .Where(p => EF.Functions.Like(p.UserName, $"%{keyword}%"))
                .Select(e => new User
                {
                    UUID = e.UUID,
                    UserName = e.UserName,
                    UserEmail = e.UserEmail,
                    UserContacts = e.UserContacts.Select(r => new UserContact
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
