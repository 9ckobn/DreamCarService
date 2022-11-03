using UnityEngine;

public class GameDependencies : MonoBehaviour
{
    public static GameDependencies instance = null;

    public SpotsHandler _spotsHandler;
    public MoneyGenerator _moneyGenerator;
    public StatsHandler _statsHandler;
    public Player _player;
    public InGameShopHandler _inGameShop;
    public VFXManager _vfxManager;

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else if (instance == this)
            Destroy(gameObject);
    }
}
