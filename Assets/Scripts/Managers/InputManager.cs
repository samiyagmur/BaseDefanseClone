using Datas.UnityObject;
using Datas.ValueObject;
using Enums;
using Keys;
using Signals;
using System;
using UnityEngine;


namespace Managers
{
    public class InputManager : MonoBehaviour
    {
        #region Self Variables

        #region Public Variables

        #endregion

        #region Serialized Variables,

        [SerializeField] private FloatingJoystick joystickInput;

        #endregion

        #region Private Variables

        private InputData _data;
        private Vector2 _inputValuesVector = Vector2.zero;
        private bool _hasTouched;

      
        #endregion

        #endregion


        private void Update()
        {
            
            JoystickInputUpdate();
        }

        private void JoystickInputUpdate()
        {
            if (Input.GetMouseButton(0))
            {
                _hasTouched = true;
            }
            if (!_hasTouched) return;
            {   
                if ((joystickInput.Direction - _inputValuesVector).sqrMagnitude == 0) return;
                _inputValuesVector = new Vector2(joystickInput.Horizontal, joystickInput.Vertical);

                InputSignals.Instance.onInputDragged?.Invoke(new HorizontalInputParams()
                {
                    MovementVector = _inputValuesVector
                });
                if (_inputValuesVector.sqrMagnitude != 0) return;
                _inputValuesVector = Vector2.zero;
                _hasTouched = false;
            }

        }


    }
}