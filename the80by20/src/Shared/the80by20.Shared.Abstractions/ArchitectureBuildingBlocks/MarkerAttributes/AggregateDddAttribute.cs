namespace the80by20.Shared.Abstractions.ArchitectureBuildingBlocks.MarkerAttributes;

/// <summary>
/// aggregate  - model true invariants in consistency boundaries according to real business rules. it is entity it has identity
///
/// aggregate aggregates set of entities + value-objects which we want to be consistent immediately,
/// and this consistency is based upon rules which this aggregate have inside
/// 
/// Aggregates designing rules
/// 4 rules about designing aggregates by Eric Evans:
/// - Model true invariants in consistency boundaries ...
/// ... (consistency boundary granica spójności - agregat ogranicza (boundary) tylko te dane, ktore wszystkie razem tworza spojny stan (consistency),
/// ... ten stan jest zmiany w spósb transkacyjny (spójność transakcyjna - natychmiastowa i acid, nie mylić z eventuall consitancy))
/// - Design small aggregate - na podstawie ES Design Level - grupujemy wg powyższego rozkaz-inwariant-zdazenie - one zamodeluja agreagt
/// - Reference other aggregates by identity
/// - Use Eventual Consistency outside the boundary
/// 
/// https://www.informit.com/articles/article.aspx?p=2020371 Vaughn Vernon implementing ddd: aggregates
/// // pdf in ddd folder implementing ddd-aggregates-vaughn vernon, part  - Rule: Model True Invariants in Consistency Boundaries
/// with description of invariants, constatiancy boundary, transactional consistancy
///
/// Clustering Entities (5) and Value Objects (6) into an Aggregate
/// with a carefully crafted consistency boundary may at first seem like quick work,
/// but among all DDD tactical guidance, this pattern is one of the least well understood.
///
/// 
/// agregat oznacza granicę spójności, technicznie transakcyjnie, ale chodzi o spójnośc biznesową
/// malutkie chudziutkie agregaty, nie ma w nich niepotrzebnych informacji
/// agregaty sa od siebie nizealezne, agregat nie moze wolac innego agregatu,
/// agregat nie moze byc parametrem innego agregatu, agregat nie zawiera innego agregatu - nie sa ze soba zrośnięte, agergaty to samotne wyspy, ewentulana referencja po idiku
/// agregat to niezalezny komponent, jesli musi sie komunikowac z innym agergatem to uzywajac value objectow
///
///
/// Aggregate - defines buisness transaction boundary, it means that:
///     - it encapsulates cohesive (only together they constitues some invariant) set of buisness information instances (information values) that are always in consistant state
///     - state - it is coheseive set of information instances that are persisted
///     - state - cohesive set of informations instances (values) that constitutes buissnes object state 
///     - the state consistancy is achived by aggratae building block by:
///         - aggregate public api so that the state is not changed directly
///         - encapulsated state in object (private fileds)
///         - state change happen in transactional write to database (ACID)
///         - aggregate instance is persisted (its ids and state, sometimes hsitory of its' events (events sourcing)) 
///
///     - aggregate should be small and include only buisness informations that are used by so called invariants:
///         - invariant is necessary condition that must be fulfilled for the state change to happen (in event storming buisness-wise important state chnage that happened is called domain event)
///         - if modelling system using event storming design level, state change is represented by events, command for these event are commands, ...
///         - ... invariant can be identified between command and event, invariant is condition that is neccessary to be fullfiled (at once, not only eventually) so that the event can happen
/// 
///     - the process of modelling agregate (for the puprpose of implementation) is called - discovering aggregate boundaries
///         - based on es design level - we put togther tuples of command-invariant-event
///         - which such tuples to put togther into one aggregate? These in which invariants  are using same information, or these with commands taht touches information that tuple with invariant use
///         - such small group of tuples of command-(invaraint)-event can be traeted as model of aggragtae (api (methods), information (private fields), events (represents state change, sometimes list of events), id)
///
///     - aggragate is discovered via invariants (yellow card in ES design level), invariant guard that state change is done in consistant way - state after this change is in consitnat way immediatley.
///         - watch out for problem of including informations (for example related entities, or not needed header fields) that are not included in invariants, avoid lazy loading of such
///         , best if aggregate is one table and small, best: id + serialized value objects
///         - such header data can be implemented as aggergate-data - its pf is fk to aggeragte, it is somtimes Readmodel
///         - keep data and operations on this data (behaviors, methods) togther in one object - rule of good cohesions - applies really good to aggragtes
///         - avoid procedural way of changing state - service changing orm entities - setters in entity are used by service - it is anti cohesive
///
/// 
///     - others (to be cleaned up)
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
///
///
/// - do not use lazy loading with aggregates - best option one table but if related data use eager loading
/// - why? because all aggregate data should be fetched from db at once to avoid potential inconsistency of data problems
/// </summary>
public class AggregateDddAttribute : Attribute
{ }