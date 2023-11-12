using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ImuDisplay : MonoBehaviour
{
    public Text speedTxt;
    public float speed;
    // Start is called before the first frame update
    void Start()
    {
        speed = 0.0f;
        Input.gyro.enabled = true;
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 gyro_value = Input.gyro.rotationRateUnbiased;
        Vector3 accel_value = new Vector3 (Input.acceleration.x, Input.acceleration.y, Input.acceleration.z);
        float accel_no_gravity = Mathf.Sqrt(accel_value.x * accel_value.x + accel_value.y * accel_value.y + accel_value.z * accel_value.z) - 1.0f;
        speed += Time.deltaTime * accel_no_gravity;
        if (accel_no_gravity < 0.1f) speed = 0.0f;
        speedTxt.text = (speed * 9.81 * 2.24).ToString("F2") + " MPH";
    }
}
