namespace ProgramLogic.Model.Interface;

public interface IAccount
{
    public string PinCode { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Email { get; set; }
    public string PhoneNumber { get; set; }
}