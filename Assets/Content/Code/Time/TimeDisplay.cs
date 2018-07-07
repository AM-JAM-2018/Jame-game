using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
namespace TimeManagent
{

    public class TimeDisplay : MonoBehaviour
    {
        [SerializeField] private Text timerDisplay = null;

        public void UpdateTimerDispaly(float time)
        {
            timerDisplay.text = TimeConverter.TimeToString(time);
        }
    }

    public class TimeConverter
    {
        public static int[] GetMinutesAndSeconds(float time)
        {
            int minutes = (int)time / 60;
            int seconds = Mathf.Abs((int)time - (60 * minutes));

            return new[] { minutes, seconds };
        }

        public static Vector3[] TimeToAngle(float time)
        {
            float angle = 360 / 60;

            var ms = GetMinutesAndSeconds(time);

            Vector3 minutesRotation = new Vector3(0, 0, angle * ms[0] * -1);
            Vector3 secondesRotation = new Vector3(0, 0, angle * ms[1] * -1);

            return new[] { minutesRotation, secondesRotation };
        }

        public static string TimeToString(float time)
        {
            var ms = GetMinutesAndSeconds(time);

            return string.Format("{0}:{1}", ms[0], ms[1].ToString().Length == 1 ? string.Format("0{0}", ms[1]) : ms[1].ToString());
        }

    }
}