using AutoFixture;
using FundConnRec.API.Controllers;
using FundConnRec.API.Repositories.Interfaces;
using FundConnRec.Models.Models;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace FundConnRec.UnitTests
{
    public class PortfolioControllerTests
    {
        public Fixture fixture;

        public Mock<IPortfolioRepository> portfolioRepository;

        public PortfoliosController portfolioController;
        public PortfolioControllerTests()
        {
            fixture = new Fixture();
            portfolioRepository = new Mock<IPortfolioRepository>();
            portfolioController = new PortfoliosController(portfolioRepository.Object);
        }

        [Theory]
        [InlineData("AB123456789")]
        public async Task GetPortfolioWithValidDataReturnsOKTest(string isin)
        {
            DateTime date = fixture.Create<DateTime>();
            Portfolio toBeReturned = fixture.Build<Portfolio>().With(x => x.ISIN, isin).With(x => x.Date, date).Without(x => x.Positions).Create();
            portfolioRepository.Setup(x => x.Get(It.IsAny<string>(), It.IsAny<DateTime>())).ReturnsAsync(toBeReturned);

            IActionResult actionResult = await portfolioController.GetPortfolio(isin, date);
            var okResult = actionResult as OkObjectResult;
            var result = okResult.Value as Portfolio;
            Assert.NotNull(result);
            Assert.Equal(result.ISIN, toBeReturned.ISIN);
            Assert.Equal(result.Date, toBeReturned.Date);
        }
    }
}
