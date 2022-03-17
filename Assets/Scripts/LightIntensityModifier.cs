using UnityEngine;
using UnityEngine.Serialization;


public class LightIntensityModifier : MonoBehaviour
{
    Light LightComp;

    [FormerlySerializedAs("IntensityMultiplier")] [SerializeField, Range(1, 5)] float intensityMultiplier;
    void Awake()
    {
        LightComp = GetComponent<Light>();
    }

    private void Update()
    {
        LightComp.intensity = Mathf.Sin(Time.time * intensityMultiplier) + 1f;
    }
}
