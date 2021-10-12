using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SigmaTest
{
    enum Days { monday = 1, tuesday, wednesday, thursday, friday, saturday, sunday }
    class IpInfo
    {
        public string IpName { get; set; }
        //List для динамічних масивів
        //ми не знаємо неперед, скільки буде даних
        List<Days> days;
        List<int> times;

        public IpInfo()
        {
            days = new List<Days>();
            times = new List<int>();
        }

        public void AddDay(string day)
        {
            Days temp;
            if (!Enum.TryParse(day, out temp))
            {
                throw new ArgumentException("Wrong day");
            }
            days.Add(temp);
        }

        public void AddTime(int time)
        {
            if (time < 0 || time > 23)
            {
                throw new ArgumentException("Wrong hour");
            }
            times.Add(time);
        }

        public int GetRepeatNumber()
        {
            return days.Count;
        }
        public string GetMostPopularDay()
        {
            string res = "";
            if (days.Count == 0)
                return "There is no popular days";
            int maxCount = -1;
            foreach (Days dayOfWeek in Enum.GetValues(typeof(Days)))
            {
                int curentCount = 0;
                for (int i = 0; i < days.Count; i++)
                {
                    if (days[i] == dayOfWeek)
                    {
                        curentCount++;
                    }
                }
                if (curentCount >= maxCount)
                {
                    maxCount = curentCount;
                    res = Enum.GetName(typeof(Days), dayOfWeek);

                }
            }
            return res;
        }

        public string GetMostPopularTime()
        {
            string res = "There is no popular time";
            int maxCount = -1;
            for (int hour = 0; hour <= 24; hour++)
            {
                int tempCount = 0;
                for (int i = 0; i < times.Count; i++)
                {
                    if (times[i] == hour)
                    {
                        tempCount++;
                    }
                }
                if (tempCount >= maxCount)
                {
                    maxCount = tempCount;
                    res = hour.ToString();
                }
            }
            return res;
        }

        public override string ToString()
        {
            string res = "";
            res += "IP Name: " + IpName;
            res += "\nHours repeat: ";
            for (int i = 0; i < times.Count; i++)
            {
                res += times[i] + "  ";
            }
            res += "\nDays repeat: ";
            for (int i = 0; i < times.Count; i++)
            {
                res += Enum.GetName(typeof(Days), days[i]) + "  ";
            }
            res += "\n\n";
            return res;
        }
    }
}

