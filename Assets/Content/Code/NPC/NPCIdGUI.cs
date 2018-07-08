using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace NPCs
{
    public class NPCIdGUI : MonoBehaviour
    {
        public static NPCIdGUI Instance { get; private set; }

        [SerializeField] private Image[] _imageLayers = null;

        [SerializeField] private Text _name = null;
        [SerializeField] private Text _surname = null;
        [SerializeField] private Text _pesel = null;
        [SerializeField] private Text _sex = null;
        [SerializeField] private Text _dateOfBirth = null;

        [SerializeField, Space] private string _unfoldAnimationParam = "DisplayState";
        [SerializeField] Animator _animator = null;
        [SerializeField] private bool _isHidden = false;
        public bool IsHidden { get { return _isHidden; } }


        private void Awake()
        {
            if (Instance == null)
                Instance = this;
            else
                Destroy(this.gameObject);

            gameObject.SetActive(false);
        }

        public void SetID(NPCId id)
        {
            _name.text = id.Name;
            _surname.text = id.Surname;

            _pesel.text = string.Empty;
			for (int i = 0; i < id.NPCPesel.PeselList.Length; i++)
				_pesel.text += AlienLanguageManager.Instance.GetValue(id.Race, id.NPCPesel.PeselList[i]);

            for (int i = 0; i < _imageLayers.Length; i++)
                if(i < id.Image.Length)
                {
                    _imageLayers[i].gameObject.SetActive(true);
                    _imageLayers[i].sprite = id.Image[i];
                }
                else
                    _imageLayers[i].gameObject.SetActive(false);

            _sex.text = id.Sex;
            _dateOfBirth.text = id.DateOfBirt;

            gameObject.SetActive(true);
        }

        public void SetUnfoldState(bool state)
        {
			_animator.SetBool(_unfoldAnimationParam, state);
			_isHidden = state;
        }
        
        public void Show()
        {
            _animator.SetBool(_unfoldAnimationParam, true);
            _isHidden = false;
        }

        public void Hide()
        {
            _animator.SetBool(_unfoldAnimationParam, false);
            _isHidden = true;
        }
    }
}