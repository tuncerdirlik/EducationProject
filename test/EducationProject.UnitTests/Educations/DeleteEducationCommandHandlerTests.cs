using EducationMicroService.Application.Contracts;
using EducationMicroService.Application.Features.Handlers.Commands;
using EducationMicroService.Application.Features.Requests.Commands;
using EducationMicroService.Domain;
using Moq;

namespace EducationProject.UnitTests.Educations
{
    public class DeleteEducationCommandHandlerTests
    {
        private readonly Mock<IUnitOfWork> _mockUnitOfWork;

        public DeleteEducationCommandHandlerTests()
        {
            _mockUnitOfWork = new Mock<IUnitOfWork>();
        }

        [Fact]
        public async Task Should_Fail_When_Item_Not_Found()
        {
            var mockEducationRepository = new Mock<IEducationRepository>();
            Education? education = null;
            mockEducationRepository.Setup(k => k.GetAsync(It.IsAny<int>())).ReturnsAsync(education);
            _mockUnitOfWork.Setup(k => k.EducationRepository).Returns(mockEducationRepository.Object);

            DeleteEducationCommand command = new DeleteEducationCommand();
            command.Id = 0;

            var handler = new DeleteEducationCommandHandler(_mockUnitOfWork.Object);
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
            education.EducationGroupId = 1;
            education.Title = faker.Random.String2(100);
            education.Description = faker.Random.String2(100);
            education.Link = faker.Random.String2(100);

            mockEducationRepository.Setup(k => k.GetAsync(It.IsAny<int>())).ReturnsAsync(education);
            _mockUnitOfWork.Setup(k => k.EducationRepository).Returns(mockEducationRepository.Object);

            DeleteEducationCommand command = new DeleteEducationCommand();
            command.Id = 1;

            var handler = new DeleteEducationCommandHandler(_mockUnitOfWork.Object);
            var result = await handler.Handle(command, CancellationToken.None);

            Assert.True(result.Success);
        }
    }
}
