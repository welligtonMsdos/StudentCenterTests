using AutoMapper;
using Moq;
using StudentCenterApi.src.Application.DTOs.Status;
using StudentCenterApi.src.Application.Services;
using StudentCenterApi.src.Domain.Interfaces;
using StudentCenterApi.src.Domain.Model;
using StudentCenterBaseTests.Enum;

namespace StudentCenterBaseTests.Tests;

public class StatusTests
{
    private readonly Mock<IStatusRepository> _repository;
    private readonly Mock<IMapper> _mapper;
    private readonly StatusService _service;

    public StatusTests()
    {
        _repository = new Mock<IStatusRepository>();
        _mapper = new Mock<IMapper>();
        _service = new StatusService(_repository.Object, _mapper.Object);
    }

    [Fact]
    public async Task Create_sending_more_than_20_characteres_description()
    {
        var statusCreateDto = new StatusCreateDto { Description = EMsg.MoreThan20Charaters };

        var statusDto = new StatusDto { Description = EMsg.MoreThan20Charaters };

        var status = new Status { Id = 1, Description = EMsg.MoreThan20Charaters };

        _mapper.Setup(x => x.Map<Status>(statusCreateDto)).Returns(status);

        _repository.Setup(x => x.Post(status)).ReturnsAsync(status);

        _mapper.Setup(x => x.Map<StatusDto>(status)).Returns(statusDto);

        var exception = await Assert.ThrowsAsync<Exception>(() => _service.Post(statusCreateDto));

        Assert.Equal("Description can be at most 20 characters", exception.Message);
    }

    [Fact]
    public async Task Create_sending_less_than_5_characters_description()
    {
        var statusCreateDto = new StatusCreateDto { Description = EMsg.LessThan5Characters };

        var statusDto = new StatusDto { Description = EMsg.LessThan5Characters };

        var status = new Status { Id = 1, Description = EMsg.LessThan5Characters };

        _mapper.Setup(x => x.Map<Status>(statusCreateDto)).Returns(status);

        _repository.Setup(x => x.Post(status)).ReturnsAsync(status);

        _mapper.Setup(x => x.Map<StatusDto>(status)).Returns(statusDto);

        var exception = await Assert.ThrowsAsync<Exception>(() => _service.Post(statusCreateDto));

        Assert.Equal("Description must be at least 5 characters", exception.Message);
    }

    [Fact]
    public async Task Create_sending_empty_description()
    {
        var statusCreateDto = new StatusCreateDto { Description = "" };

        var statusDto = new StatusDto { Description = "" };

        var status = new Status { Id = 1, Description = "" };

        _mapper.Setup(x => x.Map<Status>(statusCreateDto)).Returns(status);

        _repository.Setup(x => x.Post(status)).ReturnsAsync(status);

        _mapper.Setup(x => x.Map<StatusDto>(status)).Returns(statusDto);

        var exception = await Assert.ThrowsAsync<Exception>(() => _service.Post(statusCreateDto));

        Assert.Equal("Description cannot be empty", exception.Message);
    }

    [Fact]
    public async Task Create_Valid()
    {
        var statusCreateDto = new StatusCreateDto { Description = EMsg.ValidCharacters };

        var statusDto = new StatusDto { Description = EMsg.ValidCharacters };

        var status = new Status { Id = 1, Description = EMsg.ValidCharacters };

        _mapper.Setup(x => x.Map<Status>(statusCreateDto)).Returns(status);

        _repository.Setup(x => x.Post(status)).ReturnsAsync(status);

        _mapper.Setup(x => x.Map<StatusDto>(status)).Returns(statusDto);

        var result = await _service.Post(statusCreateDto);

        Assert.NotNull(result);

        Assert.Equal(EMsg.ValidCharacters, result.Description);
    }

    [Fact]
    public async Task GetAll()
    {
        var status = new List<Status>
        {
            new Status {Id = 1, Description = EMsg.ValidCharacters, Active = true},
            new Status {Id = 2, Description = EMsg.ValidCharacters, Active = true}
        };

        var statusDto = new List<StatusDto>
        {
            new StatusDto { Id = 1, Description = EMsg.ValidCharacters},
            new StatusDto { Id = 2, Description = EMsg.ValidCharacters},
        };

        _repository.Setup(x => x.GetAll()).ReturnsAsync(status);

        _mapper.Setup(x => x.Map<ICollection<StatusDto>>(status)).Returns(statusDto);

        var result = await _service.GetAll();

        Assert.NotNull(result);
        Assert.Equal(2, result.Count());
    }

    [Fact]
    public async Task GetByIdValid()
    {
        var status = new Status { Id = 1 };

        var statusDto = new StatusDto { Id = 1 };

        _repository.Setup(x => x.GetById(1)).ReturnsAsync(status);

        _mapper.Setup(x => x.Map<StatusDto>(status)).Returns(statusDto);

        var result = await _service.GetById(1);

        Assert.NotNull(result);

        Assert.True(status.Id > 0);
    }

