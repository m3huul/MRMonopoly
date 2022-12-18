using JMRSDK;
using JMRSDK.Toolkit.UI;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UpdateIPDValue : MonoBehaviour
{
    public TextMeshProUGUI ipdValueText;
    public Slider slider;

    private void Start()
    {
        ipdValueText.text = slider.value.ToString("f2");
    }

    private void OnEnable()
    {
        slider.onValueChanged.AddListener(UpdateIPDText);
    }

    private void OnDisable()
    {
        slider.onValueChanged.RemoveListener(UpdateIPDText);
    }

    public void UpdateIPDText(float value)
    {
        ipdValueText.text = value.ToString("f2");
        JMRRigManager.Instance.SetIPD(value);
    }
}
