using UnityEngine;

namespace UI.Popups
{
    public abstract class BasePopup : MonoBehaviour
    {
        public virtual void HidePopup()
        {
            Destroy(this.gameObject);
        }
    }
}