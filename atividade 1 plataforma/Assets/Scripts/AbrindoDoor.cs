using System;

public static class AbrindoDoor
{
    public static Action<string> OnButtonPressed;
    
    public static void ButtonPressed(string buttonID)
    {
        OnButtonPressed?.Invoke(buttonID);
    }
}
