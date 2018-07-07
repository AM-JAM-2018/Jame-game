using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace NPCs
{
    public class NPCIdGUI : MonoBehaviour
    {
        [SerializeField] private Image[] _imageLayers = null;

        [SerializeField] private Text _name = null;
        [SerializeField] private Text _surname = null;
        [SerializeField] private Text _pesel = null;
        [SerializeField] private Text _sex = null;
        [SerializeField] private Text _dateOfBirth = null;

        [SerializeField] private NPCId _id = null;
        private void Awake()
        {
            if (ImageDatabase.Instance != null) ;
        }

        public void SerID(NPCId id)
        {
            _name.text = id.Name;
            _surname.text = id.Surname;

            _pesel.text = string.Empty;
            for (int i = 0; i < id.NPCPesel.PeselList.Length; i++)
                _pesel.text += (int)id.NPCPesel.PeselList[i] + 1;

            for (int i = 0; i < _imageLayers.Length; i++)
                if(i < id.Image.Length)
                {
                    _imageLayers[i].gameObject.SetActive(true);
                    _imageLayers[i].sprite = id.Image[i];
                }
                else
                    _imageLayers[i].gameObject.SetActive(false);

            _sex.text = _id.Sex;
            _dateOfBirth.text = _id.DateOfBirt;
        }

        private void OnValidate()
        {
            if (_id != null)
                SerID(_id);
        }
    }
}