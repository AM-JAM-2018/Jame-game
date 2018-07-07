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

            public static implicit operator Queue<InputEnums.CodeInputButton> (PESEL p)
            {
                return new Queue<InputEnums.CodeInputButton>(p._pesel);
            }
        }

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

        private void Awake()
        {
            _pesel = new PESEL();

            string[] name = NameDatabase.Instance.GetName();
            _name = name[0];
            _surname = name[1];
        }
    }
}