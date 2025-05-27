using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialApp.Controller
{
    internal class GroceryListController
    {
        private readonly List<GroceryItem> _groceryItems;

        public GroceryListController()
        {
            _groceryItems = new List<GroceryItem>();
        }

        public IEnumerable<GroceryItem> GetAll()
        {
            return _groceryItems;
        }

        public GroceryItem? GetById(int id)
        {
            return _groceryItems.FirstOrDefault(item => item.Id == id);
        }

        public void Add(GroceryItem item)
        {
            if (item == null) throw new ArgumentNullException(nameof(item));
            item.Id = _groceryItems.Count > 0 ? _groceryItems.Max(i => i.Id) + 1 : 1;
            _groceryItems.Add(item);
        }

        public bool Update(GroceryItem item)
        {
            var existing = GetById(item.Id);
            if (existing == null) return false;
            existing.Name = item.Name;
            existing.Quantity = item.Quantity;
            return true;
        }

        public bool Delete(int id)
        {
            var item = GetById(id);
            if (item == null) return false;
            _groceryItems.Remove(item);
            return true;
        }
    }
    public class GroceryItem
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public int Quantity { get; set; }
    }
}