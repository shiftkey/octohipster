using System.Collections.Generic;
using System.IO;
using System.Reflection;
using Newtonsoft.Json;
using OctoHipster.Logic;
using OctoHipster.Models;

namespace OctoHipster.Services
{
    public class CustomerService
    {
        public IEnumerable<Customer> GetAll()
        {
            var json = Assembly.GetExecutingAssembly().GetFileContents("users.json");
            return JsonConvert.DeserializeObject<IEnumerable<Customer>>(json);
        }
    }
}
