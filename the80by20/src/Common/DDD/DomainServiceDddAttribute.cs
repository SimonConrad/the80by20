namespace Common.DDD;

/// <summary>
/// TODO use
/// Logic that dont seems to fit straight into application logic but it starting repeating in may places
/// cooridnates two aggragates
/// for example żeby wypłacić 2k z karty musisz miec wlascicolestwo premium
/// serwis domenowy sprawdza wlascicielstwo karty (agregat 1) jesli jestes wlascielem to zmienia stan kontat (agergat 2)
/// jest to bestanowa funkcja - np zwrocic zmieniony
/// https://www.youtube.com/watch?v=Fihj7uKQikA&list=PLUSb1w6ri8jrBc1NPoB6qqFlKlB-TDMWW&index=2
/// </summary>
public class DomainServiceDddAttribute : Attribute
{ }