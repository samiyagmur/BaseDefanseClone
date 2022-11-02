using UnityEngine;

namespace Interfaces
{
    public interface IStack
    {
        void SetStackHolder(GameObject gameObject);

        void SetGrid();

        void SendGridDataToStacker();
    }
}