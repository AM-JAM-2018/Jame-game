using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace NPCs
{
    using Race = NPC.RaceEnum;

    public class NPCId : MonoBehaviour
    {
        [Serializable] 
        public class PESEL
        {
            [SerializeField] private InputEnums.CodeInputButton[] _pesel = null;

            public PESEL(int lenght)
            {
                _pesel = new InputEnums.CodeInputButton[lenght];

                int x = Enum.GetNames(typeof(InputEnums.CodeInputButton)).Length;
                for (int i = 0; i < lenght; i++)
                {
                    _pesel[i] = (InputEnums.CodeInputButton)UnityEngine.Random.Range(0, x);
                }
            }

            public InputEnums.CodeInputButton[] PeselList { get { return _pesel; } }

            public static implicit operator Queue<InputEnums.CodeInputButton> (PESEL p)
            {
                return new Queue<InputEnums.CodeInputButton>(p._pesel);
            }
        }

        [SerializeField] private Sprite[] _image = null;
        public Sprite[] Image { get { return _image; } }

        [SerializeField] PESEL _pesel = null;
        public PESEL NPCPesel { get { return _pesel; } }

        [SerializeField] private string _name = string.Empty;
        public string Name
        {
            get { return _name; }
            set { _name = value; }
        }

        [SerializeField] private string _surname = string.Empty;
        public string Surname
        {
            get { return _surname; }
            set { _surname = value; }
        }

        [SerializeField] private string _sex = string.Empty;
        public string Sex { get { return _sex; } }

        [SerializeField] private string _dateOfBirt = string.Empty;
        public string DateOfBirt { get { return _dateOfBirt; } }


        [SerializeField] private Race _race = Race.Cat;
        public Race Race { get { return _race; } }


        private void Awake()
        {
            _pesel = new PESEL(4);

            string[] name = NameDatabase.Instance.GetName();
            _name = name[0];
            _surname = name[1];
            _sex = NameDatabase.Instance.GetSex();
            _dateOfBirt = DateOfBirthDatabase.Instance.GetDate();
        }

        private void Start()
        {
            _race = GetComponent<NPC>().Race;
            _image = ImageDatabase.Instance.GetImage(_race);
        }
    }
}