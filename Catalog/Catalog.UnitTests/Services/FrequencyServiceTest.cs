using Moq;

namespace Catalog.UnitTests.Services
{
    public class FrequencyServiceTest
    {
        private readonly IFrequencyService _service;

        private readonly Mock<IFrequencyRepository> _repository;
        private readonly Mock<ILogger<FrequencyService>> _logger;
        private readonly Mock<IMapper> _mapper;
        private readonly Mock<IDbContextWrapper<ApplicationDbContext>> _wrapper;

        private readonly Frequency _test = new Frequency()
        {
            Meets = "Test",
        };
        private readonly FrequencyEntity _testEntity = new FrequencyEntity()
        {
            Meets = "Test",
        };

        public FrequencyServiceTest()
        {
            _repository = new Mock<IFrequencyRepository>();
            _mapper = new Mock<IMapper>();
            _logger = new Mock<ILogger<FrequencyService>>();
            _wrapper = new Mock<IDbContextWrapper<ApplicationDbContext>>();

            var dbContextTransaction = new Mock<IDbContextTransaction>();
            _wrapper.Setup(s => s.BeginTransactionAsync(CancellationToken.None)).ReturnsAsync(dbContextTransaction.Object);

            _service = new FrequencyService(
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
            _repository.Setup(s => s.Add(It.IsAny<string>())).ReturnsAsync(test);

            // act
            var result = await _service.Add(It.IsAny<string>());

            // assert
            result.Should().BeGreaterThan(0);
            result.Should().NotBeNull();
        }

        [Fact]
        public async Task Add_Failed()
        {
            // assert
            var test = 0;
            _repository.Setup(s => s.Add(It.IsAny<string>())).ReturnsAsync(test);

            // act
            var result = await _service.Add(It.IsAny<string>());

            // assert
            result.Should().BeLessThanOrEqualTo(0);
        }

        [Fact]
        public async Task Get_Succesful()
        {
            // assert
            _repository.Setup(s => s.Get(It.IsAny<int>())).ReturnsAsync(_testEntity);
            _mapper.Setup(s => s.Map<Frequency>(It.Is<FrequencyEntity>(i => i.Equals(_testEntity)))).Returns(_test);

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
            var empty = new Frequency();
            FrequencyEntity emptyEntity = null!;
            _repository.Setup(s => s.Get(It.IsAny<int>())).ReturnsAsync(emptyEntity);

            // act
            var result = await _service.Get(It.IsAny<int>());

            // assert
            result.Should().NotBeNull();
            result?.Id.Should().Be(0);
            result?.Meets.Should().BeNullOrEmpty();
        }

        [Fact]
        public async Task GetMeets_Succesful()
        {
            // arrange
            var empty = new List<Frequency>();
            var emptyEntity = new List<FrequencyEntity>()
            {
                _testEntity
            };
            _repository.Setup(s => s.GetMeets()).ReturnsAsync(emptyEntity);
            _mapper.Setup(s => s.Map<Frequency>(It.Is<FrequencyEntity>(i => i.Equals(_testEntity)))).Returns(_test);

            // act
            var result = await _service.GetMeets();

            // assert
            result.Should().NotBeNullOrEmpty();
        }

        [Fact]
        public async Task GetMeets_Failed()
        {
            // assert
            List<FrequencyEntity> emptyEntity = null!;
            _repository.Setup(s => s.GetMeets()).ReturnsAsync(emptyEntity);

            // act
            var result = await _service.GetMeets();

            // assert
            result.Should().BeEmpty();
        }

        [Fact]
        public async Task UpdateName_Succesful()
        {
            // arrange
            var testName = "Test";
            var testResult = true;
            _repository.Setup(s => s.UpdateMeet(It.IsAny<int>(), testName)).ReturnsAsync(testResult);

            // act
            var result = await _service.UpdateMeet(It.IsAny<int>(), testName);

            // assert
            result.Should().BeTrue();
        }

        [Fact]
        public async Task UpdateName_Failed()
        {
            // arrange
            var testName = "Test";
            var testResult = false;
            _repository.Setup(s => s.UpdateMeet(It.IsAny<int>(), testName)).ReturnsAsync(testResult);

            // act
            var result = await _service.UpdateMeet(It.IsAny<int>(), testName);

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
