using UnityEngine;
using System.Collections.Generic;

public class CommandInvoker : MonoBehaviour
{
    private Stack<ICommand> commandStack = new Stack<ICommand>();
    private List<ICommand> commandHistory = new List<ICommand>();
    public bool IsReplaying { get; private set; }

    public void ExecuteCommand(ICommand command)
    {
        command.Execute();
        if (!IsReplaying)
        {
            commandStack.Push(command);
            commandHistory.Add(command);
        }
    }

    public void Undo()
    {
        if (commandStack.Count > 0)
        {
            ICommand command = commandStack.Pop();
            command.Undo();
        }
    }

    public IEnumerator Replay(System.Action onFinish, float delay = 1f)
    {
        IsReplaying = true;
        foreach (var command in commandHistory)
        {
            command.Execute();
            yield return new WaitForSeconds(delay);
        }
        IsReplaying = false;
        onFinish?.Invoke();
    }

    public void CancelReplayAndFinish(System.Action onFinish)
    {
        StopAllCoroutines();
        foreach (var command in commandHistory)
        {
            command.Execute();
        }
        IsReplaying = false;
        onFinish?.Invoke();
    }

    public void ClearHistory()
    {
        commandStack.Clear();
        commandHistory.Clear();
    }
}
