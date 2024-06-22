# Event Sourcing-CQRS Location Reporter

The Location Reporter microservice files the command to report the new location of a team member.

It acts as the _Command Submitter_, used by various applications (mobile, web, embedded, etc.) to report the new location of a team member.

The Location Reporter Microservice is part of the **StatlerWaldorfCorp Microservices** suite, designed for Event Sourcing and CQRS.  

This service handles a single command: submitting a location report. It converts commands into events by transforming the command's payload (a request to record a team member's location) into an event that reflects the action has taken place (e.g., _member location recorded_).

The API for this service is straightforward:

| Resource | Method | Description |
|----------|--------|-------------|
| /api/members/{memberId}/locationreports | POST   | Submits a new location report to the service |


# Why Use the Options Pattern in ASP.NET Core

In ASP.NET Core, the `Options` pattern is used to manage and configure application settings in a structured and type-safe manner. Here’s why you would use the `Options` pattern:

## Type-Safety
The `Options` pattern allows you to map configuration settings to strongly-typed objects. This ensures that you get compile-time checking and IntelliSense support in your IDE.

## Centralized Configuration
Using options, you can centralize the configuration logic, making it easier to manage and change settings without scattering configuration logic throughout the codebase.

## Separation of Concerns
The `Options` pattern helps to separate configuration from application logic. This makes the code cleaner and easier to maintain.

## Dependency Injection
Configuration options can be injected into services or controllers, promoting better design practices and enabling easier unit testing.

## Example

### Configuration in `appsettings.json`

```json
{
  "amqp": {
    "Host": "localhost",
    "Port": 5672
  },
  "teamservice": {
    "BaseUrl": "http://teams.example.com",
    "ApiKey": "your-api-key"
  }
}
```

Define Option Classes

```
public class AMQPOptions
{
    public string Host { get; set; }
    public int Port { get; set; }
}

public class TeamServiceOptions
{
    public string BaseUrl { get; set; }
    public string ApiKey { get; set; }
}
```

Configure Options in Program.cs

```
builder.Services.Configure<AMQPOptions>(builder.Configuration.GetSection("amqp"));
builder.Services.Configure<TeamServiceOptions>(builder.Configuration.GetSection("teamservice"));
```

Inject Options into Services or Controllers

```
public class SomeService
{
    private readonly AMQPOptions _amqpOptions;

    public SomeService(IOptions<AMQPOptions> amqpOptions)
    {
        _amqpOptions = amqpOptions.Value;
    }

    // Use _amqpOptions.Host and _amqpOptions.Port as needed
}
```

This structured approach ensures that configuration settings are managed efficiently, reducing errors and enhancing maintainability.


### Accessing API from Swagger
- Run `dotnet run --server.urls=http://0.0.0.0:9090`
- Access endpoint via Swagger at `https://localhost:7190/swagger/index.html`

#### Using curl
```cmd
$ curl -X 'POST' \
  'https://localhost:7190/api/members/3fa85f64-5717-4562-b3fc-2c963f66afa8/locationreports' \
  -H 'accept: */*' \
  -H 'Content-Type: application/json' \
  -d '{
  "reportId": "3fa85f64-5717-4562-b3fc-2c963f66afa6",
  "origin": "string",
  "latitude": 0,
  "longitude": 0,
  "memberId": "3fa85f64-5717-4562-b3fc-2c963f66afa6"
}'
```
