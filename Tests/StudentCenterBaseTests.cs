using AutoMapper;
using Moq;
using StudentCenterApi.src.Application.DTOs.StudentCenter;
using StudentCenterApi.src.Application.Services;
using StudentCenterApi.src.Domain.Interfaces;
using StudentCenterApi.src.Domain.Model;
using StudentCenterBaseTests.Enum;

namespace StudentCenterBaseTests.Tests;

public class StudentCenterBaseTests
{
    private readonly Mock<IStudentCenterBaseRepository> _repository;
    private readonly Mock<IMapper> _mapper;
    private readonly StudentCenterBaseService _service;

    public StudentCenterBaseTests()
    {
        _repository = new Mock<IStudentCenterBaseRepository>();
        _mapper = new Mock<IMapper>();
        _service = new StudentCenterBaseService(_repository.Object, _mapper.Object);
    }

    [Fact]
    public async Task Create_sending_more_than_20_characteres_page()
    {
        var studentCenterBaseCreateDto = new StudentCenterBaseCreateDto 
        { 
            Description = EMsg.ValidCharacters,
            Page = EMsg.MoreThan20Charaters
        };

        var studentCenterBaseDto = new StudentCenterBaseDto 
        {
            Description = EMsg.ValidCharacters,
            Page = EMsg.MoreThan20Charaters
        };

        var studentCenterBase = new StudentCenterBase 
        { 
            Id = 1,
            Description = EMsg.ValidCharacters,
            Page = EMsg.MoreThan20Charaters
        };

        _mapper.Setup(x => x.Map<StudentCenterBase>(studentCenterBaseCreateDto)).Returns(studentCenterBase);

        _repository.Setup(x => x.Post(studentCenterBase)).ReturnsAsync(studentCenterBase);

        _mapper.Setup(x => x.Map<StudentCenterBaseDto>(studentCenterBase)).Returns(studentCenterBaseDto);

        var exception = await Assert.ThrowsAsync<Exception>(() => _service.Post(studentCenterBaseCreateDto));

        Assert.Equal("Page can be at most 20 characters", exception.Message);
    }

    [Fact]
    public async Task Create_sending_more_than_20_characteres_description()
    {
        var studentCenterBaseCreateDto = new StudentCenterBaseCreateDto { Description = EMsg.MoreThan20Charaters };

        var studentCenterBaseDto = new StudentCenterBaseDto { Description = EMsg.MoreThan20Charaters };

        var studentCenterBase = new StudentCenterBase { Id = 1, Description = EMsg.MoreThan20Charaters };

        _mapper.Setup(x => x.Map<StudentCenterBase>(studentCenterBaseCreateDto)).Returns(studentCenterBase);

        _repository.Setup(x => x.Post(studentCenterBase)).ReturnsAsync(studentCenterBase);

        _mapper.Setup(x => x.Map<StudentCenterBaseDto>(studentCenterBase)).Returns(studentCenterBaseDto);

        var exception = await Assert.ThrowsAsync<Exception>(() => _service.Post(studentCenterBaseCreateDto));

        Assert.Equal("Description can be at most 20 characters", exception.Message);
    }

    [Fact]
    public async Task Create_sending_less_than_5_characters_page()
    {
        var studentCenterBaseCreateDto = new StudentCenterBaseCreateDto 
        { 
            Description = EMsg.ValidCharacters,
            Page = EMsg.LessThan5Characters
        };

        var studentCenterBaseDto = new StudentCenterBaseDto 
        {
            Description = EMsg.ValidCharacters,
            Page = EMsg.LessThan5Characters
        };

        var studentCenterBase = new StudentCenterBase 
        { 
            Id = 1,
            Description = EMsg.ValidCharacters,
            Page = EMsg.LessThan5Characters
        };

        _mapper.Setup(x => x.Map<StudentCenterBase>(studentCenterBaseCreateDto)).Returns(studentCenterBase);

        _repository.Setup(x => x.Post(studentCenterBase)).ReturnsAsync(studentCenterBase);

        _mapper.Setup(x => x.Map<StudentCenterBaseDto>(studentCenterBase)).Returns(studentCenterBaseDto);

        var exception = await Assert.ThrowsAsync<Exception>(() => _service.Post(studentCenterBaseCreateDto));

        Assert.Equal("Page must be at least 5 characters", exception.Message);
    }

