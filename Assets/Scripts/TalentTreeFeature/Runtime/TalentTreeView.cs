using System.Collections.Generic;
using UnityEngine;
using TMPro;
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
    }
}
