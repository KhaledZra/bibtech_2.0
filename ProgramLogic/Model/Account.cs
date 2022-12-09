namespace ProgramLogic.Model;

using ProgramLogic.Model.Interface;

public class Account : Entity, IAccount
{
    public string PinCode { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Email { get; set; }
    public string PhoneNumber { get; set; }

    // Load constructor from DB
    public Account(int id, string pin_code, string first_Name, string last_Name,
        string email, string phone_Number)
    {
        Id = id;
        PinCode = pin_code;
        FirstName = first_Name;
        LastName = last_Name;
        Email = email;
        PhoneNumber = phone_Number;
    }
    
    // Save constructor
    public Account(string pinCode, string firstName, string lastName,
        string email, string phoneNumber)
    {
        PinCode = pinCode;
        FirstName = firstName;
        LastName = lastName;
        Email = email;
        PhoneNumber = phoneNumber;
    }

    public Account()
    {
        
    }
}