    [Fact]
    public async Task Create_sending_less_than_5_characters_description()
    {
        var studentCenterBaseCreateDto = new StudentCenterBaseCreateDto { Description = EMsg.LessThan5Characters };

        var studentCenterBaseDto = new StudentCenterBaseDto { Description = EMsg.LessThan5Characters };

        var studentCenterBase = new StudentCenterBase { Id = 1, Description = EMsg.LessThan5Characters };

        _mapper.Setup(x => x.Map<StudentCenterBase>(studentCenterBaseCreateDto)).Returns(studentCenterBase);

        _repository.Setup(x => x.Post(studentCenterBase)).ReturnsAsync(studentCenterBase);

        _mapper.Setup(x => x.Map<StudentCenterBaseDto>(studentCenterBase)).Returns(studentCenterBaseDto);

        var exception = await Assert.ThrowsAsync<Exception>(() => _service.Post(studentCenterBaseCreateDto));

        Assert.Equal("Description must be at least 5 characters", exception.Message);
    }

    [Fact]
    public async Task Create_sending_empty_description()
    {
        var studentCenterBaseCreateDto = new StudentCenterBaseCreateDto { Description = "" };

        var studentCenterBaseDto = new StudentCenterBaseDto { Description = "" };

        var studentCenterBase = new StudentCenterBase { Id = 1, Description = "" };

        _mapper.Setup(x => x.Map<StudentCenterBase>(studentCenterBaseCreateDto)).Returns(studentCenterBase);

        _repository.Setup(x => x.Post(studentCenterBase)).ReturnsAsync(studentCenterBase);

        _mapper.Setup(x => x.Map<StudentCenterBaseDto>(studentCenterBase)).Returns(studentCenterBaseDto);

        var exception = await Assert.ThrowsAsync<Exception>(() => _service.Post(studentCenterBaseCreateDto));

        Assert.Equal("Description cannot be empty", exception.Message);
    }

    [Fact]
    public async Task Create_Valid()
    {
        var studentCenterBaseCreateDto = new StudentCenterBaseCreateDto 
        { 
            Description = EMsg.ValidCharacters,
            Page = EMsg.ValidCharacters
        };

        var studentCenterBaseDto = new StudentCenterBaseDto 
        {
            Description = EMsg.ValidCharacters,
            Page = EMsg.ValidCharacters
        };

        var studentCenterBase = new StudentCenterBase 
        { 
            Id = 1,
            Description = EMsg.ValidCharacters,
            Page = EMsg.ValidCharacters
        };

        _mapper.Setup(x => x.Map<StudentCenterBase>(studentCenterBaseCreateDto)).Returns(studentCenterBase);

        _repository.Setup(x => x.Post(studentCenterBase)).ReturnsAsync(studentCenterBase);

        _mapper.Setup(x => x.Map<StudentCenterBaseDto>(studentCenterBase)).Returns(studentCenterBaseDto);

        var result = await _service.Post(studentCenterBaseCreateDto);

        Assert.NotNull(result);

        Assert.Equal(EMsg.ValidCharacters, result.Description);
    }

    [Fact]
    public async Task GetAll()
    {
        var studentCenterBase = new List<StudentCenterBase>
        {
            new StudentCenterBase {Id = 1, Description = EMsg.ValidCharacters, Active = true},
            new StudentCenterBase {Id = 2, Description = EMsg.ValidCharacters, Active = true}
        };

        var studentCenterBaseDto = new List<StudentCenterBaseDto>
        {
            new StudentCenterBaseDto { Id = 1, Description = EMsg.ValidCharacters},
            new StudentCenterBaseDto { Id = 2, Description = EMsg.ValidCharacters},
        };

        _repository.Setup(x => x.GetAll()).ReturnsAsync(studentCenterBase);

        _mapper.Setup(x => x.Map<ICollection<StudentCenterBaseDto>>(studentCenterBase)).Returns(studentCenterBaseDto);

        var result = await _service.GetAll();

        Assert.NotNull(result);
        Assert.Equal(2, result.Count());
    }

    [Fact]
    public async Task GetByIdValid()
    {
        var studentCenterBase = new StudentCenterBase { Id = 1 };

        var studentCenterBaseDto = new StudentCenterBaseDto { Id = 1 };

        _repository.Setup(x => x.GetById(1)).ReturnsAsync(studentCenterBase);

        _mapper.Setup(x => x.Map<StudentCenterBaseDto>(studentCenterBase)).Returns(studentCenterBaseDto);

        var result = await _service.GetById(1);

        Assert.NotNull(result);

        Assert.True(studentCenterBase.Id > 0);
    }

