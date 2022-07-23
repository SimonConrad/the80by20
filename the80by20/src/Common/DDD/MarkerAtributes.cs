namespace Common.DDD // TODO Comment each element with practilal explanantion -when to use, what gives us
{
    public class AggregateDddAttribute : Attribute
    {}

    public class ValueObjectDddAttribute : Attribute
    {}

    // Has identity that makes it unique unlike vallue object wich is compared by value eneity is compared by identity, aggragate is also entity
    public class EntityDddAttribute : Attribute
    {}

    public class DomainExceptionDddAttribute : Attribute
    {}

    public class DomainEnumDddAttribute : Attribute
    {}

    public class AggregateRepositoryDddAttribute : Attribute
    {}

    // INFO Data linked with aggregate instance but not included in invariants check - so not included inside aggegate object,
    // used often for read use for decision about command
    // saved in one transaction with aggregate only when  aggragted created,
    // but when changing aggregate it does not to be included in aggrage retrival
    // when this data is chnaged also aggragte not need to be retrived from database
    public class AggregateDataDddAttribute : Attribute
    { }

    // TODO use
    // INFO Base Aggregate with collection of domain events, can be used for event sourcing, messaging,
    // for testing results of invoking command on aggregate (alternative verify getters)
    public class DomainEventDddAttribute : Attribute
    { }

    // TODO use
    // INFO Stable computing logic
    public class DomainServiceDddAttribute : Attribute
    { }

    // TODO use
    // INFO Dynamic logic, which adujust domain service; sepcific policy implmentation is created by factory, based on some changeable condition - time / apsetting / user profile, 
    // strategy design pattern
    public class PolicyDddAttribute : Attribute
    { }

    // TODO use
    // INFO factory - separate class for example for creating policy (construct interface implmentation), for vo or aggregate - factory method
    public class FactoryDddAttribute : Attribute
    { }
}