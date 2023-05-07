using AutoMapper;
using EducationGroupMicroService.Application.Contracts;
using EducationGroupMicroService.Application.DTOs;
using EducationGroupMicroService.Application.Features.Handlers.Commands;
using EducationGroupMicroService.Application.Features.Requests.Commands;
using EducationGroupMicroService.Application.Profiles;
using EducationGroupMicroService.Domain;
using Moq;


namespace EducationProject.UnitTests.EducationGroups
{
    public class UpdateEducationGroupCommandHandlerTests
    {
        private IMapper _mapper;
        private readonly Mock<IUnitOfWork> _mockUnitOfWork;

        public UpdateEducationGroupCommandHandlerTests()
        {
            var mapperConfig = new MapperConfiguration(k =>
            {
                k.AddProfile<MappingProfile>();
            });

            _mapper = new Mapper(mapperConfig);

            _mockUnitOfWork = new Mock<IUnitOfWork>();
            var mockEducationGroupRepository = new Mock<IEducationGroupRepository>();
            _mockUnitOfWork.Setup(k => k.EducationGroupRepository).Returns(mockEducationGroupRepository.Object);
        }

        [Fact]
        public async Task Should_Fail_When_Item_Not_Found()
        {
            var mockEducationGroupRepository = new Mock<IEducationGroupRepository>();
            EducationGroup? educationGroup = null;
            mockEducationGroupRepository.Setup(k => k.GetAsync(It.IsAny<int>())).ReturnsAsync(educationGroup);
            _mockUnitOfWork.Setup(k => k.EducationGroupRepository).Returns(mockEducationGroupRepository.Object);

            UpdateEducationGroupCommand command = new UpdateEducationGroupCommand();
            command.EducationGroupDto = new EducationGroupDto();
            command.EducationGroupDto.Id = 0;

            var handler = new UpdateEducationGroupCommandHandler(_mockUnitOfWork.Object, _mapper);
            var result = await handler.Handle(command, CancellationToken.None);

            Assert.False(result.Success);
            Assert.Contains(result.Errors, error => error.Equals("EducationGroup not found"));
        }

        [Fact]
        public async Task Should_Success_When_Item_Found()
        {
            var mockEducationGroupRepository = new Mock<IEducationGroupRepository>();
            EducationGroup? educationGroup = new EducationGroup();
            educationGroup.Title = new Bogus.Faker().Random.String2(100);
            educationGroup.StartDate = DateTime.Now;
            educationGroup.EndDate = DateTime.Now.AddDays(10);

            mockEducationGroupRepository.Setup(k => k.GetAsync(It.IsAny<int>())).ReturnsAsync(educationGroup);
            _mockUnitOfWork.Setup(k => k.EducationGroupRepository).Returns(mockEducationGroupRepository.Object);

            UpdateEducationGroupCommand command = new UpdateEducationGroupCommand();
            command.EducationGroupDto = new EducationGroupDto();
            command.EducationGroupDto.Id = 1;
            command.EducationGroupDto.Title = new Bogus.Faker().Random.String2(100);
            command.EducationGroupDto.StartDate = DateTime.Now;
            command.EducationGroupDto.EndDate = DateTime.Now.AddDays(10);

            var handler = new UpdateEducationGroupCommandHandler(_mockUnitOfWork.Object, _mapper);
            var result = await handler.Handle(command, CancellationToken.None);

            Assert.True(result.Success);
        }
    }
}
