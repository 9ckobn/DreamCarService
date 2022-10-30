using System.Threading.Tasks;
using System;
using System.Collections;
using UnityEngine;
using Cysharp.Threading.Tasks;

public class ItemSender : MonoBehaviour
{
    private Player _player;

    public bool itemSended;
    
    public void GetObjectToHands(Player player, GameObject currentItem)
    {
        _player = player;

        var routine = SendObjectByParabola(currentItem, _player.StackPointer.transform.localPosition, false);

        StartCoroutine(routine);
    }

    public void SpendObject(Player player, GameObject currentItem, Vector3 endPosition)
    {
        _player = player;

        var routine = SendObjectByParabola(currentItem, endPosition, true);

        StartCoroutine(routine);
    }
    IEnumerator SendObjectByParabola(GameObject itemToSend, Vector3 endPosition, bool needToDestroy)
    {
        float elapsedTime = 0;
        float totalTime = _player.playerConfig._timeToGetItemInMS / 1000f;

        while (elapsedTime < totalTime)
        {
            itemToSend.transform.localPosition = Parabola(itemToSend.transform.localPosition,
            endPosition,
            _player.StackPointer.transform.position.y + _player.ParabolaHeight,
            elapsedTime / totalTime);

            elapsedTime += Time.deltaTime;
            yield return null;
        }

        itemSended = true;

        if (needToDestroy)
            Destroy(itemToSend);

        yield break;
    }

    public static Vector3 Parabola(Vector3 start, Vector3 end, float height, float t)
    {
        Func<float, float> f = x => -1 * height * x * x + 1 * height * x;

        var mid = Vector3.Lerp(start, end, t);

        return new Vector3(mid.x, f(t) + Mathf.Lerp(start.y, end.y, Mathf.SmoothStep(0, 1, t)), mid.z);
    }

}