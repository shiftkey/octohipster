using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Newtonsoft.Json;
using OctoHipster.Logic;
using OctoHipster.Models;

namespace OctoHipster.Services
{
    public class CustomerService
    {
        Random random = new Random();
        public IEnumerable<Customer> GetAll()
        {
            var json = Assembly.GetExecutingAssembly().GetFileContents("users.json");
            return JsonConvert.DeserializeObject<IEnumerable<Customer>>(json);
        }

        public async Task<IEnumerable<Customer>> GetByName(string value)
        {
            await Task.Delay(random.Next(1000, 2000));

            return GetAll().Where(c => c.Name.ToLower().Contains(value.ToLower()));
        }
    }
}
