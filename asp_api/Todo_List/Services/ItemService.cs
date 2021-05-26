using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Todo_List.Models;
using Todo_List.Models.DB_Settings;

namespace Todo_List.Services
{
    public class ItemService
    {
        private readonly IMongoCollection<Item> _items;

        public ItemService(MyTodoListDatabaseSettings settings)
        {
            var client = new MongoClient(settings.ConnectionString);
            var database = client.GetDatabase(settings.DatabaseName);

            _items = database.GetCollection<Item>(settings.ItemsCollectionName);
        }

        public List<Item> Get(string userId)
        {
            return _items.Find(item => item.UserId == userId && item.IsDisplay == true).ToList();
        }

        public Item GetItem(string id, string userId)
        {
            return _items.Find<Item>(item => item.Id == id && item.UserId == userId && item.IsDisplay == true).FirstOrDefault();
        }

        public Item Create(Item item)
        {
            _items.InsertOne(item);
            return item;
        }

        public void Update(string id, Item itemIn) =>
            _items.ReplaceOne(item => item.Id == id, itemIn);

    }
}
