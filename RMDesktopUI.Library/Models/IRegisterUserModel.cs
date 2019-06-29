namespace RMDesktopUI.Library.Models
{
    public interface IRegisterUserModel
    {
        string EmailAddress { get; set; }
        string FirstName { get; set; }
        string LastName { get; set; }
        string Password { get; set; }
    }
}