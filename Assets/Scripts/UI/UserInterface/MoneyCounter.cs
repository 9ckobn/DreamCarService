using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MoneyCounter : MonoBehaviour
{
    private int _moneyCount;

    public int MoneyCount
    {
        get
        {
            return _moneyCount;
        }
        set
        {
            _moneyCount = value;
            GameDependencies.instance._player.playerConfig._moneyCount += value;
            MoneyCountText.text = value.ToString();
            StartCoroutine(CountAnimation());
            EventListener.OnMoneyEarned();
        }
    }

    [SerializeField] private Text MoneyCountText;
    [SerializeField] private Text DollarText;

    private IEnumerator CountAnimation()
    {
        Vector3 scale = new Vector3(.01f, .01f, .01f);

        for (int i = 0; i < 10; i++)
        {
            yield return new WaitForSeconds(0.005f);
            MoneyCountText.gameObject.transform.localScale += scale;
            DollarText.gameObject.transform.localScale += scale;
        }
        for (int i = 10; i > 0; i--)
        {
            yield return new WaitForSeconds(0.005f);
            MoneyCountText.gameObject.transform.localScale -= scale;
            DollarText.gameObject.transform.localScale -= scale;
        }

        yield break;
    }
}
