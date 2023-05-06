using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectTaskBuilder
{
    private GameObject gameObject;

    private List<TimedTask> holdGazeTasks;

    private List<SimpleTask> lookGazeTasks;

    private List<SimpleTask> touchGameObjectTasks;

    private List<TimedTask> holdGameObjectTasks;

    /// <summary>
    /// Makes an instance of the ObjectTaskBuilder.
    /// </summary>
    /// <param name="gameObject">the gameobject</param>
    public ObjectTaskBuilder(GameObject gameObject) {
        CheckIfObjectIsNull(gameObject, "Gameobject");
        this.gameObject = gameObject;
        holdGazeTasks = new List<TimedTask>();
        lookGazeTasks = new List<SimpleTask>();
        holdGameObjectTasks = new List<TimedTask>();
        touchGameObjectTasks = new List<SimpleTask>();
    }

    /// <summary>
    /// Adds a hold gaze task to this object.
    /// </summary>
    /// <param name="timedTask">the timed task</param>
    /// <returns>the object builder</returns>
    public ObjectTaskBuilder AddHoldGazeTask(TimedTask timedTask) { 
        holdGazeTasks.Add(timedTask);
        return this;
    }

    /// <summary>
    /// Adds a look gaze task to the object.
    /// </summary>
    /// <param name="simpleTask">the simple task</param>
    /// <returns>the object builder</returns>
    public ObjectTaskBuilder AddLookGazeTask(SimpleTask simpleTask) { 
        lookGazeTasks.Add(simpleTask);
        return this;
    }

    /// <summary>
    /// Builds the game object.
    /// </summary>
    /// <returns></returns>
    public GameObject Build() {
        holdGazeTasks.ForEach(timedTask => gameObject.AddComponent<HoldGazeController>().SetTimedTask(timedTask));
        lookGazeTasks.ForEach(simpleTask => gameObject.AddComponent<LookGazeController>().SetSimpleTask(simpleTask));
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
