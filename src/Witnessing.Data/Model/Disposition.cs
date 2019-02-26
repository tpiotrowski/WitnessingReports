using System;

namespace Witnessing.Data.Model
{

    public class BaseModel
    {
        public long Id { get; set; }
        //public long? Source { get; set; }
    }

    public class Disposition : BaseModel
    {
        public Hour Hour { get; set; }
        public WitnessingMember Member { get; set; }
        public DateTimeOffset Date { get; set; }
    }
    
    public class Hour : BaseModel
    {
        public DayOfWeek DayOfWeek { get; set; }
        public TimeSpan TimeOfDay { get; set; }
        public TimeSpan Duration { get; set; }
    }

    public class Location : BaseModel
    {
        public string Name { get; set; }
    }

    public class WitnessingMember : BaseModel
    {
        public string Name { get; set; }
        public string LastName { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public string MinistryPrivilege { get; set; }
    }

    public class Schedule : BaseModel
    {
        public DateTimeOffset ScheduleDate { get; set; }
        public WitnessingMember Member { get; set; }
        public Location Location { get; set; }
        public Hour Hour { get; set; }
    }
}