using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class ObjectTaskBuilder
{
    private GameObject gameObject;

    private List<TimedTask> holdGazeTasks;

    private List<SimpleTask> lookGazeTasks;

    private List<SimpleTask> touchGameObjectTasks;

    private List<TimedTask> holdGameObjectTasks;

    private bool forcedOrder;

    /// <summary>
    /// Makes an instance of the ObjectTaskBuilder.
    /// </summary>
    /// <param name="gameObject">the gameobject</param>
    /// <param name="forceOrder">true if the order is forced. False if not.</param>
    public ObjectTaskBuilder(GameObject gameObject, bool forceOrder) {
        CheckIfObjectIsNull(gameObject, "Gameobject");
        this.gameObject = gameObject;
        holdGazeTasks = new List<TimedTask>();
        lookGazeTasks = new List<SimpleTask>();
        holdGameObjectTasks = new List<TimedTask>();
        touchGameObjectTasks = new List<SimpleTask>();
        this.forcedOrder = forceOrder;
    }

    /// <summary>
    /// Adds a hold gaze task to this object.
    /// </summary>
    /// <param name="timedTask">the timed task</param>
    /// <param name="order">the order of the task</param>
    /// <returns>true if the task was added. False otherwise</returns>
    public bool AddHoldGazeTask(TimedTask timedTask, int order) {
        bool valid = timedTask.GetTaskTitle().Length > 0;
        if (valid)
        {
            holdGazeTasks.Add(timedTask);
            timedTask.SetTaskOrderAndForcedOrder(order, forcedOrder);
        }
        return valid;
    }

    /// <summary>
    /// Adds a look gaze task to the object.
    /// </summary>
    /// <param name="simpleTask">the simple task</param>
    /// <param name="order">the order of the task</param>
    /// <returns>true if the task was added. False otherwise</returns>
    public bool AddLookGazeTask(SimpleTask simpleTask, int order) {
        bool valid = simpleTask.GetTaskTitle().Length > 0;
        if (valid)
        {
            lookGazeTasks.Add(simpleTask);
            simpleTask.SetTaskOrderAndForcedOrder(order, forcedOrder);
        }
        return valid;
    }

    /// <summary>
    /// Adds a touch task.
    /// </summary>
    /// <param name="simpleTask">a simple touch task.</param>
    /// <param name="order">the order of the task</param>
    /// <returns>true if the task was added. False otherwise</returns>
    public bool AddTouchTask(SimpleTask simpleTask, int order) {
        bool valid = simpleTask.GetTaskTitle().Length > 0;
        if (valid)
        {
            touchGameObjectTasks.Add(simpleTask);
            simpleTask.SetTaskOrderAndForcedOrder(order, forcedOrder);
        }
        return valid;
    }

    /// <summary>
    /// Adds the hold for a period task to the object.
    /// </summary>
    /// <param name="timedTask">the timed task</param>
    /// <param name="order">the order of the task</param>
    /// <returns>the object builder</returns>
    public bool AddHoldObjectForXTimeTask(TimedTask timedTask, int order) {
        bool valid = timedTask.GetTaskTitle().Length > 0;
        if (valid)
        {
            holdGameObjectTasks.Add(timedTask);
            timedTask.SetTaskOrderAndForcedOrder(order, forcedOrder);
        }
        return valid;
    }

    /// <summary>
    /// Builds the game object.
    /// </summary>
    /// <returns></returns>
    public GameObject Build() {
        holdGazeTasks.ForEach(timedTask => gameObject.AddComponent<HoldGazeController>().SetTimedTask(timedTask));
        lookGazeTasks.ForEach(simpleTask => gameObject.AddComponent<LookGazeController>().SetSimpleTask(simpleTask));
        if (gameObject.GetComponent<XRGrabInteractable>() != null) {
            touchGameObjectTasks.ForEach(touchTask => gameObject.AddComponent<TouchObjectTaskController>().SetTask(touchTask));
            holdGameObjectTasks.ForEach(holdTask => gameObject.AddComponent<HoldObjectTaskController>().SetTimedTask(holdTask));
        }
        return gameObject;
    }

    /// <summary>
    /// Checks if the object is null or not. Throws an exception if the object is null.
    /// </summary>
    /// <param name="objecToCheck">the object to check</param>
    /// <param name="error">the error to be in the string.</param>
    /// <exception cref="IllegalArgumentException">gets thrown if the object to check is null.</exception>
    private void CheckIfObjectIsNull(object objecToCheck, string error)
    {
        if (objecToCheck == null)
        {
            throw new IllegalArgumentException("The " + error + " cannot be null.");
        }
    }
}
