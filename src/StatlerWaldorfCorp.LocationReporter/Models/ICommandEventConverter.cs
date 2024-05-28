using System;
namespace StatlerWaldorfCorp.LocationReporter.Models
{
	public interface ICommandEventConverter
	{
        MemberLocationRecordedEvent CommandToEvent(LocationReport locationReport);
    }
}

