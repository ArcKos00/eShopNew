using Catalog.Host.Models.Enums;
using Moq;

namespace Catalog.UnitTests.Services
{
    public class ArtefactServiceTest
    {
        private readonly IArtefactService _service;

        private readonly Mock<IArtefactRepository> _repository;
        private readonly Mock<ILogger<ArtefactService>> _logger;
        private readonly Mock<IMapper> _mapper;
        private readonly Mock<IDbContextWrapper<ApplicationDbContext>> _wrapper;

        private readonly Artefact _test = new Artefact()
        {
            Name = "Test",
            Cost = 0,
            ImageUrl = "test",
            Nature = "Test"
        };
        private readonly ArtefactEntity _testEntity = new ArtefactEntity()
        {
            Name = "Test",
            Cost = 0,
            ImagePath = "Test",
            Nature = "Test"
        };

        public ArtefactServiceTest()
        {
            _repository = new Mock<IArtefactRepository>();
            _mapper = new Mock<IMapper>();
            _logger = new Mock<ILogger<ArtefactService>>();
            _wrapper = new Mock<IDbContextWrapper<ApplicationDbContext>>();

            var dbContextTransaction = new Mock<IDbContextTransaction>();
            _wrapper.Setup(s => s.BeginTransactionAsync(CancellationToken.None)).ReturnsAsync(dbContextTransaction.Object);

            _service = new ArtefactService(
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
            _repository.Setup(s => s.Add(
                It.IsAny<string>(),
                It.IsAny<decimal>(),
                It.IsAny<string>(),
                It.IsAny<string>(),
                It.IsAny<int>(),
                It.IsAny<int>(),
                It.IsAny<int>(),
                It.IsAny<int>())).ReturnsAsync(test);

            // act
            var result = await _service.Add(
                It.IsAny<string>(),
                It.IsAny<decimal>(),
                It.IsAny<string>(),
                It.IsAny<string>(),
                It.IsAny<int>(),
                It.IsAny<int>(),
                It.IsAny<int>(),
                It.IsAny<int>());

            // assert
            result.Should().Be(test);
            result.Should().BeGreaterThan(0);
        }

        [Fact]
        public async Task Add_Failed()
        {
            // arrange
            int? test = null;
            _repository.Setup(s => s.Add(
                It.IsAny<string>(),
                It.IsAny<decimal>(),
                It.IsAny<string>(),
                It.IsAny<string>(),
                It.IsAny<int>(),
                It.IsAny<int>(),
                It.IsAny<int>(),
                It.IsAny<int>())).ReturnsAsync(test);

            // act
            var result = await _service.Add(
                It.IsAny<string>(),
                It.IsAny<decimal>(),
                It.IsAny<string>(),
                It.IsAny<string>(),
                It.IsAny<int>(),
                It.IsAny<int>(),
                It.IsAny<int>(),
                It.IsAny<int>());

            // assert
            result.Should().BeNull();
        }

        [Fact]
        public async Task Get_Succesful()
        {
            // arrange
            _repository.Setup(s => s.Get(It.IsAny<int>())).ReturnsAsync(_testEntity);
            _mapper.Setup(s => s.Map<Artefact>(It.Is<ArtefactEntity>(i => i.Equals(_testEntity)))).Returns(_test);

            // act
            var result = await _service.Get(It.IsAny<int>());

            // assert
            result.Should().Be(_test);
        }

        [Fact]
        public async Task Get_Failed()
        {
            // arrange
            ArtefactEntity emptyEntity = null!;
            _repository.Setup(s => s.Get(It.IsAny<int>())).ReturnsAsync(emptyEntity);

            // act
            var result = await _service.Get(It.IsAny<int>());

            // assert
            result.Should().NotBeNull();
            result?.Id.Should().Be(0);
            result?.Name.Should().BeNullOrEmpty();
            result?.Cost.Should().Be(0);
            result?.ImageUrl.Should().BeNullOrEmpty();
        }

        [Fact]
        public async Task GetWithContent_Succesful()
        {
            // arrange
            var _testFill = new Artefact()
            {
                Name = "Test",
                Cost = 0,
                ImageUrl = "Test",
                Nature = "Test",
                Anomaly = new Anomaly(),
                Meets = new Frequency(),
                Type = new AbnormalType(),
                Values = new Characteristic()
            };
            var _testEntityFill = new ArtefactEntity()
            {
                Name = "Test",
                Cost = 0,
                ImagePath = "Test",
                Nature = "Test",
                Anomaly = new AnomalyEntity(),
                Meets = new FrequencyEntity(),
                Type = new AbnormalTypeEntity(),
                Values = new CharacteristicEntity()
            };
            _repository.Setup(s => s.GetWithContent(It.IsAny<int>())).ReturnsAsync(_testEntityFill);
            _mapper.Setup(s => s.Map<Artefact>(It.Is<ArtefactEntity>(i => i.Equals(_testEntityFill)))).Returns(_testFill);

            // act
            var result = await _service.GetWithContent(It.IsAny<int>());

            // assert
            result.Should().NotBeNull();
        }

        [Fact]
        public async Task GetWithContent_Failed()
        {
            // arrange
            var test = new Artefact();
            ArtefactEntity emptyEntity = null!;
            _repository.Setup(s => s.GetWithContent(It.IsAny<int>())).ReturnsAsync(emptyEntity);

            // act
            var result = await _service.GetWithContent(It.IsAny<int>());

            // assert
            result.Should().NotBeNull();
        }


        [Fact]
        public async Task GetPage_Succesful()
        {
            // arrange
            var testId = 1;
            var testPageIndex = 0;
            var testPageSize = 6;
            var testTotal = 10;
            var testResponse = new PaginatedItems<ArtefactEntity>()
            {
                TotalCount = testTotal,
                Data = new List<ArtefactEntity>()
                {
                    new ArtefactEntity()
                    {
                        AbnormalTypeId = testId,
                        AnomalyId = testId,
                        FrequencyId = testId,
                    }
                }
            };

            var filter = new Dictionary<TypeFilter, int>() { { TypeFilter.Type, testId }, { TypeFilter.Anomaly, testId }, { TypeFilter.Meets, testId } };
            _repository.Setup(s => s.GetPage(testPageIndex, testPageSize, testId, testId, testId)).ReturnsAsync(testResponse);
            _mapper.Setup(s => s.Map<Artefact>(It.Is<ArtefactEntity>(i => i.Equals(_testEntity)))).Returns(_test);

            // act
            var result = await _service.GetPage(testPageIndex, testPageSize, filter);

            // assert
            result.Should().NotBeNull();
            result?.Count.Should().Be(testTotal);
            result?.Data.Should().NotBeNull();
            result?.PageIndex.Should().Be(testPageIndex);
            result?.PageSize.Should().Be(testPageSize);
        }

        [Fact]
        public async Task GetPage_Succesful_Anomaly()
        {
            // arrange
            var testId = 1;
            var testPageIndex = 0;
            var testPageSize = 6;
            var testTotal = 10;
            var testResponse = new PaginatedItems<ArtefactEntity>()
            {
                TotalCount = testTotal,
                Data = new List<ArtefactEntity>()
                {
                    new ArtefactEntity()
                    {
                        AnomalyId = testId
                    }
                }
            };

            var filter = new Dictionary<TypeFilter, int>() { { TypeFilter.Anomaly, testId } };
            _repository.Setup(s => s.GetPage(testPageIndex, testPageSize, testId, null!, null!)).ReturnsAsync(testResponse);
            _mapper.Setup(s => s.Map<Artefact>(It.Is<ArtefactEntity>(i => i.Equals(_testEntity)))).Returns(_test);

            // act
            var result = await _service.GetPage(testPageIndex, testPageSize, filter);

            // assert
            result.Should().NotBeNull();
            result?.Count.Should().Be(testTotal);
            result?.Data.Should().NotBeNull();
            result?.PageIndex.Should().Be(testPageIndex);
            result?.PageSize.Should().Be(testPageSize);
        }

        [Fact]
        public async Task GetPage_Succesful_Abnormal()
        {
            // arrange
            var testId = 1;
            var testPageIndex = 0;
            var testPageSize = 6;
            var testTotal = 10;
            var testResponse = new PaginatedItems<ArtefactEntity>()
            {
                TotalCount = testTotal,
                Data = new List<ArtefactEntity>()
                {
                    new ArtefactEntity()
                    {
                        AbnormalTypeId = testId
                    }
                }
            };

            var filter = new Dictionary<TypeFilter, int>() { { TypeFilter.Type, testId } };
            _repository.Setup(s => s.GetPage(testPageIndex, testPageSize, null!, testId, null!)).ReturnsAsync(testResponse);
            _mapper.Setup(s => s.Map<Artefact>(It.Is<ArtefactEntity>(i => i.Equals(_testEntity)))).Returns(_test);

            // act
            var result = await _service.GetPage(testPageIndex, testPageSize, filter);

            // assert
            result.Should().NotBeNull();
            result?.Count.Should().Be(testTotal);
            result?.Data.Should().NotBeNull();
            result?.PageIndex.Should().Be(testPageIndex);
            result?.PageSize.Should().Be(testPageSize);
        }

        [Fact]
        public async Task GetPage_Succesful_Frequency()
        {
            // arrange
            var testId = 1;
            var testPageIndex = 0;
            var testPageSize = 6;
            var testTotal = 10;
            var testResponse = new PaginatedItems<ArtefactEntity>()
            {
                TotalCount = testTotal,
                Data = new List<ArtefactEntity>()
                {
                    new ArtefactEntity()
                    {
                        FrequencyId = testId
                    }
                }
            };

            var filter = new Dictionary<TypeFilter, int>() { { TypeFilter.Meets, testId } };
            _repository.Setup(s => s.GetPage(testPageIndex, testPageSize, null!, null!, testId)).ReturnsAsync(testResponse);
            _mapper.Setup(s => s.Map<Artefact>(It.Is<ArtefactEntity>(i => i.Equals(_testEntity)))).Returns(_test);

            // act
            var result = await _service.GetPage(testPageIndex, testPageSize, filter);

            // assert
            result.Should().NotBeNull();
            result?.Count.Should().Be(testTotal);
            result?.Data.Should().NotBeNull();
            result?.PageIndex.Should().Be(testPageIndex);
            result?.PageSize.Should().Be(testPageSize);
        }

        [Fact]
        public async Task GetPage_Failed()
        {
            // arrange
            _repository.Setup(s => s.GetPage(It.IsAny<int>(), It.IsAny<int>(), It.IsAny<int?>(), It.IsAny<int?>(), It.IsAny<int?>())).ReturnsAsync((PaginatedItems<ArtefactEntity>)null!);

            // act
            var result = await _service.GetPage(It.IsAny<int>(), It.IsAny<int>(), null!);

            // assert
            result?.Data.Should().BeNullOrEmpty();
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
        public async Task UpdateNature_Succesful()
        {
            // arrange
            var testNature = "Test";
            var testResult = true;
            _repository.Setup(s => s.UpdateNature(It.IsAny<int>(), testNature)).ReturnsAsync(testResult);

            // act
            var result = await _service.UpdateNature(It.IsAny<int>(), testNature);

            // assert
            result.Should().BeTrue();
        }

        [Fact]
        public async Task UpdateNature_Failed()
        {
            // arrange
            var testNature = "Test";
            var testResult = false;
            _repository.Setup(s => s.UpdateNature(It.IsAny<int>(), testNature)).ReturnsAsync(testResult);

            // act
            var result = await _service.UpdateNature(It.IsAny<int>(), testNature);

            // assert
            result.Should().BeFalse();
        }

        [Fact]
        public async Task UpdateImage_Succesful()
        {
            // arrange
            var testName = "Test";
            var testResult = true;
            _repository.Setup(s => s.UpdateImage(It.IsAny<int>(), testName)).ReturnsAsync(testResult);

            // act
            var result = await _service.UpdateImage(It.IsAny<int>(), testName);

            // assert
            result.Should().BeTrue();
        }

        [Fact]
        public async Task UpdateImage_Failed()
        {
            // arrange
            var testName = "Test";
            var testResult = false;
            _repository.Setup(s => s.UpdateImage(It.IsAny<int>(), testName)).ReturnsAsync(testResult);

            // act
            var result = await _service.UpdateImage(It.IsAny<int>(), testName);

            // assert
            result.Should().BeFalse();
        }

        [Fact]
        public async Task UpdateCost_Succesful()
        {
            // arrange
            var testName = 5m;
            var testResult = true;
            _repository.Setup(s => s.UpdateCost(It.IsAny<int>(), testName)).ReturnsAsync(testResult);

            // act
            var result = await _service.UpdateCost(It.IsAny<int>(), testName);

            // assert
            result.Should().BeTrue();
        }

        [Fact]
        public async Task UpdateCost_Failed()
        {
            // arrange
            var testName = 5m;
            var testResult = false;
            _repository.Setup(s => s.UpdateCost(It.IsAny<int>(), testName)).ReturnsAsync(testResult);

            // act
            var result = await _service.UpdateCost(It.IsAny<int>(), testName);

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