    [Fact]
    public async Task GetById_sending_zeroed_id()
    {
        var studentCenterBase = new StudentCenterBase { Id = 0 };

        var studentCenterBaseDto = new StudentCenterBaseDto { Id = 0 };

        _repository.Setup(x => x.GetById(1)).ReturnsAsync(studentCenterBase);

        _mapper.Setup(x => x.Map<StudentCenterBaseDto>(studentCenterBase)).Returns(studentCenterBaseDto);

        var exception = await Assert.ThrowsAsync<Exception>(() => _service.GetById(0));

        Assert.Equal(EMsg.ZeroedId, exception.Message);
    }

    [Fact]
    public async Task Update_sending_more_than_20_characteres_page()
    {
        var statusCenterBaseUpdateDto = new StudentCenterBaseUpdateDto 
        { 
            Id = 1, 
            Description = EMsg.ValidCharacters,
            Page = EMsg.MoreThan20Charaters
        };

        var studentCenterBaseDto = new StudentCenterBaseDto 
        {
            Description = EMsg.ValidCharacters,
            Page = EMsg.MoreThan20Charaters
        };

        var studentCenterBase = new StudentCenterBase 
        { 
            Id = 1,
            Description = EMsg.ValidCharacters,
            Page = EMsg.MoreThan20Charaters
        };

        _mapper.Setup(x => x.Map<StudentCenterBase>(statusCenterBaseUpdateDto)).Returns(studentCenterBase);

        _repository.Setup(x => x.Put(studentCenterBase)).ReturnsAsync(studentCenterBase);

        _mapper.Setup(x => x.Map<StudentCenterBaseDto>(studentCenterBase)).Returns(studentCenterBaseDto);

        var exception = await Assert.ThrowsAsync<Exception>(() => _service.Put(statusCenterBaseUpdateDto));

        Assert.Equal("Page can be at most 20 characters", exception.Message);
    }

    [Fact]
    public async Task Update_sending_more_than_20_characteres_description()
    {
        var statusCenterBaseUpdateDto = new StudentCenterBaseUpdateDto { Id = 1, Description = EMsg.MoreThan20Charaters };

        var studentCenterBaseDto = new StudentCenterBaseDto { Description = EMsg.MoreThan20Charaters };

        var studentCenterBase = new StudentCenterBase { Id = 1, Description = EMsg.MoreThan20Charaters };

        _mapper.Setup(x => x.Map<StudentCenterBase>(statusCenterBaseUpdateDto)).Returns(studentCenterBase);

        _repository.Setup(x => x.Put(studentCenterBase)).ReturnsAsync(studentCenterBase);

        _mapper.Setup(x => x.Map<StudentCenterBaseDto>(studentCenterBase)).Returns(studentCenterBaseDto);

        var exception = await Assert.ThrowsAsync<Exception>(() => _service.Put(statusCenterBaseUpdateDto));

        Assert.Equal("Description can be at most 20 characters", exception.Message);
    }

    [Fact]
    public async Task Update_sending_less_than_5_characters_page()
    {
        var statusCenterBaseUpdateDto = new StudentCenterBaseUpdateDto 
        { 
            Id = 1, 
            Description = EMsg.ValidCharacters,
            Page = EMsg.LessThan5Characters
        };

        var studentCenterBaseDto = new StudentCenterBaseDto 
        {
            Description = EMsg.ValidCharacters,
            Page = EMsg.LessThan5Characters
        };

        var studentCenterBase = new StudentCenterBase 
        { 
            Id = 1,
            Description = EMsg.ValidCharacters,
            Page = EMsg.LessThan5Characters
        };

        _mapper.Setup(x => x.Map<StudentCenterBase>(statusCenterBaseUpdateDto)).Returns(studentCenterBase);

        _repository.Setup(x => x.Put(studentCenterBase)).ReturnsAsync(studentCenterBase);

        _mapper.Setup(x => x.Map<StudentCenterBaseDto>(studentCenterBase)).Returns(studentCenterBaseDto);

        var exception = await Assert.ThrowsAsync<Exception>(() => _service.Put(statusCenterBaseUpdateDto));

        Assert.Equal("Page must be at least 5 characters", exception.Message);
    }

