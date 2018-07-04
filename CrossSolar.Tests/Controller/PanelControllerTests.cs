using System;
using System.Linq;
using System.Threading.Tasks;
using CrossSolar.Controllers;
using CrossSolar.Domain;
using CrossSolar.Models;
using CrossSolar.Repository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Protocols;
using Moq;
using Unity;
using Xunit;

namespace CrossSolar.Tests.Controller
{
    public class PanelControllerTests 
    {

        public PanelControllerTests() 
        {
            _panelController = new PanelController(_panelRepositoryMock.Object);
        }
        private readonly CrossSolarDbContext dbContext = new CrossSolarDbContext();

        private readonly PanelController _panelController;


        private AnalyticsController _analyticsController = new AnalyticsController(new AnalyticsRepository(new CrossSolarDbContext()), new PanelRepository(new CrossSolarDbContext()) );
       
        //private readonly PanelRepository panelRepository = new PanelRepository(dbContext);

        private readonly Mock<IPanelRepository> _panelRepositoryMock = new Mock<IPanelRepository>();

        [Fact]
        public async Task Register_ShouldInsertPanel()
        {
            var panel = new PanelModel
            {
                Brand = "Areva",
                Latitude = 12.345678,
                Longitude = 98.7655432,
                Serial = "XXXX1111YYYY2222"
            };

            // Arrange

            // Act
            var result = await _panelController.Register(panel);

            // Assert
            Assert.NotNull(result);

            var createdResult = result as CreatedResult;
            Assert.NotNull(createdResult);
            Assert.Equal(201, createdResult.StatusCode);
        }

        [Fact]
        public void AnalyticsRepository()
        {
            //CrossSolarDbContext dbContext = new CrossSolarDbContext();
            var result = new AnalyticsRepository(dbContext);
        }

        [Fact]
        public void DayAnalyticsRepository()
        {
            //CrossSolarDbContext dbContext = new CrossSolarDbContext();
            var result = new DayAnalyticsRepository(dbContext);
        }
        [Fact]
        public void PanelRepository()
        {
            //CrossSolarDbContext dbContext = new CrossSolarDbContext();
            var result = new PanelRepository(dbContext);
        }

        [Fact]
        public async Task GenericRepositoryGetAsync()
        {

            var panel = new PanelModel
            {
                Id = 1,
                Brand = "Areva",
                Latitude = 12.345678,
                Longitude = 98.7655432,
                Serial = "XXXX1111YYYY2222"
            };
            Panel _panel = new Panel
            {
                Id = 1,
                Brand = "Brand1",
                Latitude = 2.998877,
                Longitude = 33.887766,
                Serial = "1234567890123456"
            };

            GenericRepositoryClass genericRepositoryClass = new GenericRepositoryClass();
            Panel panel1 = await genericRepositoryClass.GetAsync(_panel.Id.ToString());

            // Arrange

            // Act
            //var result = await _panelController.Register(panel1);

            //// Assert
            //Assert.NotNull(result);

            //var createdResult = result as CreatedResult;
            //Assert.NotNull(createdResult);
            //Assert.Equal(201, createdResult.StatusCode);

        }
        [Fact]
        public async Task GenericRepositoryInsertAsync()
        {          

            GenericRepositoryClass genericRepositoryClass = new GenericRepositoryClass();
            await genericRepositoryClass.InsertAsync(genericRepositoryClass._panelNew);

            // Arrange

            // Act
            //var result = await _panelController.Register(genericRepositoryClass._panel);

            // Assert
            //Assert.NotNull(result);

            //var createdResult = result as CreatedResult;
            //Assert.NotNull(createdResult);
            //Assert.Equal(201, createdResult.StatusCode);
        }
        [Fact]
        public async Task GenericRepositoryUpdateAsync()
        {

            GenericRepositoryClass genericRepositoryClass = new GenericRepositoryClass();
            await genericRepositoryClass.UpdateAsync(genericRepositoryClass._panelUpdate);

            // Arrange

            // Act
            //var result = await _panelController.Register(genericRepositoryClass._panel);

            // Assert
            //Assert.NotNull(result);

            //var createdResult = result as CreatedResult;
            //Assert.NotNull(createdResult);
            //Assert.Equal(201, createdResult.StatusCode);
        }


        private class GenericRepositoryClass : GenericRepository<Panel>
        {
            //public static PanelModel panel = new PanelModel();
            public Panel _panel = new Panel
            {
                Id = 1,
                Brand = "Brand1",
                Latitude = 2.998877,
                Longitude = 33.887766,
                Serial = "1234567890123456"
            };
            public Panel _panelUpdate = new Panel
            {
                Id = 1,
                Brand = "Brand test",
                Latitude = 2.977877,
                Longitude = 33.811766,
                Serial = "1234567890123111"
            };
            public Panel _panelNew = new Panel
            {
                Brand = "Brand12",
                Latitude = 2.999800,
                Longitude = 33.887116,
                Serial = "1234567890123477"
            };
            public GenericRepositoryClass() { }
        }



        [Fact]
        public async Task AnalyticsGet()
        {
            Panel _panel = new Panel
            {
                Id = 1,
                Brand = "Brand1",
                Latitude = 2.998877,
                Longitude = 33.887766,
                Serial = "1234567890123111"
            };

            // Arrange

            // Act
            IActionResult result = await _analyticsController.Get(_panel.Serial.ToString());

            // Assert
            Assert.NotNull(result);

            var createdResult = result as CreatedResult;
            Assert.NotNull(createdResult);
            Assert.Equal(201, createdResult.StatusCode);
        }
        [Fact]
        public async Task AnalyticsDayResults()
        {
            Panel panel = new Panel
            {
                Brand = "Brand12",
                Latitude = 2.9998,
                Longitude = 33.887116,
                Serial = "1234567890123477",
                Id = 4
            };

            // Arrange

            // Act
            IActionResult result = await _analyticsController.DayResults(panel.Id.ToString());

            // Assert
            Assert.NotNull(result);

            var createdResult = result as CreatedResult;
            Assert.NotNull(createdResult);
            Assert.Equal(201, createdResult.StatusCode);
        }
        [Fact]
        public async Task AnalyticsPost()
        {
            Panel _panel = new Panel
            {
                Id = 3,
                Brand = "Brand1",
                Latitude = 2.998877,
                Longitude = 33.887766,
                Serial = "1234567890123456"
            };
            var oneHourElectricityModel = new OneHourElectricityModel()
            {
                Id = 4,
                KiloWatt = 9223372036854775807,
                DateTime = DateTime.Now
            };

            // Arrange

            // Act
            IActionResult result = await _analyticsController.Post(_panel.Id.ToString(), oneHourElectricityModel);

            // Assert
            Assert.NotNull(result);

            var createdResult = result as CreatedResult;
            Assert.NotNull(createdResult);
            Assert.Equal(201, createdResult.StatusCode);
        }



    }
}