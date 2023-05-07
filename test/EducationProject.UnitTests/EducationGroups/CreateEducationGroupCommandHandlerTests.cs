using AutoMapper;
using EducationGroupMicroService.Application.Contracts;
using EducationGroupMicroService.Application.DTOs;
using EducationGroupMicroService.Application.DTOs.Validators;
using EducationGroupMicroService.Application.Features.Handlers.Commands;
using EducationGroupMicroService.Application.Features.Requests.Commands;
using EducationGroupMicroService.Application.Profiles;
using Moq;

namespace EducationProject.UnitTests.EducationGroups
{
    public class CreateEducationGroupCommandHandlerTests
    {
        private IMapper _mapper;
        private readonly Mock<IUnitOfWork> _mockUnitOfWork;

        public CreateEducationGroupCommandHandlerTests()
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
        public async Task Should_Fail_When_Title_Is_Empty()
        {
            var educationGroupDto = new CreateEducationGroupDto();
            educationGroupDto.Title = string.Empty;

            var validationResult = await new CreateEducationGroupDtoValidator().ValidateAsync(educationGroupDto);
            Assert.False(validationResult.IsValid);
            Assert.Contains(validationResult.Errors, error => error.ErrorMessage.Equals("Title is required"));
        }

        [Fact]
        public async Task Should_Fail_When_Title_Greater_Than_250()
        {
            var educationGroupDto = new CreateEducationGroupDto();
            educationGroupDto.Title = new Bogus.Faker().Random.String2(255);

            var validationResult = await new CreateEducationGroupDtoValidator().ValidateAsync(educationGroupDto);
            Assert.False(validationResult.IsValid);
            Assert.Contains(validationResult.Errors, error => error.ErrorMessage.Equals("Title must not be longer than 250"));
        }

        [Fact]
        public async Task Should_Fail_When_StartDate_Not_Valid()
        {
            var educationGroupDto = new CreateEducationGroupDto();
            educationGroupDto.Title = new Bogus.Faker().Random.String2(100);

            var validationResult = await new CreateEducationGroupDtoValidator().ValidateAsync(educationGroupDto);
            Assert.False(validationResult.IsValid);
            Assert.Contains(validationResult.Errors, error => error.ErrorMessage.Equals("Start Date must be a valid date"));
        }

        [Fact]
        public async Task Should_Fail_When_EndDate_Not_Valid()
        {
            var educationGroupDto = new CreateEducationGroupDto();
            educationGroupDto.Title = new Bogus.Faker().Random.String2(100);
            educationGroupDto.StartDate = DateTime.Now;

            var validationResult = await new CreateEducationGroupDtoValidator().ValidateAsync(educationGroupDto);
            Assert.False(validationResult.IsValid);
            Assert.Contains(validationResult.Errors, error => error.ErrorMessage.Equals("End Date must be a valid date"));
        }

        [Fact]
        public async Task Should_Success_When_Valid()
        {
            CreateEducationGroupCommand command = new CreateEducationGroupCommand();
            command.CreateEducationGroupDto = new CreateEducationGroupDto();
            command.CreateEducationGroupDto.Title = new Bogus.Faker().Random.String2(100);
            command.CreateEducationGroupDto.StartDate = DateTime.Now;
            command.CreateEducationGroupDto.EndDate = DateTime.Now.AddDays(10);

            var handler = new CreateEducationGroupCommandHandler(_mockUnitOfWork.Object, _mapper);
            var result = await handler.Handle(command, CancellationToken.None);

            Assert.True(result.Success);
        }
    }
}
