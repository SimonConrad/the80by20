namespace Common.DDD // TODO Comment each ddd tactical element with practilal explanantion - when to use, what gives us
// todo fix typos
// todo use for slides in presentations
{
    /// <summary>
    /// Aggregate - defines buisness transaction boundary, it means that:
    ///     - encapsulate cohesive set of buisness information values that are always in consistant state
    ///     - coheseive set of information instances (information values) that are persisted is state
    ///     - State - cohesive set of informations instances (values) that constitutes buissnes object state 
    ///     - the state consistancy is achived by:
    ///         - aggregate public api so that the state is not changed directly
    ///         - state change happen in transactional write to database (ACID)
    ///         - aggregate instance is persisted (its ids and state, sometimes hsitory of events (events sourcing)) 
    ///
    ///     - aggregate should be small and include only buisness informations that are used by so called invariants:
    ///         - invariant is necessary condition that must be fulfilled for the state change to happen (in event storming buisness-wise important state chnage that happened is called domain event)
    ///         - if modelling system using event storming design level, state change is represented by events, command for these event are commands, ...
    ///         - ... invariant can be identified between command and event, invariant is condition that is neccessary to be fullfiled (at once, not eventually) so that the event can happen
    /// 
    ///     - the process of modelling agregate (for the puprpose of implementation) is called - discovering aggregate boundaries
    ///         - based on es design level - we put togther tuples of command-invariant-event
    ///         - which such tuples to chose? These in with invariants tath are using same information, or theses with commands taht touches information that tuple with invariant use
    ///         - such small group of tuples of command-(invaraint)-event can be traeted as model of aggragtae (api (methods), information (private fields), events (represents state change, sometimes list of events), id)
    ///
    ///     - aggragate is discovered via invariants (yellow card in ES design level), invariant guard that state change is done in consistant way - state after this change is in consitnat way immediatley.
    ///         - watch out for problem of including informations (for example related entities, or not needed header fields) that are not included in invariants, avoid lazy loading of such
    ///         , best if aggregate is one table and small, best id + serialized value objects
    ///         - such header data as aggergate-data - its pf is fk to aggeragte
    ///         - keep data and operations on this data (behaviors, methods) togther in one object - rule of good cohesions - applies really good to aggragtes
    ///         - avoid procedural way of changing state - service changing orm entities setter way of coding - it is anti cohesive
    ///
    /// 
    ///     - others (to be lkeaned up)
    ///         - state consitutues of buimsees information that sohuld be chnaged together, so that after the change these information values (state) are consistent state (perisited in db)
    ///         - aggregate guards to not to transition system into inconsistnet stae
    ///         - aggregate should be statefull, and its state is persisted (it is not input model withou state)
    ///         - aggragete  is in some state, and can trnasition to other state (state is not enum, but all set of buisness infromation actual values aggregate poses)
    /// 
    ///         - typical path: create curent state of agregate fetch it from db (via its id) and creating via its factory method, based on incoming command state is chnage via api, underneath invaraints are checked, if ok write commit aggregate state to db 
    ///         - agg: kind of entity (has identity and each instance is unique) that:
    /// 
    ///         - agg is persisted in transactional (ACID) way in database, best approach one transaction one aggregate
    ///         - if more aggregates states changes need to be persisted in one business transaction then use saga pattern
    ///         - saga makes manging such buinsess transaction in kind of acid way - uses compensation if needed
    /// </summary>
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