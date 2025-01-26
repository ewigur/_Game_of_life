using UnityEngine;

public class SliderScript : MonoBehaviour
{
    [SerializeField] private UnityEngine.UI.Slider slider;
    void Start()
    {
        Application.targetFrameRate = 10;
        slider.enabled = true;
        slider.value = 10;
    }

    private void Update()
    {
        ChangeValue();
    }

    public void ChangeValue()
    {
        if (Input.GetKeyDown(KeyCode.RightArrow) && slider.value < slider.maxValue)
        {
            slider.value += 10;
            Application.targetFrameRate = (int)slider.value;
        }

        if (Input.GetKeyDown(KeyCode.LeftArrow) && slider.value > slider.minValue)
        {
            slider.value -= 10;
            Application.targetFrameRate = (int)slider.value;
        }
    }
}
