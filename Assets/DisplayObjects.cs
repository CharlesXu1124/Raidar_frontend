using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Robotics.ROSTCPConnector;
using RosMessageTypes.Std;
using TMPro;

public class DisplayObjects : MonoBehaviour
{
    // Start is called before the first frame update

    string topic;
    string[] class_names = {"person", "bicycle", "car", "motorcycle", "airplane", "bus", "train", "truck", "boat", "traffic light",
            "fire hydrant", "stop sign", "parking meter", "bench", "bird", "cat", "dog", "horse", "sheep", "cow",
            "elephant", "bear", "zebra", "giraffe", "backpack", "umbrella", "handbag", "tie", "suitcase", "frisbee",
            "skis", "snowboard", "sports ball", "kite", "baseball bat", "baseball glove", "skateboard", "surfboard",
            "tennis racket", "bottle", "wine glass", "cup", "fork", "knife", "spoon", "bowl", "banana", "apple",
            "sandwich", "orange", "broccoli", "carrot", "hot dog", "pizza", "donut", "cake", "chair", "couch",
            "potted plant", "bed", "dining table", "toilet", "tv", "laptop", "mouse", "remote", "keyboard",
            "cell phone", "microwave", "oven", "toaster", "sink", "refrigerator", "book", "clock", "vase",
            "scissors", "teddy bear", "hair drier", "toothbrush"};

    public GameObject obstacle;

    public GameObject[] obstacles;

    void Start()
    {
        obstacle.SetActive(false);
        obstacle.transform.localPosition = new Vector3 (0.0f, 0.0f, 0.0f);
        obstacles = new GameObject[10];

        topic = "/yolo_result";
        ROSConnection.GetOrCreateInstance().Subscribe<RosMessageTypes.Std.Float32MultiArrayMsg>(topic, AddMessage);
    }

    void AddMessage(RosMessageTypes.Std.Float32MultiArrayMsg message)
    {  
        foreach (GameObject o in obstacles) {
            Destroy(o);
        }

        int counter = 0;
        for (int i = 0; i < message.data.Length / 4; i += 4) {


            obstacles[counter] = Instantiate(obstacle, transform);
            obstacles[counter].SetActive(true);

            obstacles[counter].GetComponentInChildren<TextMeshPro>().text = class_names[(int) message.data[i + 3]];

            float distance = 
                message.data[i] * message.data[i] + message.data[i + 1] * message.data[i + 1] + message.data[i + 2] * message.data[i + 2];

            if (distance < 2.0f) {
                obstacles[counter].GetComponentInChildren<TextMeshPro>().color = Color.red;
            } else if (distance < 5.0f) {
                obstacles[counter].GetComponentInChildren<TextMeshPro>().color = Color.yellow;
            } else {
                obstacles[counter].GetComponentInChildren<TextMeshPro>().color = Color.green;
            }

            obstacles[counter].transform.localPosition = new Vector3 (-message.data[i] / 10.0f, -message.data[i + 1] / 10.0f, message.data[i + 2] / 10.0f);
            // Debug.Log(class_names[(int)(message.data[i + 3])]);


            counter += 1;
            
        }

    }

}
