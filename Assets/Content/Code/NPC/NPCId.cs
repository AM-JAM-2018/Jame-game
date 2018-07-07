using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace NPCs
{
    public class NPCId : MonoBehaviour
    {
        [Serializable] 
        public class PESEL
        {
            [SerializeField] private InputEnums.CodeInputButton[] _pesel = null;

            public PESEL() : this(4) {}
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

        [SerializeField] PESEL _pesel = null;
        public PESEL NPCPesel { get { return _pesel; } }

        [SerializeField] private string _name = string.Empty;
        public string Name
        {
            get { return _name; }
        }

        [SerializeField] private string _surname = string.Empty;
        public string Surname
        {
            get { return _surname; }
        }

        [SerializeField] private string _sex = string.Empty;
        public string Sex { get { return _sex; } }

        [SerializeField] private string _dateOfBirt = string.Empty;
        public string DateOfBirt { get { return _dateOfBirt; } }

        public Sprite[] Image
        {
            get
            {
                return _image;
            }
        }


        private void Awake()
        {
            _pesel = new PESEL();

            string[] name = NameDatabase.Instance.GetName();
            _name = name[0];
            _surname = name[1];

            _image = ImageDatabase.Instance.GetImage(NPC.RaceEnum.Man);
            _sex = NameDatabase.Instance.GetSex();
            _dateOfBirt = DateOfBirthDatabase.Instance.GetDate();
        }
    }
}