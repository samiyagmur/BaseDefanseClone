using Extentions;
using Interfaces;
using Keys;
using System.Collections;
using UnityEngine;

namespace Controllers
{
  
    public class TurretMovementController : RefMonoBehavior, IUsuable
    {

        private HorizontalInputParams _inputParams;

        public void SetInputParams(HorizontalInputParams value)
        {
            _inputParams = value;

            Debug.Log(_inputParams.MovementVector);
        }
        

    }

    
   
}