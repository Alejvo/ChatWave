using Application.Groups.Create;
using Application.Groups.Delete;
using Application.Groups.Get.All;
using Application.Groups.Get.Id;
using Application.Groups.Get.Name;
using Application.Groups.Update;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers;

[Route("api/groups")]
[ApiController]
public class GroupsController : ControllerBase
{
    private readonly ISender _sender;

    public GroupsController(ISender sender)
    {
        _sender = sender;
    }

    [HttpPost]
    public async Task<IActionResult> CreateGroup([FromForm] CreateGroupCommand command)
    {
        var res = await _sender.Send(command);
        return res.IsSuccess ? Created() : BadRequest();
    }

    [HttpGet]
    public async Task<IActionResult> GetGroups()
    {
        var res = await _sender.Send(new GetGroupsQuery());
        return res.IsSuccess ? Ok(res): BadRequest(); 
    }

    [HttpGet]
    [Route("id/{id}")]
    public async Task<IActionResult> GetGroupById(string id)
    {
        var res = await _sender.Send(new GetGroupByIdQuery(id));
        return res.IsSuccess ? Ok(res): NotFound(); 
    }

    [HttpGet]
    [Route("name/{name}")]
    public async Task<IActionResult> GetGroupByName(string name)
    {
        var res = await _sender.Send(new GetGroupsByNameQuery(name));
        return res.IsSuccess ? Ok(res): NotFound();
    }

    [HttpPut]
    [Route("update")]
    public async Task<IActionResult> UpdateGroup([FromForm] UpdateGroupCommand command)
    {
        var res = await _sender.Send(command);
        return res.IsSuccess ? NoContent() : NotFound();
    }

    [HttpDelete]
    [Route("delete/{id}")]
    public async Task<IActionResult> DeleteGroup(string id)
    {
        var res = await _sender.Send(new DeleteGroupCommand(id));
        return res.IsSuccess ? NoContent() : NotFound();
    }
}
