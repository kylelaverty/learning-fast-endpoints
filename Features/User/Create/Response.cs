namespace Learning.FastEndpoionts.Features.User.Create; 

/// <summary>
/// The response object for the User Create endpoint.
/// </summary>
public class Response
{
    /// <summary>
    /// The full name of the user.
    /// </summary>
    public required string FullName { get; set; }

    /// <summary>
    /// If the user is over 18.
    /// </summary>
    public bool IsOver18 { get; set; }
}