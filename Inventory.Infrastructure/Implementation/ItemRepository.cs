using Inventory.Core.Entity;
using Inventory.Core.Interfaces;
using Inventory.Web.Data;
using Microsoft.EntityFrameworkCore;


namespace Inventory.Infrastructure.Implementation
{
    public class ItemRepository : IITemRepository
    {
        private readonly ApplicationDbContext _dbContext;

        public ItemRepository(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Item> AddItemAsync(Item item)
        {
            await _dbContext.AddAsync(item);
            _dbContext.SaveChanges();
            return item;
        }

        public async Task<Item> DeleteItemByIdAsync(int id)
        {
            var itemToDelete = await GetItemByIdAsync(id);
            _dbContext.Remove(itemToDelete);
            await _dbContext.SaveChangesAsync();
            return itemToDelete;
        }

        public async Task<Item> GetItemByIdAsync(int id)
        {
           var item =await _dbContext.Items.FirstOrDefaultAsync(x => x.Id == id);
            return item;

        }

        public async Task<IReadOnlyList<Item>> GetItemListAsync()
        {
            return await _dbContext.Items.ToListAsync();
        }

        public async Task<Item> UpdateItemAsync(Item item)
        {
             _dbContext.Update(item);
             await _dbContext.SaveChangesAsync();
            return item;
        }
    }
}
