using AutoMapper;
using Moq;
using StudentCenterApi.src.Application.DTOs.RequestType;
using StudentCenterApi.src.Application.Services;
using StudentCenterApi.src.Domain.Interfaces;
using StudentCenterApi.src.Domain.Model;
using StudentCenterBaseTests.Enum;

namespace StudentCenterBaseTests.Tests;

public class RequestTypeTests
{
    private readonly Mock<IRequestTypeRepository> _repository;
    private readonly Mock<IMapper> _mapper;
    private readonly RequestTypeService _service;

    public RequestTypeTests()
    {
        _repository = new Mock<IRequestTypeRepository>();
        _mapper = new Mock<IMapper>();
        _service = new RequestTypeService(_repository.Object, _mapper.Object);
    }

    [Fact]
    public async Task Create_sending_more_than_100_characteres_description()
    {
        var requestTypeCreateDto = new RequestTypeCreateDto { Description = EMsg.MoreThan100Charaters };

        var requestTypeDto = new RequestTypeDto { Description = EMsg.MoreThan100Charaters };

        var requestType = new RequestType { Id = 1, Description = EMsg.MoreThan100Charaters };

        _mapper.Setup(x => x.Map<RequestType>(requestTypeCreateDto)).Returns(requestType);

        _repository.Setup(x => x.Post(requestType)).ReturnsAsync(requestType);

        _mapper.Setup(x => x.Map<RequestTypeDto>(requestType)).Returns(requestTypeDto);

        var exception = await Assert.ThrowsAsync<Exception>(() => _service.Post(requestTypeCreateDto));

        Assert.Equal("Description can be at most 100 characters", exception.Message);
    }

    [Fact]
    public async Task Create_sending_less_than_5_characters_description()
    {
        var requestTypeCreateDto = new RequestTypeCreateDto { Description = EMsg.LessThan5Characters };

        var requestTypeDto = new RequestTypeDto { Description = EMsg.LessThan5Characters };

        var requestType = new RequestType { Id = 1, Description = EMsg.LessThan5Characters };

        _mapper.Setup(x => x.Map<RequestType>(requestTypeCreateDto)).Returns(requestType);

        _repository.Setup(x => x.Post(requestType)).ReturnsAsync(requestType);

        _mapper.Setup(x => x.Map<RequestTypeDto>(requestType)).Returns(requestTypeDto);

        var exception = await Assert.ThrowsAsync<Exception>(() => _service.Post(requestTypeCreateDto));

        Assert.Equal("Description must be at least 5 characters", exception.Message);
    }

    [Fact]
    public async Task Create_sending_empty_description()
    {
        var requestTypeCreateDto = new RequestTypeCreateDto { Description = "" };

        var requestTypeDto = new RequestTypeDto { Description = "" };

        var requestType = new RequestType { Id = 1, Description = "" };

        _mapper.Setup(x => x.Map<RequestType>(requestTypeCreateDto)).Returns(requestType);

        _repository.Setup(x => x.Post(requestType)).ReturnsAsync(requestType);

        _mapper.Setup(x => x.Map<RequestTypeDto>(requestType)).Returns(requestTypeDto);

        var exception = await Assert.ThrowsAsync<Exception>(() => _service.Post(requestTypeCreateDto));

        Assert.Equal("Description cannot be empty", exception.Message);
    }

    [Fact]
    public async Task Create_Valid()
    {
        var requestTypeCreateDto = new RequestTypeCreateDto { Description = EMsg.ValidCharacters };

        var requestTypeDto = new RequestTypeDto { Description = EMsg.ValidCharacters };

        var requestType = new RequestType { Id = 1, Description = EMsg.ValidCharacters };

        _mapper.Setup(x => x.Map<RequestType>(requestTypeCreateDto)).Returns(requestType);

        _repository.Setup(x => x.Post(requestType)).ReturnsAsync(requestType);

        _mapper.Setup(x => x.Map<RequestTypeDto>(requestType)).Returns(requestTypeDto);


        var result = await _service.Post(requestTypeCreateDto);

        Assert.NotNull(result);

        Assert.Equal(EMsg.ValidCharacters, result.Description);
    }

    [Fact]
    public async Task GetAll()
    {
        var requestType = new List<RequestType>
        {
            new RequestType {Id = 1, Description = EMsg.ValidCharacters, Active = true},
            new RequestType {Id = 2, Description = EMsg.ValidCharacters, Active = true}
        };

        var requestTypeDto = new List<RequestTypeDto>
        {
            new RequestTypeDto { Id = 1, Description = EMsg.ValidCharacters},
            new RequestTypeDto { Id = 2, Description = EMsg.ValidCharacters},
        };

        _repository.Setup(x => x.GetAll()).ReturnsAsync(requestType);

        _mapper.Setup(x => x.Map<ICollection<RequestTypeDto>>(requestType)).Returns(requestTypeDto);

        var result = await _service.GetAll();

        Assert.NotNull(result);
        Assert.Equal(2, result.Count());
    }

    [Fact]
    public async Task GetByIdValid()
    {
        var requestType = new RequestType { Id = 1 };

        var requestTypeDto = new RequestTypeDto { Id = 1 };

        _repository.Setup(x => x.GetById(1)).ReturnsAsync(requestType);

        _mapper.Setup(x => x.Map<RequestTypeDto>(requestType)).Returns(requestTypeDto);

        var result = await _service.GetById(1);

        Assert.NotNull(result);

        Assert.True(requestTypeDto.Id > 0);
    }

