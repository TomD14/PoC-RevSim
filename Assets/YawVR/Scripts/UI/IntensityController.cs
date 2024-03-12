using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using YawVR;

public class IntensityController : MonoBehaviour
{

    private Slider slider;

    YawController yawController;

    private void Awake()
    {
        slider = GetComponent<Slider>();
    }

    // Start is called before the first frame update
    void Start()
    {
        yawController = YawController.Instance();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ChangeRollIntensity()
    {
        yawController.SetRotationMultiplier(yawController.RotationMultiplier.y, yawController.RotationMultiplier.x, slider.value);
    }
}
