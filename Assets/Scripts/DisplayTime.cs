using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;



public class DisplayTime : MonoBehaviour
{
    public Text _curTime;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        var curr = DateTime.Now;
        _curTime.text = curr.ToString("HH:mm");
        
    }
}
