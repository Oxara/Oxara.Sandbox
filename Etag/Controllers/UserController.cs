using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
namespace ETag.Delta;

[ApiController]
[Route("[controller]")]
public class UserController(IServiceProvider serviceProvider) : ControllerBase
{
    private EF_Context _context = serviceProvider.GetRequiredService<EF_Context>();

    [HttpGet("SearchByUserName")]
    public async Task<IEnumerable<UserDTO>> SearchByUserName(string keyword, CancellationToken cancellationToken)
    {
        if (string.IsNullOrWhiteSpace(keyword)) return Enumerable.Empty<UserDTO>();

        var result = await _context.Set<User>()
            .Include(e => e.UserContacts)
            .Where(p => EF.Functions.Like(p.UserName, $"%{keyword}%"))
            .Select(e => new UserDTO
            {
                UUID = e.UUID,
                UserName = e.UserName,
                FirstName = e.FirstName,
                LastName = e.LastName,
                UserEmail = e.UserEmail,
                UserContacts = e.UserContacts.Select(r => new UserContactDTO
                {
                    UUID = r.UUID,
                    UserName = r.UserName,
                    FirstName = r.FirstName,
                    LastName = r.LastName,
                    UserEmail = r.UserEmail,
                }).ToList()
            })
            .ToListAsync(cancellationToken);

        return result;
    }
}
