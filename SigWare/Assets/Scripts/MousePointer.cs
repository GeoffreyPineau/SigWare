using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GR19
{
    public class MousePointer : MonoBehaviour
    {

        public Texture2D chargingTexture;
        public Texture2D defaultTexture;
        public Camera cam;
        public GameObject plugs;
        public CursorMode curMode = CursorMode.Auto;
        public Vector2 hotSpot = Vector2.zero;

        void OnMouseEnter()
        {

            Ray ray = cam.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit))
            {
                if (hit.collider.gameObject == plugs)
                {
                    Cursor.SetCursor(chargingTexture, hotSpot, curMode);
                    //Debug.Log("chargingMouseCursor");
                }
            }
        }

        void OnMouseExit()
        {
            Cursor.SetCursor(defaultTexture, hotSpot, curMode);
            //Debug.Log("RunningMouseCursor");
        }
    }
}

