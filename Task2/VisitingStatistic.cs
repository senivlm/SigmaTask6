using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SigmaTest
{
    class VisitingStatistic
    {2 треба було шукати моду. Можна було спробувати написати параметризований метод 
        List<IpInfo> arrIp;

        public IpInfo this[int index]
        {
            get
            {
                if ((index >= arrIp.Count) || (index < 0))
                    throw new IndexOutOfRangeException("Wrong index");
                return arrIp[index];
            }
        }
Для чого?
        public VisitingStatistic()
        : this("N/A") { }
        public VisitingStatistic(string path)
        {
            arrIp = new List<IpInfo>();
            ReadFromFile(path);
        }

        public void ReadFromFile(string path)
        {
            try
            {
                if (File.Exists(path))
                {
                    using (StreamReader reader = new StreamReader(path))
                    {
                        string line;

                        //відповідає чи не було повторів IP
                        int indexRepeat;
                        //поки не дійдемо кінця файлу
                        while ((line = reader.ReadLine()) != null)
                        {
                            indexRepeat = -1;
                            string[] lineSplit = line.Split();
                            if (lineSplit.Length != 3)
                                throw new ArgumentException("Wrong text input");

                            //отримуємо потрібні дані
                            //ім'я
                            string ipName = lineSplit[0];
                            if (ipName.Length < 14)
                            {
                                throw new ArgumentException("Wrong name input");
                            }
                            //година
                            string[] str_time = lineSplit[1].Split(':');
                            if (str_time.Length != 3)
                                throw new ArgumentException("Wrong time input");

                            int hour;
                            if (!Int32.TryParse(str_time[0], out hour) || (hour < 0) || (hour > 23))
                                throw new ArgumentException("Wrong hour input");

                            //день тижня
                            string day = lineSplit[2];


                            //перевіряємо чи вже записаний ip
                            for (int k = 0; k < arrIp.Count; k++)
                            {
                                if ((arrIp[k].IpName == ipName))
                                {
                                    indexRepeat = k;
                                    break;
                                }
                            }
                            //якщо  нема то додати елемент
                            if (indexRepeat == -1)
                            {
                                arrIp.Add(new IpInfo());
                                int lastIndex = arrIp.Count - 1;
                                arrIp[lastIndex].IpName = ipName;
                                arrIp[lastIndex].AddDay(day);
                                arrIp[lastIndex].AddTime(hour);
                            }
                            //якщо є то оновити дані
                            else
                            {
                                arrIp[indexRepeat].AddDay(day);
                                arrIp[indexRepeat].AddTime(hour);
                            }
                        }
                    }
                }
                else
                {
                    throw new FileNotFoundException("File not found");
                }
            }
            catch (FormatException ex)
            {
                Console.WriteLine(ex.Message);
            }
            catch (FileNotFoundException ex)
            {
                Console.WriteLine(ex.Message);
            }
            catch (ArgumentException ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        public string GetMostPopularTimeOverall()
        {
            string res = "There is no popular time";
            int maxCount = -1;
            for (int hour = 0; hour <= 24; hour++)
            {
                int tempCount = 0;
                for (int i = 0; i < arrIp.Count; i++)
                {
                    if (Int32.Parse(arrIp[i].GetMostPopularTime()) == hour)
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
            string res = "All info\n";
            for (int i = 0; i < arrIp.Count; i++)
            {
                res += arrIp[i].ToString();
            }
            return res;
        }
    }
}