    [Fact]
    public async Task GetById_sending_zeroed_id()
    {
        var status = new Status { Id = 0 };

        var statusDto = new StatusDto { Id = 0 };

        _repository.Setup(x => x.GetById(1)).ReturnsAsync(status);

        _mapper.Setup(x => x.Map<StatusDto>(status)).Returns(statusDto);

        var exception = await Assert.ThrowsAsync<Exception>(() => _service.GetById(0));

        Assert.Equal(EMsg.ZeroedId, exception.Message);
    }

    [Fact]
    public async Task Update_sending_more_than_20_characteres_description()
    {
        var statusUpdateDto = new StatusUpdateDto { Id = 1, Description = EMsg.MoreThan20Charaters };

        var statusDto = new StatusDto { Description = EMsg.MoreThan20Charaters };

        var status = new Status { Id = 1, Description = EMsg.MoreThan20Charaters };

        _mapper.Setup(x => x.Map<Status>(statusUpdateDto)).Returns(status);

        _repository.Setup(x => x.Put(status)).ReturnsAsync(status);

        _mapper.Setup(x => x.Map<StatusDto>(status)).Returns(statusDto);

        var exception = await Assert.ThrowsAsync<Exception>(() => _service.Put(statusUpdateDto));

        Assert.Equal("Description can be at most 20 characters", exception.Message);
    }

    [Fact]
    public async Task Update_sending_less_than_5_characters_description()
    {
        var statusUpdateDto = new StatusUpdateDto { Id = 1, Description = EMsg.LessThan5Characters };

        var statusDto = new StatusDto { Description = EMsg.LessThan5Characters };

        var status = new Status { Id = 1, Description = EMsg.LessThan5Characters };

        _mapper.Setup(x => x.Map<Status>(statusUpdateDto)).Returns(status);

        _repository.Setup(x => x.Put(status)).ReturnsAsync(status);

        _mapper.Setup(x => x.Map<StatusDto>(status)).Returns(statusDto);

        var exception = await Assert.ThrowsAsync<Exception>(() => _service.Put(statusUpdateDto));

        Assert.Equal("Description must be at least 5 characters", exception.Message);
    }

    [Fact]
    public async Task Update_sending_empty_description()
    {
        var statusUpdateDto = new StatusUpdateDto { Id = 1, Description = "" };

        var statusDto = new StatusDto { Description = "" };

        var status = new Status { Id = 1, Description = "" };

        _mapper.Setup(x => x.Map<Status>(statusUpdateDto)).Returns(status);

        _repository.Setup(x => x.Put(status)).ReturnsAsync(status);

        _mapper.Setup(x => x.Map<StatusDto>(status)).Returns(statusDto);

        var exception = await Assert.ThrowsAsync<Exception>(() => _service.Put(statusUpdateDto));

        Assert.Equal("Description cannot be empty", exception.Message);
    }

    [Fact]
    public async Task update_Valid()
    {
        var statusUpdateDto = new StatusUpdateDto { Id = 1, Description = EMsg.ValidCharacters };

        var statusDto = new StatusDto { Description = EMsg.ValidCharacters };

        var status = new Status { Id = 1, Description = EMsg.ValidCharacters };

        _mapper.Setup(x => x.Map<Status>(statusUpdateDto)).Returns(status);

        _repository.Setup(x => x.Put(status)).ReturnsAsync(status);

        _mapper.Setup(x => x.Map<StatusDto>(status)).Returns(statusDto);

        var result = await _service.Put(statusUpdateDto);

        Assert.NotNull(result);

        Assert.Equal(EMsg.ValidCharacters, result.Description);
    }

    [Fact]
    public async Task Delete_Valid()
    {
        var status = new Status { Id = 1, Description = EMsg.ValidCharacters };

        var statusDto = new StatusDto { Id = 1, Description = EMsg.ValidCharacters };

        _repository.Setup(x => x.GetById(1)).ReturnsAsync(status);

        _mapper.Setup(x => x.Map<StatusDto>(status)).Returns(statusDto);

        _repository.Setup(x => x.Delete(It.IsAny<Status>())).ReturnsAsync(true);

        var result = await _service.Delete(statusDto);

        Assert.True(result);
    }

    [Fact]
    public async Task Delete_sending_zeroed_id()
    {
        var status = new Status { Id = 0 };

        var statusDto = new StatusDto { Id = 0 };

        _repository.Setup(x => x.GetById(0)).ReturnsAsync(status);

        _mapper.Setup(x => x.Map<StatusDto>(status)).Returns(statusDto);

        _repository.Setup(x => x.Delete(It.IsAny<Status>())).ReturnsAsync(true);

        var exception = await Assert.ThrowsAsync<Exception>(() => _service.Delete(statusDto));

        Assert.Equal(EMsg.ZeroedId, exception.Message);
    }
}
