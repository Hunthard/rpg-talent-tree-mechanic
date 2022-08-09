using System;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Huntag.TalentTreeFeature
{
    public class TalentButton : Graphic, IPointerClickHandler
    {
        public class TalentButtonEventArgs : EventArgs
        {
            public Image Icon;
            public Talent Talent;

            public TalentButtonEventArgs() : base()
            { }

            public TalentButtonEventArgs(Image icon, Talent talent) : this()
            {
                Icon = icon;
                Talent = talent;
            }
        }
        
        public event EventHandler<TalentButtonEventArgs> Clicked = delegate { };

        [SerializeField]
        private Image _icon;
        public Image Icon => _icon;

        [SerializeField]
        private TMP_Text _name;
        public TMP_Text Name => _name;

        public Talent Talent { get; set; }

        #region Public Methods

        public void OnPointerClick(PointerEventData eventData)
        {
            Clicked(this, new TalentButtonEventArgs(Icon, Talent));
        }

        #endregion

        #region Private Methods

        #endregion
    }
}