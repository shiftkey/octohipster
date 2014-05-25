using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Newtonsoft.Json;
using OctoHipster.Logic;
using OctoHipster.Models;

namespace OctoHipster.Services
{
    public interface ICustomerService
    {
        Task<IEnumerable<Customer>> GetByName(string value);
    }

    public class CustomerService : ICustomerService
    {
        readonly Random _random = new Random();

        static IEnumerable<Customer> GetAll()
        {
            var json = Assembly.GetExecutingAssembly().GetFileContents("users.json");
            return JsonConvert.DeserializeObject<IEnumerable<Customer>>(json);
        }

        public async Task<IEnumerable<Customer>> GetByName(string value)
        {
            Trace.WriteLine("Fetching customers containing value: " + value);

            // emulate some network delay here before returning the data
            await Task.Delay(_random.Next(1000, 3000));

            // sometimes the request will fail, gotta handle that in the client
            if (_random.Next(100) > 90)
            {
                throw new InvalidOperationException("something bad happened");
            }

            return GetAll().Where(c => c.Name.ToLower().Contains(value.ToLower()));
        }
    }
}
