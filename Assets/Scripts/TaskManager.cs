using NUnit.Framework;
using TMPro;
using UnityEngine;
using System.Collections.Generic;

[System.Serializable]
public class Task
{
    public string taskName;
    public bool isCompleted = false;
    public TextMeshProUGUI Text;

    public void MarkCompleted()
    {
        isCompleted = true;
        if(Text != null)
        {
            Text.color = Color.green;
        }
    }
}
public class TaskManager : MonoBehaviour
{
    public static TaskManager Instance { get; private set; }
    [SerializeField] private List<Task> tasks = new List<Task>();

    private void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
    public void CompleteTask(string name)
    {
        foreach(var t in tasks)
        {
            if(t.taskName == name && !t.isCompleted)
            {
                t.MarkCompleted();
                break;
            }
        }
    }

    public void CompleteAllTasks(Task task)
    {
        if(task != null && !task.isCompleted)
        {
            task.MarkCompleted();
        }
    }

    public bool AllTasksCompleted()
    {
        foreach(var t in tasks)
        {
            if(!t.isCompleted) return false;
        }
        return true;
    }
}
