using Moq;

namespace Catalog.UnitTests.Services
{
    public class CharacteristicServiceTest
    {
        private readonly ICharacteristicService _service;

        private readonly Mock<ICharacteristicRepository> _repository;
        private readonly Mock<ILogger<CharacteristicService>> _logger;
        private readonly Mock<IMapper> _mapper;
        private readonly Mock<IDbContextWrapper<ApplicationDbContext>> _wrapper;

        private readonly Characteristic _test = new Characteristic()
        {
            Radiation = 1,
            Restoration = 1,
            Saturation = 1,
            ElectricalProtection = 1,
            ChemicalProtection = 1,
            RestorationHealth = 1,
            MaximumWeight = 1,
            ProtectionDogs = 1,
            ThermalProtection = 1,
            WoundHealing = 1
        };
        private readonly CharacteristicEntity _testEntity = new CharacteristicEntity()
        {
            Radiation = 1,
            Restoration = 1,
            Saturation = 1,
            ElectricalProtection = 1,
            ChemicalProtection = 1,
            RestorationHealth = 1,
            MaximumWeight = 1,
            ProtectionDogs = 1,
            ThermalProtection = 1,
            WoundHealing = 1
        };

        public CharacteristicServiceTest()
        {
            _repository = new Mock<ICharacteristicRepository>();
            _mapper = new Mock<IMapper>();
            _logger = new Mock<ILogger<CharacteristicService>>();
            _wrapper = new Mock<IDbContextWrapper<ApplicationDbContext>>();

            var dbContextTransaction = new Mock<IDbContextTransaction>();
            _wrapper.Setup(s => s.BeginTransactionAsync(CancellationToken.None)).ReturnsAsync(dbContextTransaction.Object);

            _service = new CharacteristicService(
                _repository.Object,
                _logger.Object,
                _mapper.Object,
                _logger.Object,
                _wrapper.Object);
        }

        [Fact]
        public async Task Add_Succesful()
        {
            // assert
            var test = 4;
            _repository.Setup(s => s.Add(
                It.IsAny<int>(),
                It.IsAny<int>(),
                It.IsAny<int>(),
                It.IsAny<int>(),
                It.IsAny<int>(),
                It.IsAny<int>(),
                It.IsAny<int>(),
                It.IsAny<int>(),
                It.IsAny<int>(),
                It.IsAny<int>())).ReturnsAsync(test);

            // act
            var result = await _service.Add(
                It.IsAny<int>(),
                It.IsAny<int>(),
                It.IsAny<int>(),
                It.IsAny<int>(),
                It.IsAny<int>(),
                It.IsAny<int>(),
                It.IsAny<int>(),
                It.IsAny<int>(),
                It.IsAny<int>(),
                It.IsAny<int>());

            // assert
            result.Should().BeGreaterThan(0);
        }

        [Fact]
        public async Task Add_Failed()
        {
            // assert
            var test = 0;
            _repository.Setup(s => s.Add(
                It.IsAny<int>(),
                It.IsAny<int>(),
                It.IsAny<int>(),
                It.IsAny<int>(),
                It.IsAny<int>(),
                It.IsAny<int>(),
                It.IsAny<int>(),
                It.IsAny<int>(),
                It.IsAny<int>(),
                It.IsAny<int>())).ReturnsAsync(test);

            // act
            var result = await _service.Add(
                It.IsAny<int>(),
                It.IsAny<int>(),
                It.IsAny<int>(),
                It.IsAny<int>(),
                It.IsAny<int>(),
                It.IsAny<int>(),
                It.IsAny<int>(),
                It.IsAny<int>(),
                It.IsAny<int>(),
                It.IsAny<int>());

            // assert
            result.Should().BeLessThanOrEqualTo(0);
        }

        [Fact]
        public async Task Get_Succesful()
        {
            // assert
            _repository.Setup(s => s.Get(It.IsAny<int>())).ReturnsAsync(_testEntity);
            _mapper.Setup(s => s.Map<Characteristic>(It.Is<CharacteristicEntity>(i => i.Equals(_testEntity)))).Returns(_test);

            // act
            var result = await _service.Get(It.IsAny<int>());

            // assert
            result.Should().NotBeNull();
            result.Should().Be(_test);
            result.Radiation.Should().BeGreaterThan(0);
            result.Restoration.Should().BeGreaterThan(0);
            result.Saturation.Should().BeGreaterThan(0);
            result.ElectricalProtection.Should().BeGreaterThan(0);
            result.ChemicalProtection.Should().BeGreaterThan(0);
            result.RestorationHealth.Should().BeGreaterThan(0);
            result.MaximumWeight.Should().BeGreaterThan(0);
            result.ProtectionDogs.Should().BeGreaterThan(0);
            result.ThermalProtection.Should().BeGreaterThan(0);
            result.WoundHealing.Should().BeGreaterThan(0);
        }

