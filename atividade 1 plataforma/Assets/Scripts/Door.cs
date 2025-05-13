using UnityEngine;

public class Door : MonoBehaviour
{
   public string linkedButtonID; 

   private void OnEnable()
   {
      AbrindoDoor.OnButtonPressed += OnButtonPressed;
   }

   private void OnDisable()
   {
      AbrindoDoor.OnButtonPressed -= OnButtonPressed;
   }

   private void OnButtonPressed(string buttonID)
   {
      Debug.Log("Porta escutou: " + buttonID);
      if (buttonID == linkedButtonID)
      {
         Debug.Log("Porta destruída!");
         Destroy(gameObject); 
      }
   }
}
