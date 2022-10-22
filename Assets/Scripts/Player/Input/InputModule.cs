using UnityEngine;

public abstract class InputModule : IInputService
{
    protected const string _verticalAxis = "Vertical";
    protected const string _horizontalAxis = "Horizontal";

    public abstract Vector2 Axis { get; }

    protected static Vector2 SimpleInputAxis() =>
        new Vector2(SimpleInput.GetAxis(_horizontalAxis), SimpleInput.GetAxis(_verticalAxis));
}
