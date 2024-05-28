using System;
using StatlerWaldorfCorp.LocationReporter.Events;

namespace StatlerWaldorfCorp.LocationReporter.Models
{
	public class CommandEventConverter: ICommandEventConverter
	{
        public MemberLocationRecordedEvent CommandToEvent(LocationReport locationReport)
        {
            MemberLocationRecordedEvent locationRecordedEvent = new MemberLocationRecordedEvent
            {
                Latitude = locationReport.Latitude,
                Longitude = locationReport.Longitude,
                Origin = locationReport.Origin,
                MemberId = locationReport.MemberId,
                ReportId = locationReport.MemberId,
                RecordedTime = DateTime.Now.ToUniversalTime().Ticks
            };
            return locationRecordedEvent;
        }
    }
}

