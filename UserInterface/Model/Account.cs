namespace UserInterface;

public class Account : Entity, IAccount
{
    public string UserName { get; set; }
    public string Password { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Email { get; set; }
    public string PhoneNumber { get; set; }
    public int PermissionId { get; set; }

    // Load constructor from DB
    public Account(int id, string user_Name, string password, string first_Name, string last_Name,
        string email, string phone_Number, int permission_Id)
    {
        Id = id;
        UserName = user_Name;
        Password = password;
        FirstName = first_Name;
        LastName = last_Name;
        Email = email;
        PhoneNumber = phone_Number;
        PermissionId = permission_Id;
    }
    
    // Save constructor
    public Account(string userName, string password, string firstName, string lastName,
        string email, string phoneNumber, int permissionId)
    {
        UserName = userName;
        Password = password;
        FirstName = firstName;
        LastName = lastName;
        Email = email;
        PhoneNumber = phoneNumber;
        PermissionId = permissionId;
    }
}