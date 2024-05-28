using System;
using Newtonsoft.Json;

namespace StatlerWaldorfCorp.LocationReporter.Events
{
	public class MemberLocationRecordedEvent
	{
        public Guid ReportId { get; set; }
        public string Origin { get; set; }
		public double Latitude { get; set; }
		public double Longitude { get; set; }
		public Guid MemberId { get; set; }
		public Guid TeamId { get; set; }
        public long RecordedTime { get; set; }


        public string toJson()
		{
			return JsonConvert.SerializeObject(this);
		}
	}
}

