using UnityEngine;

public interface IInputHandler
{
    bool IsMouseButtonHeldDown(int button);
    bool IsMouseButtonDown(int button);
    bool IsMouseButtonUp(int button);
    bool IsKeyboardButtonHeldDown(KeyCode key);
    bool IsKeyboardButtonDown(KeyCode key);
    bool IsKeyboardButtonUp(KeyCode key);
    Vector3 GetMousePosition();
}
public class InputHandler : IInputHandler
{
    public bool IsMouseButtonHeldDown(int button) => Input.GetMouseButton(button);
    public bool IsMouseButtonDown(int button) => Input.GetMouseButtonDown(button);
    public bool IsMouseButtonUp(int button) => Input.GetMouseButtonUp(button);
    public bool IsKeyboardButtonHeldDown(KeyCode key) => Input.GetKey(key);
    public bool IsKeyboardButtonDown(KeyCode key) => Input.GetKeyDown(key);
    public bool IsKeyboardButtonUp(KeyCode key) => Input.GetKeyUp(key);
    public Vector3 GetMousePosition() => Input.mousePosition;
}
