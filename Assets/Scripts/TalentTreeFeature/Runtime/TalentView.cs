using System;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class TalentView : Graphic, IPointerClickHandler
{
    public class TalentViewEventArgs : EventArgs
    {
        public TalentView TalentView;

        public TalentViewEventArgs(TalentView talentView) : base()
        {
            TalentView = talentView;
        }
    }

    public event EventHandler<TalentViewEventArgs> Clicked = delegate { };

    [SerializeField]
    private Image _icon;
    public Image Icon => _icon;

    [SerializeField]
    private TMP_Text _name;
    public TMP_Text Name => _name;

    #region Public Methods

    public void OnPointerClick(PointerEventData eventData)
    {
        Clicked(this, new TalentViewEventArgs(this));
        Debug.Log(_name.text);
    }

    #endregion

    #region Private Methods

    #endregion
}
