﻿using System;

namespace Witnessing.Data.Model
{

    public class BaseModel
    {
        public long Id { get; set; }
        //public long? Source { get; set; }
    }

    public class Disposition : BaseModel
    {
        public override string ToString()
        {
            return $"{nameof(Hour)}: {Hour}, {nameof(Member)}: {Member}, {nameof(Date)}: {Date}";
        }

        public Hour Hour { get; set; }
        public WitnessingMember Member { get; set; }
        public DateTime Date { get; set; }
    }
    
    public class Hour : BaseModel
    {
        public DayOfWeek DayOfWeek { get; set; }
        public TimeSpan TimeOfDay { get; set; }
        public TimeSpan Duration { get; set; }

        public override string ToString()
        {
            return $"{nameof(DayOfWeek)}: {DayOfWeek}, {nameof(TimeOfDay)}: {TimeOfDay}, {nameof(Duration)}: {Duration}";
        }
    }

    public class Location : BaseModel
    {
        public override string ToString()
        {
            return $"{nameof(Name)}: {Name}";
        }

        public string Name { get; set; }
    }


    
    public class WitnessingMember : BaseModel
    {
        public override string ToString()
        {
            return $"{nameof(Name)}: {Name}, {nameof(LastName)}: {LastName}, {nameof(Phone)}: {Phone}, {nameof(Email)}: {Email}, {nameof(MinistryPrivilege)}: {MinistryPrivilege}";
        }

        public string Name { get; set; }
        public string LastName { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public string MinistryPrivilege { get; set; }
    }

    public class Schedule : BaseModel
    {

        public DateTime ScheduleDate { get; set; }
        public WitnessingMember Member { get; set; }
        public Location Location { get; set; }
        public Hour Hour { get; set; }
    }
}