namespace the80by20.Shared.Abstractions.ArchitectureBuildingBlocks.MarkerAttributes;

/// <summary>
/// INFO
/// Hibryde of Saga + Process Manager
/// orchestrate process spanning over multiple modules / or one module in time
/// guards consistent state of application - it is business transaction
/// have mechanism of compensation - rollback appropriate commands if there was a fail
/// have state in which it is currently
/// have data
/// organise process steps
/// also called workflow
/// 
/// if can try to avoid distributed business transactions
/// 
/// example of distributed business transaction: process of flight reservation which consists of sending 3 commands to separate modules / services
/// : 1. reserve-plane 2. reserve-car-to-hotel 3. reserve-hotel
/// with the need of compensation if any fails (by "rollback")
///
/// saga can be alternative to cron job - for example requirment if 10 users signed-up to something
/// with cron job - every day at midnight check table and so on
/// instead use sage whchc not query db but react to event of signedup user, and do its logic if user more then 10
/// </summary>
public class SagaAttribute : Attribute
{ }