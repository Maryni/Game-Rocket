using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace Project.UI
{
    public class UIController : MonoBehaviour
    {
        [SerializeField] private GameObject _game;
        [SerializeField] private GameObject _menu;
        [SerializeField] private TMP_Text _money;

        #region public functions

        public void ChangeViewGame() => ChangeView(_game);
        public void ChangeViewMenu() => ChangeView(_menu);
        public void SetMoneyText(string value) => SetText(_money, value);

        #endregion public functions

        #region private functions

        private void ChangeView(GameObject item) => item.SetActive(!item.activeSelf);
        private void SetText(TMP_Text text, string value) => text.text = value;

        #endregion private functions
    }
}