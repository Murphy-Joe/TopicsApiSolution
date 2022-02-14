namespace TopicsApi.Models;

/*{
    "currentDeveloper": "Bob Smith",
    "phone": "555-1212",
    "email": "bob@aol.com",
    "lastChecked": "ISO 8601 String of a Date"
}*/

// The Method, What it is, Model
public record GetCurrentDeveloperModel(string currentDeveloper, string phone, string email, DateTime lastChecked);

