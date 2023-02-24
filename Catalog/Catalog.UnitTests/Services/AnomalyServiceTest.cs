namespace Catalog.UnitTests.Services
{
    public class AnomalyServiceTest
    {
        private readonly IAnomalyService _service;

        private readonly Mock<IAnomalyRepository> _repository;
        private readonly Mock<ILogger<AnomalyService>> _logger;
        private readonly Mock<IMapper> _mapper;
        private readonly Mock<IDbContextWrapper<ApplicationDbContext>> _wrapper;

        private readonly Anomaly _test = new Anomaly()
        {
            Name = "Test",
        };
        private readonly AnomalyEntity _testEntity = new AnomalyEntity()
        {
            Name = "Test",
        };

        public AnomalyServiceTest()
        {
            _repository = new Mock<IAnomalyRepository>();
            _mapper = new Mock<IMapper>();
            _logger = new Mock<ILogger<AnomalyService>>();
            _wrapper = new Mock<IDbContextWrapper<ApplicationDbContext>>();

            var dbContextTransaction = new Mock<IDbContextTransaction>();
            _wrapper.Setup(s => s.BeginTransactionAsync(CancellationToken.None)).ReturnsAsync(dbContextTransaction.Object);

            _service = new AnomalyService(
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
            _repository.Setup(s => s.Add(It.IsAny<string>(), It.IsAny<int>(), It.IsAny<int>(), It.IsAny<int>())).ReturnsAsync(test);

            // act
            var result = await _service.Add(It.IsAny<string>(), It.IsAny<int>(), It.IsAny<int>(), It.IsAny<int>());

            // assert
            result.Should().BeGreaterThan(0);
        }

        [Fact]
        public async Task Add_Failed()
        {
            // assert
            var test = 0;
            _repository.Setup(s => s.Add(It.IsAny<string>(), It.IsAny<int>(), It.IsAny<int>(), It.IsAny<int>())).ReturnsAsync(test);

            // act
            var result = await _service.Add(It.IsAny<string>(), It.IsAny<int>(), It.IsAny<int>(), It.IsAny<int>());

            // assert
            result.Should().BeLessThanOrEqualTo(0);
        }

        [Fact]
        public async Task Get_Succesful()
        {
            // assert
            _repository.Setup(s => s.Get(It.IsAny<int>())).ReturnsAsync(_testEntity);
            _mapper.Setup(s => s.Map<Anomaly>(It.Is<AnomalyEntity>(i => i.Equals(_testEntity)))).Returns(_test);

            // act
            var result = await _service.Get(It.IsAny<int>());

            // assert
            result.Should().NotBeNull();
            result.Should().Be(_test);
        }

        [Fact]
        public async Task Get_Failed()
        {
            // assert
            var empty = new Anomaly();
            AnomalyEntity emptyEntity = null!;
            _repository.Setup(s => s.Get(It.IsAny<int>())).ReturnsAsync(emptyEntity);

            // act
            var result = await _service.Get(It.IsAny<int>());

            // assert
            result.Should().NotBeNull();
            result?.Id.Should().Be(0);
            result?.Name.Should().BeNullOrEmpty();
        }

        [Fact]
        public async Task GetClasses_Succesful()
        {
            // arrange
            var empty = new List<Anomaly>();
            var emptyEntity = new List<AnomalyEntity>()
            {
                _testEntity
            };
            _repository.Setup(s => s.GetAnomaly()).ReturnsAsync(emptyEntity);
            _mapper.Setup(s => s.Map<Anomaly>(It.Is<AnomalyEntity>(i => i.Equals(_testEntity)))).Returns(_test);

            // act
            var result = await _service.GetAnomaly();

            // assert
            result.Should().NotBeNullOrEmpty();
        }

        [Fact]
        public async Task GetClasses_Failed()
        {
            // assert
            List<AnomalyEntity> emptyEntity = null!;
            _repository.Setup(s => s.GetAnomaly()).ReturnsAsync(emptyEntity);

            // act
            var result = await _service.GetAnomaly();

            // assert
            result.Should().BeEmpty();
        }

        [Fact]
        public async Task UpdateName_Succesful()
        {
            // arrange
            var testName = "Test";
            var testResult = true;
            _repository.Setup(s => s.UpdateName(It.IsAny<int>(), testName)).ReturnsAsync(testResult);

            // act
            var result = await _service.UpdateName(It.IsAny<int>(), testName);

            // assert
            result.Should().BeTrue();
        }

        [Fact]
        public async Task UpdateName_Failed()
        {
            // arrange
            var testName = "Test";
            var testResult = false;
            _repository.Setup(s => s.UpdateName(It.IsAny<int>(), testName)).ReturnsAsync(testResult);

            // act
            var result = await _service.UpdateName(It.IsAny<int>(), testName);

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
