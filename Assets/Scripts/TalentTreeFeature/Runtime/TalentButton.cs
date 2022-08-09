using System;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Huntag.TalentTreeFeature
{
    public class TalentButton : Graphic, IPointerClickHandler
    {
        public event EventHandler Clicked = delegate { };

        [SerializeField]
        private Image _icon;
        public Image Icon => _icon;

        [SerializeField]
        private TMP_Text _name;
        public TMP_Text Name => _name;

        #region Public Methods

        public void OnPointerClick(PointerEventData eventData)
        {
            Clicked(this, new EventArgs());
            Debug.Log(_name.text);
        }

        #endregion

        #region Private Methods

        #endregion
    }
}