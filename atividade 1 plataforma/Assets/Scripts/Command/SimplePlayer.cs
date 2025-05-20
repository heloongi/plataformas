using System;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class SimplePlayer : MonoBehaviour
{

    public int moedas;
    private CommandManager myCommandManager;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        myCommandManager = new CommandManager();
    }

    // Update is called once per frame
    void Update()
    {
        if (Keyboard.current.upArrowKey.wasPressedThisFrame || Keyboard.current.wKey.wasPressedThisFrame)
        {
           // transform.position += Vector3.up;
           myCommandManager.AddComponent(new MoveUp(transform));
        }
        
        if (Keyboard.current.rightArrowKey.wasPressedThisFrame || Keyboard.current.dKey.wasPressedThisFrame)
        {
            // transform.position += Vector3.right;
            myCommandManager.AddComponent(new MoveRight(transform));
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Coin"))
        {
           // moedas++;
            //Destroy(other.gameObject);
            myCommandManager.AddCommand(new GetCoin(this, other,gameObject));
        }
    }

    public void UndoLastCommand()
    {
        myCommandManager.UndoCommands();
    }
}
