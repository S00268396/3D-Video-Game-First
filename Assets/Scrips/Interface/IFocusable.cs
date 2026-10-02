using UnityEngine;

public interface IFocusable
{
    public void OnFocus(GameObject Interactor);
    public void UnFocus(GameObject Interactor);
}
