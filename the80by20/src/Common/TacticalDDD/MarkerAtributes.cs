namespace Common.TacticalDDD
{
    public class AggregateDddAttribute : Attribute
    {
    }

    public class ValueObjectDddAttribute : Attribute
    {

    }

    public class DomainExceptionDddAttribute : Attribute
    {
    }

    public class DomainEnumDDDAttribute : Attribute
    {

    }

    public class AggregateRepositoryAttribute : Attribute
    {

    }

    // Data linked with aggregate instance but not included in invariants check - so not straight in aggegate object, used often for reads, to decide about command
    // saved in one transaction with aggregate only when  aggragted created, but when changing aggregate it does not to be included in aggrage retrival
    // same when this data is chnaged aggragte not  need to be retrived to chnage this data
    public class AggregateDataAttribute : Attribute
    {

    }

    // TODO Domain Event with Base Aggregate with collection of domain events
    // TODO Domain Service - stable computing logic
    // TODO Policy - dynamic logic adujsting domain service based on some changeable condition - apsetting, user profile etd, created by factory
    // TODO factory - for policy, for vo a/ aggregate - factory method
}