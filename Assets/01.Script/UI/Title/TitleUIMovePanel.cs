using UIManage;
using UnityEngine;

public class TitleUIMovePanel : UIMovePanel
{
    public override void Open()
    {
        if (_isActive) return;
        base.Open();
        MovePanel(_activePos);
    }

    public override void Close()
    {
        MovePanel(_defaultPos);
    }
}