    [Fact]
    public async Task GetById_sending_zeroed_id()
    {
        var requestType = new RequestType { Id = 0 };

        var requestTypeDto = new RequestTypeDto { Id = 0 };

        _repository.Setup(x => x.GetById(1)).ReturnsAsync(requestType);

        _mapper.Setup(x => x.Map<RequestTypeDto>(requestType)).Returns(requestTypeDto);

        var exception = await Assert.ThrowsAsync<Exception>(() => _service.GetById(0));

        Assert.Equal(EMsg.ZeroedId, exception.Message);
    }

    [Fact]
    public async Task Update_sending_more_than_100_characteres_description()
    {
        var requestTypeUpdateDto = new RequestTypeUpdateDto { Id = 1, Description = EMsg.MoreThan100Charaters };

        var requestTypeDto = new RequestTypeDto { Description = EMsg.MoreThan100Charaters };

        var requestType = new RequestType { Id = 1, Description = EMsg.MoreThan100Charaters };

        _mapper.Setup(x => x.Map<RequestType>(requestTypeUpdateDto)).Returns(requestType);

        _repository.Setup(x => x.Put(requestType)).ReturnsAsync(requestType);

        _mapper.Setup(x => x.Map<RequestTypeDto>(requestType)).Returns(requestTypeDto);

        var exception = await Assert.ThrowsAsync<Exception>(() => _service.Put(requestTypeUpdateDto));

        Assert.Equal("Description can be at most 100 characters", exception.Message);
    }

    [Fact]
    public async Task Update_sending_less_than_5_characters_description()
    {
        var requestTypeUpdateDto = new RequestTypeUpdateDto { Id = 1, Description = EMsg.LessThan5Characters };

        var requestTypeDto = new RequestTypeDto { Description = EMsg.LessThan5Characters };

        var requestType = new RequestType { Id = 1, Description = EMsg.LessThan5Characters };

        _mapper.Setup(x => x.Map<RequestType>(requestTypeUpdateDto)).Returns(requestType);

        _repository.Setup(x => x.Put(requestType)).ReturnsAsync(requestType);

        _mapper.Setup(x => x.Map<RequestTypeDto>(requestType)).Returns(requestTypeDto);

        var exception = await Assert.ThrowsAsync<Exception>(() => _service.Put(requestTypeUpdateDto));

        Assert.Equal("Description must be at least 5 characters", exception.Message);
    }

    [Fact]
    public async Task Update_sending_empty_description()
    {
        var requestTypeUpdateDto = new RequestTypeUpdateDto { Id = 1, Description = "" };

        var requestTypeDto = new RequestTypeDto { Description = "" };

        var requestType = new RequestType { Id = 1, Description = "" };

        _mapper.Setup(x => x.Map<RequestType>(requestTypeUpdateDto)).Returns(requestType);

        _repository.Setup(x => x.Put(requestType)).ReturnsAsync(requestType);

        _mapper.Setup(x => x.Map<RequestTypeDto>(requestType)).Returns(requestTypeDto);

        var exception = await Assert.ThrowsAsync<Exception>(() => _service.Put(requestTypeUpdateDto));

        Assert.Equal("Description cannot be empty", exception.Message);
    }

    [Fact]
    public async Task update_Valid()
    {
        var requestTypeUpdateDto = new RequestTypeUpdateDto { Id = 1, Description = EMsg.ValidCharacters };

        var requestTypeDto = new RequestTypeDto { Description = EMsg.ValidCharacters };

        var requestType = new RequestType { Id = 1, Description = EMsg.ValidCharacters };

        _mapper.Setup(x => x.Map<RequestType>(requestTypeUpdateDto)).Returns(requestType);

        _repository.Setup(x => x.Put(requestType)).ReturnsAsync(requestType);

        _mapper.Setup(x => x.Map<RequestTypeDto>(requestType)).Returns(requestTypeDto);

        var result = await _service.Put(requestTypeUpdateDto);

        Assert.NotNull(result);

        Assert.Equal(EMsg.ValidCharacters, result.Description);
    }

    [Fact]
    public async Task Delete_Valid()
    {
        var requestType = new RequestType { Id = 1, Description = EMsg.ValidCharacters };

        var requestTypeDto = new RequestTypeDto { Id = 1, Description = EMsg.ValidCharacters };

        _repository.Setup(x => x.GetById(1)).ReturnsAsync(requestType);

        _mapper.Setup(x => x.Map<RequestTypeDto>(requestType)).Returns(requestTypeDto);

        _repository.Setup(x => x.Delete(It.IsAny<RequestType>())).ReturnsAsync(true);

        var result = await _service.Delete(requestTypeDto);

        Assert.True(result);
    }

    [Fact]
    public async Task Delete_sending_zeroed_id()
    {
        var requestType = new RequestType { Id = 0 };

        var requestTypeDto = new RequestTypeDto { Id = 0 };

        _repository.Setup(x => x.GetById(0)).ReturnsAsync(requestType);

        _mapper.Setup(x => x.Map<RequestTypeDto>(requestType)).Returns(requestTypeDto);

        _repository.Setup(x => x.Delete(It.IsAny<RequestType>())).ReturnsAsync(true);

        var exception = await Assert.ThrowsAsync<Exception>(() => _service.Delete(requestTypeDto));

        Assert.Equal(EMsg.ZeroedId, exception.Message);
    }
}
