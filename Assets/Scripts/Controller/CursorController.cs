using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CursorController : MonoBehaviour
{
    int mongroundLayer = (1 << (int)Layer.Monster) | (1 << (int)Layer.Ground);
    CursorType cursorType = CursorType.None;
    Texture2D attackCursor;
    Texture2D handCursor;

    void Start()
    {
        attackCursor = MasterManager.Resource.Load<Texture2D>("Textures/Cursor/Attack");
        handCursor = MasterManager.Resource.Load<Texture2D>("Textures/Cursor/Hand");
    }

    void Update()
    {
        if (Input.GetMouseButton(0))
            return;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit rayHit;
        if (Physics.Raycast(ray, out rayHit, 100.0f, mongroundLayer))
        {
            if (rayHit.collider.gameObject.layer == ((int)Layer.Monster))
            {
                if (cursorType != CursorType.Attack)
                {
                    Cursor.SetCursor(attackCursor, new Vector2(attackCursor.width / 5, 0), CursorMode.Auto);
                    cursorType = CursorType.Attack;
                }
            }
            else
            {
                if (cursorType != CursorType.Hand)
                {
                    Cursor.SetCursor(handCursor, new Vector2(handCursor.width / 3, 0), CursorMode.Auto);
                    cursorType = CursorType.Hand;
                }
            }
        }
    }
}
