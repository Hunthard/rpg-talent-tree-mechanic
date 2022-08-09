using System;
using System.Collections.Generic;
using UnityEngine;

namespace Huntag.TalentTreeFeature
{
    public class TalentTreeView : MonoBehaviour
    {
        public event Action Enabled = delegate { };
        public event Action Disable = delegate { };
        
        public List<TalentButton> TalentButtons;

        #region Unity Messages

        private void OnEnable()
        {
            Enabled();
        }

        private void OnDisable()
        {
            Disable();
        }

        #endregion
    }
}
