using Infrastructure.Services;

namespace Catalog.UnitTests.Services
{
    public class AbnormalServiceTest
    {
        private readonly IAbnormalTypeService _service;

        private readonly Mock<IAbnormalTypeRepository> _repository;
        private readonly Mock<ILogger<AbnormalTypeService>> _logger;
        private readonly Mock<IMapper> _mapper;
        private readonly Mock<IDbContextWrapper<ApplicationDbContext>> _wrapper;

        private readonly AbnormalType _test = new AbnormalType()
        {
            Name = "Test",
        };
        private readonly AbnormalTypeEntity _testEntity = new AbnormalTypeEntity()
        {
            Name = "Test",
        };

        public AbnormalServiceTest()
        {
            _repository = new Mock<IAbnormalTypeRepository>();
            _mapper = new Mock<IMapper>();
            _logger = new Mock<ILogger<AbnormalTypeService>>();
            _wrapper = new Mock<IDbContextWrapper<ApplicationDbContext>>();

            var dbContextTransaction = new Mock<IDbContextTransaction>();
            _wrapper.Setup(s => s.BeginTransactionAsync(CancellationToken.None)).ReturnsAsync(dbContextTransaction.Object);

            _service = new AbnormalTypeService(
                _repository.Object,
                _logger.Object,
                _mapper.Object,
                _logger.Object,
                _wrapper.Object);
        }

        [Fact]
        public async Task Add_Succesful()
        {
            // arrange
            var test = 5;
            _repository.Setup(s => s.Add(It.IsAny<string>())).ReturnsAsync(test);

            // act
            var result = await _service.Add(It.IsAny<string>());

            // assert
            result.Should().Be(test);
            result.Should().BeGreaterThan(0);
        }

        [Fact]
        public async Task Add_Failed()
        {
            // arrange
            int? test = null;
            _repository.Setup(s => s.Add(It.IsAny<string>())).ReturnsAsync(test);

            // act
            var result = await _service.Add(It.IsAny<string>());

            // assert
            result.Should().BeNull();
        }

        [Fact]
        public async Task Get_Succesful()
        {
            // arrange
            _repository.Setup(s => s.Get(It.IsAny<int>())).ReturnsAsync(_testEntity);
            _mapper.Setup(s => s.Map<AbnormalType>(It.Is<AbnormalTypeEntity>(i => i.Equals(_testEntity)))).Returns(_test);

            // act
            var result = await _service.Get(It.IsAny<int>());

            // assert
            result.Should().Be(_test);
        }

        [Fact]
        public async Task Get_Failed()
        {
            // arrange
            AbnormalTypeEntity emptyEntity = null!;
            _repository.Setup(s => s.Get(It.IsAny<int>())).ReturnsAsync(emptyEntity);

            // act
            var result = await _service.Get(It.IsAny<int>());

            // assert
            result.Should().NotBeNull();
            result?.Id.Should().Be(0);
            result?.Name.Should().BeNullOrEmpty();
        }

        [Fact]
        public async Task GetTypes_Succesful()
        {
            // arrange
            var empty = new List<AbnormalType>();
            var emptyEntity = new List<AbnormalTypeEntity>()
            {
                _testEntity
            };
            _repository.Setup(s => s.GetTypes()).ReturnsAsync(emptyEntity);
            _mapper.Setup(s => s.Map<AbnormalType>(It.Is<AbnormalTypeEntity>(i => i.Equals(_testEntity)))).Returns(_test);

            // act
            var result = await _service.GetTypes();

            // assert
            result.Should().NotBeNull();
        }

        [Fact]
        public async Task GetTypes_Failed()
        {
            // arrange
            List<AbnormalTypeEntity> emptyEntity = null!;
            _repository.Setup(s => s.GetTypes()).ReturnsAsync(emptyEntity);

            // act
            var result = await _service.GetTypes();

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
