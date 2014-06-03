using System.Collections.Generic;
using System.Reactive.Linq;
using System.Threading.Tasks;
using Caliburn.Micro;
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
        public async Task PopulatesResultsFromSearchText()
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
            await viewModel.UpdateSearchResults.ExecuteAsync();

            // assert
            Assert.Equal(1, viewModel.MatchingCustomers.Count);
        }
    }
}
