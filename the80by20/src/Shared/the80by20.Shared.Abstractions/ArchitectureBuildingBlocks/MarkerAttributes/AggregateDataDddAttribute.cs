namespace the80by20.Shared.Abstractions.ArchitectureBuildingBlocks.MarkerAttributes;

/// <summary>
/// INFO Data linked with aggregate instance but not included in invariants check - so not included inside aggegate object,
/// used often for read use for decision about command
/// saved in one transaction with aggregate only when  aggragted created,
/// but when changing aggregate it does not to be included in aggrage retrival
/// when this data is chnaged also aggragte not need to be retrived from database
/// regerence aggragate with id, don do naviagation proporties to aggregate and opposite way
///
///
/// can be implemnted as anemic entity with crud operations done by crud repository, generic repo can be reused in this case
/// changes on its fields do not have consequences
/// when modelling its event is like DataChanged
/// </summary>
public class AggregateDataDddAttribute : Attribute
{ }

// od mariusza g. odp na w/w podejście:
// Takie rozwiązanie ma jak najbardziej sens gdy:
// - danych na potrzeby UI/prezentacji jest sporo,
// - agregat jest często zmiany, co wymusza jego częsty odczyt i zapis do/z bazy,
// - potrzebujesz odczytywać te dane i pokazywać je użytkownikom systemu bez potrzeby zmiany agregatu.

// Czasem oczywiście można też zrobić bez tego rozdzielenia.
// Pragmatycznie, jeśli tych danych odczytowych nie ma dużo, nie ma problemu z wydajnością,
// to można trochę tych danych dodać do agregatu. Nie trzeba wtedy rozdzielać tabel w systemie, etc. J
// a z tym wtedy nie mam problemu ;) Ale jak napisałem, to zależy od konkretnego przypadku