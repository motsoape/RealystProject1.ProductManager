using ProductManager.Repositories.Entities;
using ProductManager.Repositories.Interfaces;

namespace ProductManager.Repositories
{
    public class ProductRepository : IDataRepository<Product>
    {
        readonly ProductContext _productContext;
        public ProductRepository(ProductContext context)
        {
            _productContext = context;
        }

        public void Add(Product entity)
        {
            _productContext.Products.Add(entity);
            _productContext.SaveChanges();
        }

        public void Delete(Product entity)
        {
            _productContext.Products.Remove(entity);
            _productContext.SaveChanges();
        }

        public Product Get(int id)
        {
            return _productContext.Products.FirstOrDefault(e => e.ProductID == id);
        }

        public IEnumerable<Product> GetAll()
        {
            return _productContext.Products.ToList();
        }

        public void Update(Product entity)
        {
            var _entityToUpdate = Get(entity.ProductID);
            if (_entityToUpdate != null)
                throw new Exception("The product cannot be updated");

            _entityToUpdate.Price = entity.Price;
            _entityToUpdate.Name = entity.Name;
            _entityToUpdate.ReleaseDate = entity.ReleaseDate;

            _productContext.SaveChanges();
        }
    }
}