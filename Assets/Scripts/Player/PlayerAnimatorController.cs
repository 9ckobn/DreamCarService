using UnityEngine;

public class PlayerAnimatorController : MonoBehaviour
{
    private static string MoveHash = "Walk";
    private static string MoveHndshash = "WalkHands";
    private static string isWithHandsParametr = "isWithHands";

    private bool withHands;
    public bool isWithHands
    {
        get { return withHands; }
        set
        {
                animator.SetBool(isWithHandsParametr, value);
                withHands = value;
        }
    }


    public CharacterController characterController;
    public Animator animator;
    public AnimatorState animatorState
    {
        private get { return animatorState; }
        set
        {
            ChangeState(value);
            _currentState = value;
        }
    }
    private AnimatorState _currentState;

    public bool isWithItems
    {
        private get { return isWithItems; }
        set
        {
            if (value)
                animatorState = AnimatorState.runWithItems;
            isWithItems = value;
        }
    }

    private void ChangeState(AnimatorState state)
    {
        switch (state)
        {
            case AnimatorState.idle:
                animator.SetFloat(MoveHash, 0, .1f, Time.deltaTime);
                break;
            case AnimatorState.run:
                animator.SetFloat(MoveHash, characterController.velocity.magnitude, .1f, Time.deltaTime);
                break;
            case AnimatorState.runWithItems:
                animator.SetFloat(MoveHash, characterController.velocity.magnitude, .1f, Time.deltaTime);
                break;
            default:
                break;
        }
    }

    private void Update()
    {
        if (characterController.velocity.magnitude > 1)
            animatorState = AnimatorState.run;
        else
            animatorState = AnimatorState.idle;
    }
}

public enum AnimatorState
{
    idle,
    run,
    runWithItems,
    idleWithHands
}
