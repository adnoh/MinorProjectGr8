using UnityEngine;
using System.Collections;

public class BuildMenuControl : MonoBehaviour {

    private bool working;
    private int operation;
    private Vector3 scalingSpeed = new Vector3(0.1f, 0.1f, 0f);
    private GameObject menu;

    void Start()
    {
        working = false;
        operation = 0;
    }

    void Update()
    {
        if (working)
        {
            switch (operation)
            {
                case 1:
                    menu.transform.localScale += scalingSpeed;
                    if (menu.transform.localScale == new Vector3(1, 1, 0))
                    {
                        working = false;
                        operation = 0;
                    }
                    break;
                case 2:
                    menu.transform.localScale -= scalingSpeed;
                    if (menu.transform.localScale == new Vector3(0, 0, 0))
                    {
                        working = false;
                        operation = 0;
                        menu.SetActive(false);
                        menu.transform.localScale = new Vector3(1, 1, 1);
                    }
                    break;
                default:
                    break;
            }
        }
    }

    public void CloseMenu(GameObject menu)
    {
        if (working == false)
        {
            this.menu = menu;
            working = true;
            operation = 2;
            menu.transform.localScale = new Vector3(1, 1, 0);
        }
    }

    public void OpenMenu(GameObject menu)
    {
        if (working == false)
        {
            this.menu = menu;
            working = true;
            operation = 1;
            menu.transform.localScale = new Vector3(0, 0, 0);
        }
    }
}