    [Fact]
    public async Task Update_sending_less_than_5_characters_description()
    {
        var statusCenterBaseUpdateDto = new StudentCenterBaseUpdateDto { Id = 1, Description = EMsg.LessThan5Characters };

        var studentCenterBaseDto = new StudentCenterBaseDto { Description = EMsg.LessThan5Characters };

        var studentCenterBase = new StudentCenterBase { Id = 1, Description = EMsg.LessThan5Characters };

        _mapper.Setup(x => x.Map<StudentCenterBase>(statusCenterBaseUpdateDto)).Returns(studentCenterBase);

        _repository.Setup(x => x.Put(studentCenterBase)).ReturnsAsync(studentCenterBase);

        _mapper.Setup(x => x.Map<StudentCenterBaseDto>(studentCenterBase)).Returns(studentCenterBaseDto);

        var exception = await Assert.ThrowsAsync<Exception>(() => _service.Put(statusCenterBaseUpdateDto));

        Assert.Equal("Description must be at least 5 characters", exception.Message);
    }

    [Fact]
    public async Task Update_sending_empty_description()
    {
        var statusCenterBaseUpdateDto = new StudentCenterBaseUpdateDto { Id = 1, Description = "" };

        var studentCenterBaseDto = new StudentCenterBaseDto { Description = "" };

        var studentCenterBase = new StudentCenterBase { Id = 1, Description = "" };

        _mapper.Setup(x => x.Map<StudentCenterBase>(statusCenterBaseUpdateDto)).Returns(studentCenterBase);

        _repository.Setup(x => x.Put(studentCenterBase)).ReturnsAsync(studentCenterBase);

        _mapper.Setup(x => x.Map<StudentCenterBaseDto>(studentCenterBase)).Returns(studentCenterBaseDto);

        var exception = await Assert.ThrowsAsync<Exception>(() => _service.Put(statusCenterBaseUpdateDto));

        Assert.Equal("Description cannot be empty", exception.Message);
    }

    [Fact]
    public async Task update_Valid()
    {
        var statusCenterBaseUpdateDto = new StudentCenterBaseUpdateDto 
        { 
            Id = 1, 
            Description = EMsg.ValidCharacters,
            Page = EMsg.ValidCharacters
        };

        var studentCenterBaseDto = new StudentCenterBaseDto 
        {
            Description = EMsg.ValidCharacters,
            Page = EMsg.ValidCharacters
        };

        var studentCenterBase = new StudentCenterBase
        { 
            Id = 1,
            Description = EMsg.ValidCharacters,
            Page = EMsg.ValidCharacters
        };

        _mapper.Setup(x => x.Map<StudentCenterBase>(statusCenterBaseUpdateDto)).Returns(studentCenterBase);

        _repository.Setup(x => x.Put(studentCenterBase)).ReturnsAsync(studentCenterBase);

        _mapper.Setup(x => x.Map<StudentCenterBaseDto>(studentCenterBase)).Returns(studentCenterBaseDto);

        var result = await _service.Put(statusCenterBaseUpdateDto);

        Assert.NotNull(result);

        Assert.Equal(EMsg.ValidCharacters, result.Description);
    }

    [Fact]
    public async Task Delete_Valid()
    {
        var studentCenterBase = new StudentCenterBase { Id = 1, Description = EMsg.ValidCharacters };

        var studentCenterBaseDto = new StudentCenterBaseDto { Id = 1, Description = EMsg.ValidCharacters };

        _repository.Setup(x => x.GetById(1)).ReturnsAsync(studentCenterBase);

        _mapper.Setup(x => x.Map<StudentCenterBaseDto>(studentCenterBase)).Returns(studentCenterBaseDto);

        _repository.Setup(x => x.Delete(It.IsAny<StudentCenterBase>())).ReturnsAsync(true);

        var result = await _service.Delete(studentCenterBaseDto);

        Assert.True(result);
    }

    [Fact]
    public async Task Delete_sending_zeroed_id()
    {
        var studentCenterBase = new StudentCenterBase { Id = 0 };

        var studentCenterBaseDto = new StudentCenterBaseDto { Id = 0 };

        _repository.Setup(x => x.GetById(0)).ReturnsAsync(studentCenterBase);

        _mapper.Setup(x => x.Map<StudentCenterBaseDto>(studentCenterBase)).Returns(studentCenterBaseDto);

        _repository.Setup(x => x.Delete(It.IsAny<StudentCenterBase>())).ReturnsAsync(true);

        var exception = await Assert.ThrowsAsync<Exception>(() => _service.Delete(studentCenterBaseDto));

        Assert.Equal(EMsg.ZeroedId, exception.Message);
    }
}
