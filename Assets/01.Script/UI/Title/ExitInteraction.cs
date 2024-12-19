using InteractSystem;
using UnityEngine;

public class ExitInteraction : InteractObject
{
    private void Awake()
    {
        OnUnDetectedEvent.AddListener(Clear);
        OnDetectedEvent.AddListener(PannelConnection);
    }

    private void PannelConnection()
    {
        OnInteractEvent.AddListener(OpenPanel);
    }

    private void Clear()
    {
        OnInteractEvent.RemoveListener(OpenPanel);
    }

    private void OnDestroy()
    {
        OnUnDetectedEvent.RemoveListener(Clear);
        OnDetectedEvent.RemoveListener(PannelConnection);
    }

    private void OpenPanel()
    {
        
    }
}
