namespace IMS.UseCases.Products.Interfaces
{
    public interface IDeleteProductUseCase
    {
        Task Execute(int id);
    }
}