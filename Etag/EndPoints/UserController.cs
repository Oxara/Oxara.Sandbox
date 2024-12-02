using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NCalc;
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
            .ToListAsync(cancellationToken);

        return result.ToDTO();
    }
}
