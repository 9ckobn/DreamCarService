using UnityEngine;

public class GameDependencies : MonoBehaviour
{
    public static GameDependencies instance = null;

    public SpotsHandler _spotsHandler;

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else if (instance == this)
            Destroy(gameObject);
    }
}
