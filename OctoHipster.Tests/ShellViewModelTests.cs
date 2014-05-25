using System.Collections.Generic;
using System.Threading.Tasks;
using NSubstitute;
using OctoHipster.Models;
using OctoHipster.Services;
using OctoHipster.ViewModels;
using Xunit;

namespace OctoHipster.Tests
{
    public class ShellViewModelTests
    {
        [Fact]
        public void PopulatesResultsFromSearchText()
        {
            // arrange
            var service = Substitute.For<ICustomerService>();
            IEnumerable<Customer> customers = new List<Customer>
            {
                new Customer()
            };
            service.GetByName("hello").Returns(Task.FromResult(customers));
            var viewModel = new ShellViewModel(service);

            // act
            viewModel.SearchText = "hello";

            // assert
            Assert.Equal(1, viewModel.MatchingCustomers.Count);
        }
    }
}
