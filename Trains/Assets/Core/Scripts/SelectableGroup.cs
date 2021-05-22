using System.Collections.Generic;

public class SelectableGroup
{
    private List<ISelectable> selectables = new List<ISelectable>();

    public SelectableGroup()
    {
    }

    public void Add(ISelectable selectable)
    {
        selectables.Add(selectable);
    }

    public void Remove(ISelectable selectable)
    {
        selectables.Remove(selectable);
    }

    public void SelectGroup()
    {
        foreach (var selectable in selectables)
        {
            if (!selectable.IsSelected.Value)
            {
                selectable.IsSelected.SetValueAndForceNotify(true);
            }
        }
    }

    public void UnSelectAll()
    {
        foreach (var selectable in selectables)
        {
            selectable.IsSelected.SetValueAndForceNotify(false);
        }
    }

    public void AddSelectableItem(ISelectable selectable)
    {
        if (!selectables.Contains(selectable))
        {
            selectables.Add(selectable);
        }
    }

    public void RemoveSelectableItem(ISelectable selectable)
    {
        selectables.Remove(selectable);
    }

    public bool Contains(ISelectable selectable)
    {
        return selectables.Contains(selectable);
    }
}