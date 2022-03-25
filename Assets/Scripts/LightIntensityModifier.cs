using UnityEngine;
using UnityEngine.Serialization;


public class LightIntensityModifier : MonoBehaviour
{
    Light LightComp;

    [FormerlySerializedAs("IntensityMultiplier")] [SerializeField, Range(0, 1)] float speed = 0.8f;
    void Awake()
    {
        LightComp = GetComponent<Light>();
    }

    private void Update()
    {
        LightComp.intensity = Mathf.Abs(Mathf.Sin(Time.time * speed)) + 1f;
    }
}
