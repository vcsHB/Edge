using System.Collections;
using UIManage;
using UnityEngine;

namespace InteractSystem
{
    public class StartInteraction : InteractObject
    {
        [SerializeField] private string scneeName;
        [SerializeField] private UIMovePanel[] movePannel;

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

        private IEnumerator MovePanels()
        {
            for(int i =0;i < movePannel.Length; i++)
            {
                movePannel[i].Open();
                yield return new WaitForSeconds(1f);
            }
        }
    }
}


