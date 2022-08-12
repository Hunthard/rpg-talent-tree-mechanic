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
        public TMP_Text PointsLeft;

        public Button Explore;
        public Button Reset;
        public Button ResetAll;
        public Button AddPoint;

        public List<TalentButton> TalentButtons;

        public event UnityAction ExploreClicked = delegate { };
        public event UnityAction ResetClicked = delegate { };
        public event UnityAction ResetAllClicked = delegate { };
        public event UnityAction AddPointClicked = delegate { };

        private void OnEnable()
        {
            Explore.onClick.AddListener(ExploreClicked);
            Reset.onClick.AddListener(ResetClicked);
            ResetAll.onClick.AddListener(ResetAllClicked);
            AddPoint.onClick.AddListener(AddPointClicked);
        }

        private void OnDisable()
        {
            Explore.onClick.RemoveListener(ExploreClicked);
            Reset.onClick.RemoveListener(ResetClicked);
            ResetAll.onClick.RemoveListener(ResetAllClicked);
            AddPoint.onClick.RemoveListener(AddPointClicked);
        }
    }
}
