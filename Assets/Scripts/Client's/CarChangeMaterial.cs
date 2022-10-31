using UnityEngine;

public class CarChangeMaterial : MonoBehaviour
{
    [SerializeField] private Material[] Materials;
    [SerializeField] private MeshRenderer MaterialToChange;

    [SerializeField] private int materialIndex = 0;

    void OnEnable()
    {
        var mat = MaterialToChange.materials[materialIndex];
        mat.mainTexture = Materials[Random.Range(0, Materials.Length)].mainTexture;
    }
}
