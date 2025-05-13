using UnityEngine;

public class PlataformButton : MonoBehaviour
{
    public string buttonID; 

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.GetComponent<Player>() != null)
        {
            Debug.Log("Player detectado pelo botão!");
            AbrindoDoor.ButtonPressed(buttonID);
        }
    }
}
