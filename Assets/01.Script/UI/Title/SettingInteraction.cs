using Agents;
using InputManage;
using InteractSystem;
using System;
using UIManage;
using UnityEngine;

namespace InteractSystem
{
    public class SettingInteraction : InteractObject
    {

        public PausePanel Setting;

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
            Setting.HandleToggle();
        }
    }
}

