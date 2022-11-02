using Datas.ValueObject;
using Keys;
using Signals;
using UnityEngine;

namespace Managers
{
    public class InputManager : MonoBehaviour
    {
        #region Self Variables



        #region Serialized Variables,

        [SerializeField] private FloatingJoystick joystickInput;

        #endregion Serialized Variables,

        #region Private Variables

        private InputData _data;
        private Vector2 _inputValuesVector = Vector2.zero;
        private bool _hasTouched;

        #endregion Private Variables

        #endregion Self Variables

        #region Event Subscription

        private void OnEnable() => SubscribeEvents();

        private void SubscribeEvents()
        {
            CoreGameSignals.Instance.onReset += OnReset;
        }

        private void UnsubscribeEvents()
        {
            CoreGameSignals.Instance.onReset -= OnReset;
        }

        private void OnDisable() => UnsubscribeEvents();

        #endregion Event Subscription

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
                _inputValuesVector = new Vector2(joystickInput.Horizontal, joystickInput.Vertical);

                InputSignals.Instance.onInputDragged?.Invoke(new HorizontalInputParams()
                {
                    MovementVector = _inputValuesVector
                });
                if (_inputValuesVector.sqrMagnitude != 0) return;
                _inputValuesVector = Vector2.zero;
                _hasTouched = false;
            }
            InputSignals.Instance.onInputTakenActive?.Invoke(_hasTouched);
        }

        private void OnReset()
        {
            _hasTouched = false;
        }
    }
}