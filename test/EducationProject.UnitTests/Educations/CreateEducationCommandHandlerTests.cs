using AutoMapper;
using Bogus;
using EducationGroupMicroService.Application.DTOs;
using EducationGroupMicroService.Application.Features.Handlers.Commands;
using EducationMicroService.Application.Contracts;
using EducationMicroService.Application.DTOs;
using EducationMicroService.Application.DTOs.Validators;
using EducationMicroService.Application.Features.Handlers.Commands;
using EducationMicroService.Application.Features.Requests.Commands;
using EducationMicroService.Application.Profiles;
using Moq;

namespace EducationProject.UnitTests.Educations
{
    public class CreateEducationCommandHandlerTests
    {
        private IMapper _mapper;
        private readonly Mock<IUnitOfWork> _mockUnitOfWork;

        public CreateEducationCommandHandlerTests()
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
        public async Task Should_Fail_When_EducationGroupId_Not_Valid()
        {
            var educationDto = new CreateEducationDto();
            educationDto.EducationGroupId = 0;

            var validationResult = await new CreateEducationDtoValidator().ValidateAsync(educationDto);
            Assert.False(validationResult.IsValid);
            Assert.Contains(validationResult.Errors, error => error.ErrorMessage.Equals("Education Group Id must be valid"));
        }

        [Fact]
        public async Task Should_Fail_When_Title_Is_Empty()
        {
            var educationDto = new CreateEducationDto();
            educationDto.EducationGroupId = 1;
            educationDto.Title = string.Empty;

            var validationResult = await new CreateEducationDtoValidator().ValidateAsync(educationDto);
            Assert.False(validationResult.IsValid);
            Assert.Contains(validationResult.Errors, error => error.ErrorMessage.Equals("Title is required"));
        }

        [Fact]
        public async Task Should_Fail_When_Title_Greater_Than_250()
        {
            var educationDto = new CreateEducationDto();
            educationDto.EducationGroupId = 1;
            educationDto.Title = new Bogus.Faker().Random.String2(255);

            var validationResult = await new CreateEducationDtoValidator().ValidateAsync(educationDto);
            Assert.False(validationResult.IsValid);
            Assert.Contains(validationResult.Errors, error => error.ErrorMessage.Equals("Title must not be longer than 250"));
        }

        [Fact]
        public async Task Should_Fail_When_Description_Is_Empty()
        {
            var educationDto = new CreateEducationDto();
            educationDto.EducationGroupId = 1;
            educationDto.Title = new Bogus.Faker().Random.String2(100);

            var validationResult = await new CreateEducationDtoValidator().ValidateAsync(educationDto);
            Assert.False(validationResult.IsValid);
            Assert.Contains(validationResult.Errors, error => error.ErrorMessage.Equals("Description is required"));
        }

        [Fact]
        public async Task Should_Fail_When_Link_Is_Empty()
        {
            var faker = new Bogus.Faker();

            var educationDto = new CreateEducationDto();
            educationDto.EducationGroupId = 1;
            educationDto.Title = faker.Random.String2(100);
            educationDto.Description = faker.Random.String2(100);

            var validationResult = await new CreateEducationDtoValidator().ValidateAsync(educationDto);
            Assert.False(validationResult.IsValid);
            Assert.Contains(validationResult.Errors, error => error.ErrorMessage.Equals("Link is required"));
        }

        [Fact]
        public async Task Should_Fail_When_Link_Greater_Than_2000()
        {
            var faker = new Bogus.Faker();

            var educationDto = new CreateEducationDto();
            educationDto.EducationGroupId = 1;
            educationDto.Title = faker.Random.String2(100);
            educationDto.Description = faker.Random.String2(100);
            educationDto.Link = faker.Random.String2(2001);

            var validationResult = await new CreateEducationDtoValidator().ValidateAsync(educationDto);
            Assert.False(validationResult.IsValid);
            Assert.Contains(validationResult.Errors, error => error.ErrorMessage.Equals("Link must not be longer than 2000"));
        }

        [Fact]
        public async Task Should_Success_When_Validation_Success()
        {
            var faker = new Bogus.Faker();

            var educationDto = new CreateEducationDto();
            educationDto.EducationGroupId = 1;
            educationDto.Title = faker.Random.String2(100);
            educationDto.Description = faker.Random.String2(100);
            educationDto.Link = faker.Random.String2(2000);


            CreateEducationCommand command = new CreateEducationCommand();
            command.CreateEducationDto = educationDto;

            var handler = new CreateEducationCommandHandler(_mockUnitOfWork.Object, _mapper);
            var result = await handler.Handle(command, CancellationToken.None);

            Assert.True(result.Success);
        }

    }
}
