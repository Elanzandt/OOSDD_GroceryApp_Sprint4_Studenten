
using Grocery.Core.Interfaces.Repositories;
using Grocery.Core.Interfaces.Services;
using Grocery.Core.Models;

namespace Grocery.Core.Services
{
    public class BoughtProductsService : IBoughtProductsService
    {
        private readonly IGroceryListItemsRepository _groceryListItemsRepository;
        private readonly IClientRepository _clientRepository;
        private readonly IProductRepository _productRepository;
        private readonly IGroceryListRepository _groceryListRepository;
        public BoughtProductsService(IGroceryListItemsRepository groceryListItemsRepository, IGroceryListRepository groceryListRepository, IClientRepository clientRepository, IProductRepository productRepository)
        {
            _groceryListItemsRepository=groceryListItemsRepository;
            _groceryListRepository=groceryListRepository;
            _clientRepository=clientRepository;
            _productRepository=productRepository;
        }
        public List<BoughtProducts> Get(int? productId)
        {
            if (productId == null || productId == 0) { return new List<BoughtProducts>(); }

            List<BoughtProducts> result = new List<BoughtProducts>();

            var allGrocyLists = _groceryListRepository.GetAll();

            foreach (var groceryList in allGrocyLists)
            {
                List<GroceryListItem> items = _groceryListItemsRepository.GetAllOnGroceryListId(groceryList.Id);

                if(items.Any(i => i.ProductId == productId)) {
                    Client client = _clientRepository.Get(groceryList.ClientId);
                    Product product = _productRepository.Get(productId.Value);

                    if(product != null && client != null)
                    {
                        result.Add(new BoughtProducts(client, groceryList, product));
                    }
                }
            }

            return result;
        }
    }
}
