using System.Collections.Generic;
using UnityEngine;

public class MenuUIController : MonoBehaviour
{
    public List<GameObject> Menu;

    private int _currentUIIndex = 0;

    private void OnEnable()
    {
        SelectUI(0);
    }

    public void SelectUI(int index)
    {
        DisableAllMenu();
        Menu[index].SetActive(true);
        _currentUIIndex = index;
    }

    private void DisableAllMenu()
    {
        foreach (var ui in Menu)
        {
            ui.SetActive(false);
        }
    }
}
