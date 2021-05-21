using System.Collections.Generic;
using System.Linq;
using UniRx;

namespace Inventory
{
    public class InventoryPresenter : PresenterBehaviour<InventoryModel>
    {
        public InventoryElemenetPresenterFactory itemPresenterFactory;
        private List<InventoryElementPresenter> inventoryElementPresenters = new List<InventoryElementPresenter>();

        protected override void OnInjectModel()
        {
            base.OnInjectModel();
            inventoryElementPresenters = itemPresenterFactory.Create(model.inventoryElements.ToList()).ToList();
            AddToDisposables(model.inventoryElements.ObserveAdd().Subscribe(x => CreateElement(x.Value)));
            AddToDisposables(model.inventoryElements.ObserveRemove().Subscribe(x => RemoveElement(x.Value)));
        }

        private void CreateElement(InventoryElement inventoryElement)
        {
            inventoryElementPresenters.Add(itemPresenterFactory.Create(inventoryElement));
        }

        private void RemoveElement(InventoryElement inventoryElement)
        {
            var presenter = inventoryElementPresenters.FirstOrDefault(x => x.model == inventoryElement);
            
            if (presenter != null)
            {
                DestroyImmediate(presenter);
                inventoryElementPresenters.Remove(presenter);
            }
        }
    }
}