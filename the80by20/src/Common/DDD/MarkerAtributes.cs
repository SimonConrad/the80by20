namespace Common.DDD // TODO Comment each ddd tactical element with practilal explanantion - when to use, what gives us
// todo fix typos
// todo review and fix content
// todo use for slides in presentations
{
    /// <summary>
    /// https://www.informit.com/articles/article.aspx?p=2020371 Vaughn Vernon implementing ddd: aggregates
    /// 
    /// // pdf in ddd folder implementing ddd-aggregates-vaughn vernon - with description of invariants , constatiancy boundary, transactional consistancy
    /// Rule: Model True Invariants in Consistency Boundaries
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
    /// </summary
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
   
    /// TODO asynchronous read model
    /// <summary>
    /// Read model
    /// 
    /// read model and write model should be same only in CRUDs (like Administration module), ex. category anemic entity
    /// 
    /// in cases when wrtie logic is more complex, like in Core module we incorportae deep model for writes:
    /// ... consisting of ddd buildig blocks -  application logic started by command, domain logic operating on different levels: capabilities, operations, politics, decision makers
    /// read model on the other site is separate model concentrating on containing  data related to decisions, and for fast reads
    /// read model consists of information needed for read in app (green card in event storming), this read information is then used to make decision while doing command
    /// 
    /// read model can be implemented by object consisting of plain fields but also we may use value objects (from domain layer) ...
    /// ... and reader which fetch or save data from peristance - db / cache and is optimized for doing fast read - ex plan sql using ado, dapper, separate database optimized for reads, denormalized tables etc.
    /// read model data is projection of persisted data by writes (projections is combination of written data optimized for reads) ...
    /// ... so when persisting agregate data also read model data should be persisted - maybe done synchronously, for example: in same placa as aggregates is saved ...
    /// or asynchronously using messaging - read model listener can be subscribed to aggregate events and then proper data from domain event can be persistsed 
    /// 
    /// information in read model is different then information in ddd entities and aggregate beacouse these 2 sets of data have diffrenet purposes ...
    /// ... entities / aggregates - logic which especially guards consistnent state transisitons by invariants...
    /// ... read model - data for reads, and services - readers, for purpose of analytics and decison - support commands
    /// 
    /// to achieve loosely copupled read model and entity but somehow relate read model to entityt / aggregate, read model can have id pointing to aggragate ...
    /// ... but don't have navigation proprties from aggregate to read model and opposite way - it may enter problem for enetring aggragate into inconcistant state
    ///
    ///
    /// ... read model should be mapped to data struture taht can be serialized into json when transmitted over http, so this mapping should be included in applicatin,
    /// ... this data can be reader-dto
    ///
    ///
    ///
    /// Aggregates designing rules
    /// 4 rules about designing aggregates by Eric Evans:
    /// - Model true invariants in consistency boundaries (granica spójności - agreagt zawiera tylko te instancje informacji, ktore wszystkie razem tworza spojny stan)
    /// - Design small aggregate
    /// - Reference other aggregates by identity
    /// - Use Eventual Consistency outside the boundary
    /// 
    /// </summary>
    public class ReadModelDddAttribute : Attribute
    {

    }

    // domeny, subdomeny, b ctxty, archetypy modeli biznesowy, struktury wielkiej skali
    // najpierw subdomeny, potem destylacja b ctxtu
    // granice bounded contextu wyznaczemy heurystyka pojedynczego zrodla prawdy, czyli jest jedno zrodlo prawdy jesli:
    // zadajac pytanie biznesowe otrzymuje odpowiedz, na ktora wplyw maja komendy + zdarzenia wystepujace w ramach jednego kontkstu
    // np złamanie zasady pojedynczego zrodla prawdy:
    // jesli zadajac pytanie o dostepnosc musze wiedziec czy jest cos na magazynie (1 b.ctxt)
    // + dodtakowo złozyć to z inromacja czy cos jest w rezerwacji (2 ctxt)
    // powinienem miec jeden bctxt dostepnosc, część wspólna to id - wzorzec snowflake i to, że produkt jest czym innym w bctxt dostepnosc a czym innym w bctxt bestseller
    // z kolei kesz tych informacji readmodel moze byc zlozony z kilku bctxtow, ale to kesz - wtorny wzgledem komand ktore wplywaja na informacje czyli odpowiedz na pytanie biznesowe
    // te komendy i zdarzenia powinny byc razem w jednym bctxt
    //
    // inne złamanie pojedynczego źrdóła prawy żeby  na pytanie czy pacjent jes żywy - musimy przejśc sie po kilku oddziałącj i zajc do kostnicy
    // antywzorzec feature envy - jeden bctxt zadzrosci drugiemy danych, zachowań
    // ksiazki veinberga
}