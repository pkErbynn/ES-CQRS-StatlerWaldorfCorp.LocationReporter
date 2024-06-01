# Event Sourcing-CQRS Location Reporter

The Location Reporter microservice files the command to report the new location of a team member.

It acts as the _Command Submitter_, used by various applications (mobile, web, embedded, etc.) to report the new location of a team member.

The Location Reporter Microservice is part of the **StatlerWaldorfCorp Microservices** suite, designed for Event Sourcing and CQRS.  

This service handles a single command: submitting a location report. It converts commands into events by transforming the command's payload (a request to record a team member's location) into an event that reflects the action has taken place (e.g., _member location recorded_).

The API for this service is straightforward:

| Resource | Method | Description |
|----------|--------|-------------|
| /api/members/{memberId}/locationreports | POST   | Submits a new location report to the service |
