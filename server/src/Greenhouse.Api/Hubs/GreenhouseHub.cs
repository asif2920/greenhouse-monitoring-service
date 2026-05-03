namespace Greenhouse.Api.Hubs;

using Microsoft.AspNetCore.SignalR;

public class GreenhouseHub : Hub
{
    public Task Heartbeat() => Task.CompletedTask;
}
