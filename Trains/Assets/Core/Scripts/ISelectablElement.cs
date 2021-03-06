using System.Drawing;
using UniRx;

public interface ISelectable
{
    ReactiveProperty<bool> IsSelected { get; }
}