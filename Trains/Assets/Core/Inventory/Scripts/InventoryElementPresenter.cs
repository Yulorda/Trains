using UnityEngine.UI;

namespace Inventory
{
    public class InventoryElementPresenter : PresenterBehaviour<InventoryElement>
    {
        public Image image;
        public Button button;

        protected override void OnInjectModel()
        {
            base.OnInjectModel();
            image.sprite = model.ItemDescription.icon;
            button.onClick.AddListener(model.Click);
        }

        protected override void OnRemoveModel()
        {
            base.OnRemoveModel();
            button.onClick.RemoveListener(model.Click);
        }
    }
}