using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace Huntag.TalentTreeFeature
{
    public class TalentTreeView : MonoBehaviour
    {
        public TMP_Text TalentName;
        public TMP_Text TalentDescription;

        public Button Explore;
        public Button Reset;

        public List<TalentButton> TalentButtons;

        public event UnityAction ExploreClicked = delegate { };
        public event UnityAction ResetClicked = delegate { };

        private void OnEnable()
        {
            Explore.onClick.AddListener(ExploreClicked);
            Reset.onClick.AddListener(ResetClicked);
        }

        private void OnDisable()
        {
            Explore.onClick.RemoveListener(ExploreClicked);
            Reset.onClick.RemoveListener(ResetClicked);
        }
    }
}
