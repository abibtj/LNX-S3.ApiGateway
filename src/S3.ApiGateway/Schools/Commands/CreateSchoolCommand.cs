using System;
using S3.Common.Messages;
using Newtonsoft.Json;

namespace S3.ApiGateway.Schools.Commands
{
    // Immutable
    // Custom routing key: #.registration
    [MessageNamespace("registration")]
    public class CreateSchoolCommand : ICommand
    {
        public Guid Id { get; private set; }
        public string Name { get; private set; }
        public string Category { get; private set; } // Primary, Secondary //***TODO: an enumeration might be better
        public string Address { get; private set; } //***TODO: create an address object
        public DateTime CreatedAt { get; private set; }

        [JsonConstructor]
        public CreateSchoolCommand(Guid id, string name, string category, string address)
        {
            //***TODO: Do proper validation of the parameters before assignment
            Id = id;
            Name = name;
            Category = category;
            Address = address;
            CreatedAt = DateTime.UtcNow;
        }
    }
}