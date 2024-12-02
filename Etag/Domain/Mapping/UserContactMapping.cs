namespace ETag.Delta;

public static partial class MappingHelper
{
    public static IEnumerable<UserContactDTO> ToDTO(this IEnumerable<UserContact> entityList, User user)
    {
        if (entityList == null || !entityList.Any())
            return Enumerable.Empty<UserContactDTO>();

        var viewModel = entityList.Select(p => p.ToDTO(user));
        return viewModel;
    }

    public static UserContactDTO ToDTO(this UserContact p, User user)
    {
        var viewModel = new UserContactDTO()
        {
            UUID = p.UUID,
            FirstName = p.FirstName,
            LastName = p.LastName,
            UserEmail = p.UserEmail,
            UserName = p.UserName,
            UserUUID = user.UUID
        };

        return viewModel;
    }
}
