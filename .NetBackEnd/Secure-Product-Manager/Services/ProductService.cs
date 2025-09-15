using Microsoft.Extensions.Options;
using MongoDB.Driver;
using Secure_Product_Manager.Entity;

namespace Secure_Product_Manager.Services
{
    public class ProductService
    {
        private readonly IMongoCollection<Product> _productsCollection;
        public ProductService(
       IOptions<ProductsManagerDataBase> bookStoreDatabaseSettings)
        {
            var mongoClient = new MongoClient(
                bookStoreDatabaseSettings.Value.ConnectionString);

            var mongoDatabase = mongoClient.GetDatabase(
                bookStoreDatabaseSettings.Value.DatabaseName);

            _productsCollection = mongoDatabase.GetCollection<Product>("products");
        }

        public async Task<List<Product>> GetAsync() =>
            await _productsCollection.Find(_ => true).ToListAsync();

        public async Task<Product?> GetAsync(string id) =>
            await _productsCollection.Find(x => x.id == id).FirstOrDefaultAsync();

        public async Task CreateAsync(Product newBook) =>
            await _productsCollection.InsertOneAsync(newBook);

        public async Task UpdateAsync(string id, Product updatedBook) =>
        await _productsCollection.ReplaceOneAsync(x => x.id == id, updatedBook);

        public async Task RemoveAsync(string id) =>
            await _productsCollection.DeleteOneAsync(x => x.id == id);
    }
}
