namespace the80by20.Shared.Abstractions.ArchitectureBuildingBlocks.MarkerAttributes;

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
/// </summary>
public class ReadModelDddAttribute : Attribute
{
}