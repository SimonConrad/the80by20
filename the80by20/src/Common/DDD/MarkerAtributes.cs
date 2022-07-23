namespace Common.DDD // TODO Comment each ddd tactical element with practilal explanantion - when to use, what gives us
// todo fix typos
// todo use for slides in presentations
{
    // Aggregate, kind of entity (has identity and each instance is unique) that:
    // INFO defines transaction  boundary - which is persisted in transactional (ACID) way in database, best approach one transaction one aggregate
    // INFO if more aggregates state changes to be persisted in one business transaction then use saga pattern which makes mange such buinsess transaction in kind of acid way - uses compensation if needed
    // INFO aggragate is discovered via invang on this riants (yellow card in ES design level), invariant guard that state change is done in consistant way - state after this change is in consitnat way immediatley.
    // INFO State - cohesive pieces of information that constitutes buissnes object state 
    // INFO watch out for problem of including informations (for example related entities, or not needed header fields) that are not included in invariants, avoid lazy loading of such, best if aggregate is one table and small, best id + serialized value objects
    // INFO keep data and operations on this data (behaviors, methods) togther in one object - rile of good cohesions - applies really hard to aggragtes - avoid service changing orm entities setter way of coding - it is anti cohesive
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