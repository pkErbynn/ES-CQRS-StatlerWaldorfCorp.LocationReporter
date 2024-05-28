using System;
using Microsoft.AspNetCore.Mvc;
using StatlerWaldorfCorp.LocationReporter.Events;
using StatlerWaldorfCorp.LocationReporter.Models;
using StatlerWaldorfCorp.LocationReporter.Services;

namespace StatlerWaldorfCorp.LocationReporter.Controllers
{
	[Route("/api/members/{memberId}/locationreports")]
	public class LocationReportsController: Controller
	{
		private ICommandEventConverter converter;
		private IEventEmitter eventEmitter;
		private ITeamServiceClient teamServiceClient;

		public LocationReportsController(
			ICommandEventConverter converter,
            IEventEmitter eventEmitter,
            ITeamServiceClient teamServiceClient)
		{
			this.converter = converter;
			this.eventEmitter = eventEmitter;
			this.teamServiceClient = teamServiceClient;
		}

		public ActionResult PostLocationReport(Guid memberId, [FromBody]LocationReport locationReport)
		{
			MemberLocationRecordedEvent locationRecordedEvent = converter.CommandToEvent(locationReport);
			teamServiceClient.GetTeamForMember(locationReport.MemberId);
			eventEmitter.EmitLocationRecordedEvent(locationRecordedEvent);

			return this.Created($"api/members/{memberId}/locationreports/{locationReport.ReportId}", locationReport);
		}
	}
}

