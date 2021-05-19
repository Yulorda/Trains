using Inventory;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Invnetory
{
    namespace Example
    {
        public class InventoryExample : MonoBehaviour
        {
            [SerializeField]
            List<GameObject> inventoryElements = new List<GameObject>();

            [SerializeField]
            InventoryPresenter inventroyPresenter;

            private void Start()
            {
                var elements = new List<IInventoryElement>();
                inventoryElements.ForEach(x => elements.Add(x.GetComponent<IInventoryElement>()));

                var model = new InventoryModel(elements);

                inventroyPresenter.InjectModel(model);
                inventroyPresenter.gameObject.SetActive(true);
            }
        }
    }
}