using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CursorManager : AbstractSingleton<CursorManager>
{
    [SerializeField] Texture2D pointerTexture;
    [SerializeField] Texture2D handTexture;
    [SerializeField] Texture2D grabTexture;

    [SerializeField] CursorMode cursorMode = CursorMode.Auto;
    [SerializeField] Vector2 hotSpot = Vector2.zero;

    public enum State { Pointer, Hand, Grab };
    State state = State.Pointer;

    public void Start()
    {
        SetPointer();
    }

    public void SetGrab()
    {
        state = State.Grab;
        Cursor.SetCursor(grabTexture, hotSpot, cursorMode);
    }

    public void SetHand()
    {
        state = State.Hand;
        Cursor.SetCursor(handTexture, hotSpot, cursorMode);
    }

    public void SetPointer()
    {
       state = State.Pointer;
       Cursor.SetCursor(pointerTexture, Vector2.zero, cursorMode);
    }

    public State GetState()
    {
        return state;
    }

    public bool IsHand()
    {
        return state == State.Hand;
    }
    public bool IsGrab()
    {
        return state == State.Grab;
    }
    public bool IsPointer()
    {
        return state == State.Pointer;
    }
}
