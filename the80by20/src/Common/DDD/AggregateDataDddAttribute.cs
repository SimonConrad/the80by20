namespace Common.DDD;

/// <summary>
/// INFO Data linked with aggregate instance but not included in invariants check - so not included inside aggegate object,
/// used often for read use for decision about command
/// saved in one transaction with aggregate only when  aggragted created,
/// but when changing aggregate it does not to be included in aggrage retrival
/// when this data is chnaged also aggragte not need to be retrived from database
/// regerence aggragate with id, don do naviagation proporties to aggregate and opposite way
/// </summary>
public class AggregateDataDddAttribute : Attribute
{ }