using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CheckListProducer : MonoBehaviour
{
    [SerializeField]
    private GameObject checkboxPrefab;
    [SerializeField]
    private TaskManager taskManager;
    private Task[] tasks;

    // Start is called before the first frame update
    void Start()
    {
        tasks = taskManager.GetTaskList();
        foreach (Task task in tasks) {
            GameObject taskBox = Instantiate(checkboxPrefab, gameObject.transform);
            taskBox.transform.parent = gameObject.transform;
            Text textField = taskBox.GetComponentInChildren<Text>();
            textField.text = task.GetTitle();
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
