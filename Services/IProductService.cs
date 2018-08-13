using DomainModel;

namespace Services
{
    public interface IProductService
    {
        Product[] GetAllProductsPaged(int offset, int pageSize);
    }
}
