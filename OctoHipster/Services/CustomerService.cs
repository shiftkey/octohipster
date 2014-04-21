using System.Collections.Generic;
using System.IO;
using System.Reflection;
using Newtonsoft.Json;
using OctoHipster.Models;

namespace OctoHipster.Services
{
    public class CustomerService
    {
        public IEnumerable<Customer> GetAll()
        {
            var currentDirectory = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            var dataFile = Path.Combine(currentDirectory, "data", "users.json");
            var json = File.ReadAllText(dataFile);
            var users = JsonConvert.DeserializeObject<IEnumerable<Customer>>(json);
            return users;
        }
    }
}
