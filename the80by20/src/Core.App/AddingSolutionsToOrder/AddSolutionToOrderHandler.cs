namespace Core.App.AddingSolutionsToOrder
{
    public class AddSolutionToOrderHandler
    {
        public void Add(AddSolutionToOrderCommand command)
        {
            // utwórz / lub pobierz zamówienie

            // dodaj rozwiązanie do zamówienia

            // zweryfikuj czy liczba rozwiązań w zamówieniu <=4, jeśli nie to przerwij use case (rzuć exception biznesowy)

            // zapisz do bazy
        }
    }
}