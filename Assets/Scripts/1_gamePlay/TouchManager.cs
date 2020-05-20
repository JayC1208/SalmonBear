using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TouchManager : MonoBehaviour
{
    public GameObject arms, requirement, back, fever;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0)) Clicked();       // error: can slide the finger to click other object
    }

    void Clicked()
    {
        Vector3 mouse3D = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector2 mousePos = new Vector2(mouse3D.x, mouse3D.y);

        RaycastHit2D hit = Physics2D.Raycast(mousePos, Vector2.zero);

        if (hit.collider != null)
        {
            if (hit.collider.name == "LeftArm")
            {
                arms.GetComponent<PlayerMovement>().LeftClick();
            }

            else if (hit.collider.name == "RightArm")
            {
                arms.GetComponent<PlayerMovement>().RightClick();
            }

            else if (hit.collider.name == "Wasabi")
            {
                requirement.GetComponent<SideManager>().WasabiClick();
            }

            else if (hit.collider.name == "Tea")
            {
                requirement.GetComponent<SideManager>().TeaClick();
            }

            else if (hit.collider.name == "SoySauce")
            {
                requirement.GetComponent<SideManager>().SoyClick();
            }

            else if(hit.collider.name == "LA_fever")
            {
                fever.GetComponent<FeverManager>().LeftClick_F();
            }

            else if (hit.collider.name == "RA_fever")
            {
                fever.GetComponent<FeverManager>().RightClick_F();
            }

            else if (hit.collider.name == "Salmon")
            {
                back.GetComponent<GameManager>().FeverStart();
            }
        }
    }
}
