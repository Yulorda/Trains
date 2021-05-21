using UniRx;
using UnityEngine;

    public class TrainStation : ISelectable, IConnectionPoint
    {
        ReactiveProperty<Vector3> IConnectionPoint.Position => Position;

        public ReactiveProperty<bool> IsSelected { get; private set; } = new ReactiveProperty<bool>();
        public ReactiveProperty<Vector3> Position = new ReactiveProperty<Vector3>();
        public ReactiveCommand OnPointerClick = new ReactiveCommand();
        public ReactiveCommand OnPointerDrag = new ReactiveCommand();
        public ReactiveCommand OnMouseUp = new ReactiveCommand();
        public ReactiveCommand OnMouseDown = new ReactiveCommand();

        public TrainStation()
        {
        }

        public void Select()
        {
            IsSelected.SetValueAndForceNotify(true);
        }

        public void UnSelect()
        {
            IsSelected.SetValueAndForceNotify(false);
        }
    }
