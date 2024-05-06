namespace Learning.FastEndpoionts.Features.User.Create;

/// <summary>
/// The user data of the request.
/// </summary>
public class Request
{
    /// <summary>
    /// The first name of the user.
    /// </summary>
    public required string FirstName { get; set; }

    /// <summary>
    /// The last name of the user.
    /// </summary>
    public required string LastName { get; set; }

    /// <summary>
    /// The age of the user.
    /// </summary>
    public int Age { get; set; }
}