        [Fact]
        public async Task Get_Failed()
        {
            // assert
            var empty = new Characteristic();
            CharacteristicEntity emptyEntity = null!;
            _repository.Setup(s => s.Get(It.IsAny<int>())).ReturnsAsync(emptyEntity);

            // act
            var result = await _service.Get(It.IsAny<int>());

            // assert
            result.Should().NotBeNull();
            result.Radiation.Should().Be(0);
            result.Restoration.Should().Be(0);
            result.Saturation.Should().Be(0);
            result.ElectricalProtection.Should().Be(0);
            result.ChemicalProtection.Should().Be(0);
            result.RestorationHealth.Should().Be(0);
            result.MaximumWeight.Should().Be(0);
            result.ProtectionDogs.Should().Be(0);
            result.ThermalProtection.Should().Be(0);
            result.WoundHealing.Should().Be(0);
        }

        [Fact]
        public async Task UpdateRadiation_Succesful()
        {
            // arrange
            var testValue = 2;
            var testResult = true;
            _repository.Setup(s => s.UpdateRadiation(It.IsAny<int>(), testValue)).ReturnsAsync(testResult);

            // act
            var result = await _service.UpdateRadiation(It.IsAny<int>(), testValue);

            // assert
            result.Should().BeTrue();
        }

        [Fact]
        public async Task UpdateRadiation_Failed()
        {
            // arrange
            var testValue = 1;
            var testResult = false;
            _repository.Setup(s => s.UpdateRadiation(It.IsAny<int>(), testValue)).ReturnsAsync(testResult);

            // act
            var result = await _service.UpdateRadiation(It.IsAny<int>(), testValue);

            // assert
            result.Should().BeFalse();
        }

        [Fact]
        public async Task UpdateRestoration_Succesful()
        {
            // arrange
            var testValue = 2;
            var testResult = true;
            _repository.Setup(s => s.UpdateRestoration(It.IsAny<int>(), testValue)).ReturnsAsync(testResult);

            // act
            var result = await _service.UpdateRestoration(It.IsAny<int>(), testValue);

            // assert
            result.Should().BeTrue();
        }

        [Fact]
        public async Task UpdateRestoration_Failed()
        {
            // arrange
            var testValue = 1;
            var testResult = false;
            _repository.Setup(s => s.UpdateRestoration(It.IsAny<int>(), testValue)).ReturnsAsync(testResult);

            // act
            var result = await _service.UpdateRestoration(It.IsAny<int>(), testValue);

            // assert
            result.Should().BeFalse();
        }

        [Fact]
        public async Task UpdateHealth_Succesful()
        {
            // arrange
            var testValue = 2;
            var testResult = true;
            _repository.Setup(s => s.UpdateHealth(It.IsAny<int>(), testValue)).ReturnsAsync(testResult);

            // act
            var result = await _service.UpdateHealth(It.IsAny<int>(), testValue);

            // assert
            result.Should().BeTrue();
        }

        [Fact]
        public async Task UpdateHealth_Failed()
        {
            // arrange
            var testValue = 1;
            var testResult = false;
            _repository.Setup(s => s.UpdateHealth(It.IsAny<int>(), testValue)).ReturnsAsync(testResult);

            // act
            var result = await _service.UpdateHealth(It.IsAny<int>(), testValue);

            // assert
            result.Should().BeFalse();
        }

        [Fact]
        public async Task UpdateWoundHealing_Succesful()
        {
            // arrange
            var testValue = 2;
            var testResult = true;
            _repository.Setup(s => s.UpdateWoundHealing(It.IsAny<int>(), testValue)).ReturnsAsync(testResult);

            // act
            var result = await _service.UpdateWoundHealing(It.IsAny<int>(), testValue);

            // assert
            result.Should().BeTrue();
        }

        [Fact]
        public async Task UpdateWoundHealing_Failed()
        {
            // arrange
            var testValue = 1;
            var testResult = false;
            _repository.Setup(s => s.UpdateWoundHealing(It.IsAny<int>(), testValue)).ReturnsAsync(testResult);

            // act
            var result = await _service.UpdateWoundHealing(It.IsAny<int>(), testValue);

            // assert
            result.Should().BeFalse();
        }

        [Fact]
        public async Task UpdateMaximumWeight_Succesful()
        {
            // arrange
            var testValue = 2;
            var testResult = true;
            _repository.Setup(s => s.UpdateMaximumWeight(It.IsAny<int>(), testValue)).ReturnsAsync(testResult);

            // act
            var result = await _service.UpdateMaximumWeight(It.IsAny<int>(), testValue);

            // assert
            result.Should().BeTrue();
        }

        [Fact]
        public async Task UpdateMaximumWeight_Failed()
        {
            // arrange
            var testValue = 1;
            var testResult = false;
            _repository.Setup(s => s.UpdateMaximumWeight(It.IsAny<int>(), testValue)).ReturnsAsync(testResult);

            // act
            var result = await _service.UpdateMaximumWeight(It.IsAny<int>(), testValue);

            // assert
            result.Should().BeFalse();
        }

