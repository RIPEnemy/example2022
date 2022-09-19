using UnityEngine;

public class TesterGlobalEventHandler : MonoBehaviour
{
    public void HandleObtainEmpty()
    {
        Debug.Log("<- Handle empty OBTAIN.");
    }

    public void HandleObtain(object sender, object data)
    {
        Debug.Log(
            $"<- Obtain '{(data as GameObject).name}'"
            + $" from '{(sender as ObjectPool).name}' pool."
        );
    }

    public void HandleReleaseEmpty()
    {
        Debug.Log("<- Handle empty RELEASE.");
    }

    public void HandleRelease(object sender, object returnedObject)
    {
        Debug.Log(
            $"-> Return '{(returnedObject as GameObject).name}'"
            + $" to '{(sender as ObjectPool).name}' pool."
        );
    }
}
