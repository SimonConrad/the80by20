namespace the80by20.Shared.Abstractions.ArchitectureBuildingBlocks.MarkerAttributes;

/// <summary>
/// https://www.youtube.com/watch?v=UFXgk3_j_2Q&list=PLUSb1w6ri8jrBc1NPoB6qqFlKlB-TDMWW&index=23
/// W systemie istnieją obiekty, które tożsamość potrzebują - np karta biblioteczna, ale też istnieją obiekty, które tej tożsamości nie potrzebują
/// te obiekty nie opsiują CZYM są, tylko JAKIE są,
/// np obiekt reprezentujący zoobowiązanie finansowe wobec biblioteki będzie zawierał wartość kary i wartość początkową kary
/// sztandarowy vo: money - składa się z waluty i ilość środków
/// porównywanie vo będzie sprowadzało do porówynywania wartości a nie idntyfikatorów, które są tu zbędne
/// koncpet whole value pattern - wartości składowane razem w jakims obiekcie pozwlałay na np. poprawnę ich walidacje, powinny tworzyć formę pojęciowej całości
/// i mocno korespondować z modelem domenym, który jest odwierciedlony w kodzie
///
/// inny przykładem vo moga byc współrzedne - bo np jeśli mam tylko szerokość geo. bez długości geo. to nie potrafię narysować takiego obiektu na mapie
/// dopiero obydwa parametry połaczone razem w obiekcie parametry geograficzne pozwlają reprezentować dany kojncpet domenowy
/// i dodatkowo 2 obiekty z 2 takimi samymy wspołrzednymi będą wskazyać ten sam punkt na mapie - identyczność przez wartości (nie tożsamość)
///
/// immutablity - niezmienność, chcialibyśmy by w wyniku wywoływania zmian na nich metodami tworzone były nowe obiekty
/// sens niezmiennośći -  żeby nie było takiego problemu: jeżeli ta sama instancje obiektu wartośći byłaby współdzilona w innych obiektach,
/// np kilka encji posiada w środku tą samą instancje vo
/// to zmiana w jednym vo powoduje automatycznie modyfikacje  winnych obiektach jeśli ta wartość byłaby współdzilona
///
/// czeklista za pomocą której możemy sprawdzić czy to o czym rozmawiamy możemy zaimplementować jako value object
/// - opisuje rzecz w domenie
/// - immutablity (niezmienność), uwolnienie od skutkó ubocznych
/// - modelowanie za pomoća whole value pattern
/// - wymienność (brak tożsamości i porównywanie za pomocą wartości)
///
/// - dobrze się testuje
/// - utworzenie metodą wytórczą
/// - persytujemy razem z obiektem z tożsamością encją lub/i agregatem
/// </summary>
public class ValueObjectDddAttribute : Attribute
{ }