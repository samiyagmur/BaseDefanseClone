using Command;
using Data.UnityObject;
using Signals;
using UnityEngine;

namespace Managers
{
    public class SaveManager : MonoBehaviour
    {
        #region Self Variables

        #region Private Variables

        private LoadGameCommand _loadGameCommand;
        private SaveGameCommand _saveGameCommand;

        #endregion Private Variables

        #endregion Self Variables

        private void Awake()
        {
            Initialization();
        }

        private void Initialization()
        {
            _loadGameCommand = new LoadGameCommand();
            _saveGameCommand = new SaveGameCommand();
        }

        #region Event Subscription

        private void OnEnable()
        {
            SubscribeEvents();
        }

        private void SubscribeEvents()
        {
            SaveLoadSignals.Instance.onSaveGameData += _saveGameCommand.Execute;
            SaveLoadSignals.Instance.onLoadGameData += _loadGameCommand.Execute<CD_Level>;
        }

        private void UnsubscribeEvents()
        {
            SaveLoadSignals.Instance.onSaveGameData -= _saveGameCommand.Execute;
            SaveLoadSignals.Instance.onLoadGameData -= _loadGameCommand.Execute<CD_Level>;
        }

        private void OnDisable()
        {
            UnsubscribeEvents();
        }

        #endregion Event Subscription
    }
}