        [Fact]
        public async Task UpdateProtecrionDogs_Succesful()
        {
            // arrange
            var testValue = 2;
            var testResult = true;
            _repository.Setup(s => s.UpdateProtecrionDogs(It.IsAny<int>(), testValue)).ReturnsAsync(testResult);

            // act
            var result = await _service.UpdateProtecrionDogs(It.IsAny<int>(), testValue);

            // assert
            result.Should().BeTrue();
        }

        [Fact]
        public async Task UpdateProtecrionDogs_Failed()
        {
            // arrange
            var testValue = 1;
            var testResult = false;
            _repository.Setup(s => s.UpdateProtecrionDogs(It.IsAny<int>(), testValue)).ReturnsAsync(testResult);

            // act
            var result = await _service.UpdateProtecrionDogs(It.IsAny<int>(), testValue);

            // assert
            result.Should().BeFalse();
        }

        [Fact]
        public async Task UpdateThermalProtection_Succesful()
        {
            // arrange
            var testValue = 2;
            var testResult = true;
            _repository.Setup(s => s.UpdateThermalProtection(It.IsAny<int>(), testValue)).ReturnsAsync(testResult);

            // act
            var result = await _service.UpdateThermalProtection(It.IsAny<int>(), testValue);

            // assert
            result.Should().BeTrue();
        }

        [Fact]
        public async Task UpdateThermalProtection_Failed()
        {
            // arrange
            var testValue = 1;
            var testResult = false;
            _repository.Setup(s => s.UpdateThermalProtection(It.IsAny<int>(), testValue)).ReturnsAsync(testResult);

            // act
            var result = await _service.UpdateThermalProtection(It.IsAny<int>(), testValue);

            // assert
            result.Should().BeFalse();
        }

        [Fact]
        public async Task UpdateChenmicalProtection_Succesful()
        {
            // arrange
            var testValue = 2;
            var testResult = true;
            _repository.Setup(s => s.UpdateChenmicalProtection(It.IsAny<int>(), testValue)).ReturnsAsync(testResult);

            // act
            var result = await _service.UpdateChenmicalProtection(It.IsAny<int>(), testValue);

            // assert
            result.Should().BeTrue();
        }

        [Fact]
        public async Task UpdateChenmicalProtection_Failed()
        {
            // arrange
            var testValue = 1;
            var testResult = false;
            _repository.Setup(s => s.UpdateChenmicalProtection(It.IsAny<int>(), testValue)).ReturnsAsync(testResult);

            // act
            var result = await _service.UpdateChenmicalProtection(It.IsAny<int>(), testValue);

            // assert
            result.Should().BeFalse();
        }

        [Fact]
        public async Task UpdateElectricalProtection_Succesful()
        {
            // arrange
            var testValue = 2;
            var testResult = true;
            _repository.Setup(s => s.UpdateElectricalProtection(It.IsAny<int>(), testValue)).ReturnsAsync(testResult);

            // act
            var result = await _service.UpdateElectricalProtection(It.IsAny<int>(), testValue);

            // assert
            result.Should().BeTrue();
        }

        [Fact]
        public async Task UpdateElectricalProtection_Failed()
        {
            // arrange
            var testValue = 1;
            var testResult = false;
            _repository.Setup(s => s.UpdateElectricalProtection(It.IsAny<int>(), testValue)).ReturnsAsync(testResult);

            // act
            var result = await _service.UpdateElectricalProtection(It.IsAny<int>(), testValue);

            // assert
            result.Should().BeFalse();
        }

        [Fact]
        public async Task UpdateSaturation_Succesful()
        {
            // arrange
            var testValue = 2;
            var testResult = true;
            _repository.Setup(s => s.UpdateSaturation(It.IsAny<int>(), testValue)).ReturnsAsync(testResult);

            // act
            var result = await _service.UpdateSaturation(It.IsAny<int>(), testValue);

            // assert
            result.Should().BeTrue();
        }

        [Fact]
        public async Task UpdateSaturation_Failed()
        {
            // arrange
            var testValue = 1;
            var testResult = false;
            _repository.Setup(s => s.UpdateSaturation(It.IsAny<int>(), testValue)).ReturnsAsync(testResult);

            // act
            var result = await _service.UpdateSaturation(It.IsAny<int>(), testValue);

            // assert
            result.Should().BeFalse();
        }

        [Fact]
        public async Task Delete_Succesful()
        {
            // arrange
            var testResult = true;
            _repository.Setup(s => s.Delete(It.IsAny<int>())).ReturnsAsync(testResult);

            // act
            var result = await _service.Delete(It.IsAny<int>());

            // assert
            result.Should().BeTrue();
        }

        [Fact]
        public async Task Delete_Failed()
        {
            // arrange
            var testResult = false;
            _repository.Setup(s => s.Delete(It.IsAny<int>())).ReturnsAsync(testResult);

            // act
            var result = await _service.Delete(It.IsAny<int>());

            // assert
            result.Should().BeFalse();

        }
    }
}
