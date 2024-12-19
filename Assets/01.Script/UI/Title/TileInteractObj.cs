using InputManage;
using System;
using UnityEngine;

namespace UI.Title
{

    public enum PanelType
    {
        Start,Setting,Exit
    }

    public class TileInteractObj : MonoBehaviour
    {
        [SerializeField] private PlayerInput _inputReader;
        [SerializeField] private float _raius;
        [SerializeField] private LayerMask _plaeyrLayer;
        [SerializeField] private GameObject _panel;
        [SerializeField] private PanelType _panelType;

        private void Awake()
        {
            
        }

        
    }
}

