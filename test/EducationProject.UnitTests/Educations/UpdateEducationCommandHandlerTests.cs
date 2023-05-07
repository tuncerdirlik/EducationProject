using AutoMapper;
using EducationMicroService.Application.Contracts;
using EducationMicroService.Application.DTOs;
using EducationMicroService.Application.Features.Handlers.Commands;
using EducationMicroService.Application.Features.Requests.Commands;
using EducationMicroService.Application.Profiles;
using EducationMicroService.Domain;
using Moq;

namespace EducationProject.UnitTests.Educations
{
    public class UpdateEducationCommandHandlerTests
    {
        private IMapper _mapper;
        private readonly Mock<IUnitOfWork> _mockUnitOfWork;

        public UpdateEducationCommandHandlerTests()
        {
            var mapperConfig = new MapperConfiguration(k =>
            {
                k.AddProfile<MappingProfile>();
            });

            _mapper = new Mapper(mapperConfig);

            _mockUnitOfWork = new Mock<IUnitOfWork>();
            var mockEducationRepository = new Mock<IEducationRepository>();
            _mockUnitOfWork.Setup(k => k.EducationRepository).Returns(mockEducationRepository.Object);
        }

        [Fact]
        public async Task Should_Fail_When_Item_Not_Found()
        {
            var mockEducationRepository = new Mock<IEducationRepository>();
            Education? education = null;
            mockEducationRepository.Setup(k => k.GetAsync(It.IsAny<int>())).ReturnsAsync(education);
            _mockUnitOfWork.Setup(k => k.EducationRepository).Returns(mockEducationRepository.Object);

            UpdateEducationCommand command = new UpdateEducationCommand();
            command.EducationDto = new EducationDto();
            command.EducationDto.Id = 0;

            var handler = new UpdateEducationCommandHandler(_mockUnitOfWork.Object, _mapper);
            var result = await handler.Handle(command, CancellationToken.None);

            Assert.False(result.Success);
            Assert.Contains(result.Errors, error => error.Equals("Item not found"));
        }

        [Fact]
        public async Task Should_Success_When_Item_Found()
        {
            var faker = new Bogus.Faker();

            var mockEducationRepository = new Mock<IEducationRepository>();
            Education? education = new Education();
            education.Title = faker.Random.String2(100);
            education.Description = faker.Random.String2(100);
            education.Link = faker.Random.String2(100);

            mockEducationRepository.Setup(k => k.GetAsync(It.IsAny<int>())).ReturnsAsync(education);
            _mockUnitOfWork.Setup(k => k.EducationRepository).Returns(mockEducationRepository.Object);

            UpdateEducationCommand command = new UpdateEducationCommand();
            command.EducationDto = new EducationDto();
            command.EducationDto.Id = 1;
            command.EducationDto.Title = new Bogus.Faker().Random.String2(100);
            command.EducationDto.Description = faker.Random.String2(100);
            command.EducationDto.Link = faker.Random.String2(100);

            var handler = new UpdateEducationCommandHandler(_mockUnitOfWork.Object, _mapper);
            var result = await handler.Handle(command, CancellationToken.None);

            Assert.True(result.Success);
        }
    }
}
