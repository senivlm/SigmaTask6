using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SigmaTest
{
    class Polynomial : IComparable<Polynomial>
    {
        private double[] polynKoefs;
        //масив коефіціентів поліноми
        //polinKoefs[0] - коеф при нульвому степені
        //polinKoefs[i] - коеф при і-тому степені


        //конструктор без параметрів-----
        public Polynomial()
            : this(new double[] { 1, 2 }) { }
        //з параметром koefs - масив коефіціентів------ 
        public Polynomial(double[] koefs)
        {
            try
            {
                PolinomKoefs = koefs;
            }
            catch (ArgumentException ex)
            {
                Console.WriteLine(ex.Message);
            }

        }
        //властивість масиву коефіціентів------------
        public double[] PolinomKoefs
        {
            get { return this.polynKoefs; }
            set
            {
                //якщо масив не порожній 
                if (value.Length > 0)
                {
                    this.polynKoefs = value;
                }
                else
                {
                    throw new ArgumentException("Empty Array");
                }
            }
        }
        //індиксатор для доступу до коефіціентів------------
        public double this[int index]
        {
            get
            {
                return this.polynKoefs[index];
            }
            set
            {
                //коеф не рівний нулю
                if (value != 0)
                {
                    //вже існує - заміна
                    if (this.polynKoefs.Length >= index)
                    {
                        this.polynKoefs[index] = value;
                    }
                    //інакше створити
                    else
                    {
                        double[] temp_arr = new double[index + 1];
                        //створюємо новий поліном
                        for (int i = 0; i < index + 1; i++)
                        {
                            if (i < this.polynKoefs.Length)
                                temp_arr[i] = this.polynKoefs[i];
                            else if (i == index)
                                temp_arr[i] = value;
                            else
                                temp_arr[i] = 0;
                        }
                        //переносимо зміни
                        PolinomKoefs = temp_arr;
                    }
                }
                //коеф рівний нулю
                else
                {
                    //коеф з таким степенем вже існує - видалити
                    //присвоюємо просто 0, при виводі його не буде вказано
                    if (this.polynKoefs.Length >= index)
                    {
                        this.polynKoefs[index] = value;
                    }

                    //Якщо не існує - нічого не робити
                }
                polynKoefs[index] = value;
            }
        }
        //Отримати поліном з стрічки------------------
        public void Parse(string polynom)
        {
            try
            {
                string[] polSplit = polynom.Split();
                //розмір поліному
                int polSize = polSplit.Length;
                //тимчасова змінна для зберігання кеоф
                double[] temp_arr = new double[polSize];
                //її поточний індекс
                int temp_index = 0;
                //перший кеофіціент
                if (!Double.TryParse(polSplit[0], out temp_arr[temp_index++]))
                {
                    throw new ArgumentException("Wrong Argument");
                }
                //дістаємо інші коеф
                for (int i = 2; i < polSize; i = i + 2)
                {
                    //розділяємо число і x через знак '*'
                    string num = polSplit[i].Split('*')[0];
                    if (!Double.TryParse(num, out temp_arr[temp_index++]))
                    {
                        throw new ArgumentException("Wrong Arguments");
                    }
                }
                //зберігаємо зміни
                this.PolinomKoefs = temp_arr;
            }
            catch (IndexOutOfRangeException ex)
            {
                Console.WriteLine("Wrong input arguments(index out of range)");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
        //додати поліноми-------------------
        public static Polynomial operator +(Polynomial p1, Polynomial p2)
        {
            try
            {
                if (p2.PolinomKoefs.Length == 0 || p1.PolinomKoefs.Length == 0)
                {
                    throw new ArgumentException("Polynomes are empty");
                }
                //визначити який має більший розмір
                int maxSize = Math.Max(p1.PolinomKoefs.Length, p2.PolinomKoefs.Length);
                double[] temp = new double[maxSize];
                //тепер додаємо коефи з обох поліномів
                for (int i = 0; i < p1.PolinomKoefs.Length; i++)
                {
                    temp[i] = p1[i];
                }
                for (int i = 0; i < p2.PolinomKoefs.Length; i++)
                {
                    temp[i] += p2[i];
                }
                //вертаємо зміни
                return new Polynomial(temp);
            }
            catch (ArgumentException ex)
            {
                Console.WriteLine(ex.Message);
                return new Polynomial();
            }
            catch (IndexOutOfRangeException ex)
            {
                Console.WriteLine(ex.Message);
                return new Polynomial();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return new Polynomial();
            }
        }
        //додати з протилежним знаком--------------------
        public static Polynomial operator -(Polynomial p1, Polynomial p2)
        {
            double[] temp = new double[p2.PolinomKoefs.Length];
            for (int i = 0; i < p2.PolinomKoefs.Length; i++)
            {
                temp[i] = -p2[i];
            }
            return p1 + new Polynomial(temp);
        }
        //перемножити поліноми-------------------------
        public static Polynomial operator *(Polynomial p1, Polynomial p2)
        {
            try
            {
                if (p1.PolinomKoefs.Length == 0 || p2.polynKoefs.Length == 0)
                {
                    throw new ArgumentException("Polynomes are empty");
                }
                //взнаємо новий розмір масиву коефіціентів
                int newSize = p1.PolinomKoefs.Length + p2.polynKoefs.Length - 1;


                //автоматисно заповниться нулями
                double[] temp = new double[newSize];

                //отримуємо коефіціенти
                for (int i = 0; i < p1.PolinomKoefs.Length; i++)
                {
                    for (int k = 0; k < p2.PolinomKoefs.Length; k++)
                    {
                        temp[i + k] += p1[i] * p2[k];
                    }
                }
                //зберігаємо зміни 
                return new Polynomial(temp);
            }
            catch (ArgumentException ex)
            {
                Console.WriteLine(ex.Message);
                return new Polynomial();
            }
            catch (IndexOutOfRangeException ex)
            {
                Console.WriteLine(ex.Message);
                return new Polynomial();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return new Polynomial();
            }

        }

        public static implicit operator Polynomial(double val)
        {
            return new Polynomial(new double[] { val });
        }

        //Equal()------------------------------
        public override bool Equals(object obj)
        {
            //чи типи однакові 
            if ((obj.GetType() == this.GetType()))
            {
                var other = obj as Polynomial;
                //чи розмір онаковий
                if (other.PolinomKoefs.Length == this.PolinomKoefs.Length)
                {
                    //чи коефіціенти однакові
                    for (int i = 0; i < other.PolinomKoefs.Length; i++)
                    {
                        if (other[i] != this[i])
                        {
                            return false;
                        }
                    }
                    // тоді об'єкти рівні
                    return true;
                }
            }
            return false;
        }
        //to String---------------------------
        public override string ToString()
        {
            string res = "";
            if (polynKoefs[0] != 0)
                res += $"{polynKoefs[0]}";

            for (int i = 1; i < polynKoefs.Length; i++)
            {
                //не виводити нульові коефіціенти полінома
                if (polynKoefs[i] == 0)
                    continue;
                if (polynKoefs[i] >= 0)
                {
                    res += $" +{polynKoefs[i]}x^{i}";
                }
                else
                {
                    res += $" {polynKoefs[i]}x^{i}";
                }

            }
            return res;
        }
        public int CompareTo(Polynomial other)
        {
            if (other == null)
                throw new NotImplementedException();
            int res = 0;
            if (this.PolinomKoefs.Length > other.PolinomKoefs.Length)
            {
                res = 1;
            }
            else if (this.PolinomKoefs.Length < other.PolinomKoefs.Length)
                res = -1;
            return res;
        }

        //Задвання 3 
        //функція магічного квадрату
        public static string MagicalSquare(int n)
        {
            try
            {
                if ((n < 1) || (n % 2 == 0))
                {
                    throw new ArgumentException("Wring Input");
                }

                int[,] matrix = new int[n, n];
                int currentElement = 1;

                int row = n / 2;
                int column = n - 1;
                //встановлення першого елемента і його збільшення
                matrix[row, column] = currentElement++;

                row--;
                column++;
                //поки не встановимо кожен елемнет на позицію
                while (currentElement != n * n + 1)
                {
                    if (row == -1 && column == n)
                    {
                        row = 0;
                        column = n - 2;
                    }
                    else
                    {
                        if (column == n)
                        {
                            column = 0;
                        }
                        if (row == -1)
                        {
                            row = n - 1;
                        }
                    }

                    if (matrix[row, column] != 0)
                    {
                        column = column - 2;
                        row++;
                        continue;
                    }
                    else
                    {
                        matrix[row, column] = currentElement++;
                    }
                    row--;
                    column++;
                }


                string result = "\nSize of magic square = " + matrix.GetLength(0) + "\n";
                result += "Sum of all rows, columns and diagonals = " +
                    matrix.GetLength(0) * (matrix.GetLength(0) * matrix.GetLength(0) + 1) / 2 + "\n\n";
                for (int i = 0; i < matrix.GetLength(0); i++)
                {
                    for (int j = 0; j < matrix.GetLength(1); j++)
                        result += string.Format("{0}\t", matrix[i, j]);
                    result += "\n";
                }
                return result;
            }
            catch (ArgumentException ex)
            {
                return ex.Message;
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }
    }
}
