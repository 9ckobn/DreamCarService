using UnityEngine;

public class StandaloneInputModule : InputModule
{
    public override Vector2 Axis
    {
        get
        {
            Vector2 Axis = SimpleInputAxis();

            if (Axis == Vector2.zero)
                Axis = UnityAxis();

            return Axis;
        }
    }

    private static Vector2 UnityAxis() =>
        new Vector2(UnityEngine.Input.GetAxis(_horizontalAxis), UnityEngine.Input.GetAxis(_verticalAxis));

}