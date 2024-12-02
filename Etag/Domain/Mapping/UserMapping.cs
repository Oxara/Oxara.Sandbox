namespace ETag.Delta;

public static partial class MappingHelper
{
    public static IEnumerable<UserDTO> ToDTO(this IEnumerable<User> entityList)
    {
        var viewModel = entityList.Select(user => user.ToDTO());
        return viewModel;
    }

    public static UserDTO ToDTO(this User entity)
    {
        var viewModel = new UserDTO
        {
            UUID = entity.UUID,
            UserName = entity.UserName,
            FirstName = entity.FirstName,
            LastName = entity.LastName,
            UserEmail = entity.UserEmail,
            Birthday = entity.Birthday,
            UserFullName = entity.FirstName + ' ' + entity.LastName,
            Age = CalculateAge(entity.Birthday),
            UserContacts = entity.UserContacts.ToDTO(entity)
        };

        return viewModel;
    }

    private static int CalculateAge(DateOnly birthday)
    {
        var today = DateOnly.FromDateTime(DateTime.Today);
        var age = today.Year - birthday.Year;

        if (birthday > today.AddYears(-age)) age--;
        return age;
    }
}
