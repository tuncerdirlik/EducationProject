using EducationGroupMicroService.Application.Contracts;
using EducationGroupMicroService.Application.Features.Handlers.Commands;
using EducationGroupMicroService.Application.Features.Requests.Commands;
using EducationGroupMicroService.Domain;
using Moq;

namespace EducationProject.UnitTests.EducationGroups
{
    public class DeleteEducationGroupCommandHandlerTests
    {
        private readonly Mock<IUnitOfWork> _mockUnitOfWork;

        public DeleteEducationGroupCommandHandlerTests()
        {
            _mockUnitOfWork = new Mock<IUnitOfWork>();
        }

        [Fact]
        public async Task Should_Fail_When_Item_Not_Found()
        {
            var mockEducationGroupRepository = new Mock<IEducationGroupRepository>();
            EducationGroup? educationGroup = null;
            mockEducationGroupRepository.Setup(k => k.GetAsync(It.IsAny<int>())).ReturnsAsync(educationGroup);
            _mockUnitOfWork.Setup(k => k.EducationGroupRepository).Returns(mockEducationGroupRepository.Object);

            DeleteEducationGroupCommand command = new DeleteEducationGroupCommand();
            command.Id = 0;

            var handler = new DeleteEducationGroupCommandHandler(_mockUnitOfWork.Object);
            var result = await handler.Handle(command, CancellationToken.None);

            Assert.False(result.Success);
            Assert.Contains(result.Errors, error => error.Equals("Item not found"));
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

            DeleteEducationGroupCommand command = new DeleteEducationGroupCommand();
            command.Id = 1;

            var handler = new DeleteEducationGroupCommandHandler(_mockUnitOfWork.Object);
            var result = await handler.Handle(command, CancellationToken.None);

            Assert.True(result.Success);
        }
    }
}
