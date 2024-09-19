using UnityEngine;

public class SliderScript : MonoBehaviour
{
    [SerializeField] private UnityEngine.UI.Slider slider;
    void Start()
    {
        Application.targetFrameRate = 20;
        slider.enabled = true;
    }

    void Update()
    {
        Application.targetFrameRate = (int)slider.value;
    }
}
