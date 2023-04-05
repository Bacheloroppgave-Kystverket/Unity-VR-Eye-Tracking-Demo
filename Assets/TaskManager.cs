using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TaskManager : MonoBehaviour {
    [SerializeField]
    private Task[] tasks;
    


    // Start is called before the first frame update
    void Start()
    {
      
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public Task[] GetTaskList() {
        return tasks;
    }

    public int GetRemainingTaskAmount()
    {
        int remainingTaskAmount = 0;
        foreach (Task  task in tasks)
        {
            if(!task.IsCompleted())
            {
                remainingTaskAmount++;
            }
        }
        return remainingTaskAmount;
    }
}