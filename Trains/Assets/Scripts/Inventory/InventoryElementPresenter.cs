using UnityEngine.UI;

namespace Inventory
{
    public class InventoryElementPresenter : PresenterBehaviour<IInventoryElement>
    {
        public Image image;
        public Button button;

        protected override void OnInjectModel()
        {
            base.OnInjectModel();
            image.sprite = model.ItemDescription.icon;
            button.onClick.AddListener(model.StartAction);
        }

        protected override void OnRemoveModel()
        {
            base.OnRemoveModel();
            button.onClick.RemoveListener(model.StartAction);
        }
    }
}