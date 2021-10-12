using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SigmaTest
{

    class Matrix2D
    {
        private int[,] matrix;
        //необхідна попередня ініціалізація
        //щоб парвильно виділити пам'ять у властивостях
        private int rowCount = 1;
        private int columnCount = 1;


        public Matrix2D(int row = 1, int column = 1)
        {
            try
            {
                RowCount = row;
                ColumnCount = column;
            }
            catch (ArgumentException ex)
            {
                Console.WriteLine(ex.Message);
            }
            catch (IndexOutOfRangeException ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
        public int this[int i, int k]
        {
            get
            {
                if ((i < 0) || (i >= RowCount)
                    || (k < 0) || (k >= ColumnCount))
                {
                    throw new IndexOutOfRangeException("Index out of range");
                }

                return matrix[i, k];
            }
            set
            {
                if ((i < 0) || (i >= RowCount)
                    || (k < 0) || (k >= ColumnCount))
                {
                    throw new IndexOutOfRangeException("Index out of range");
                }
                matrix[i, k] = value;
            }
        }

        public void InitMatr()
        {
            Random random = new Random();
            for (int i = 0; i < RowCount; i++)
            {
                for (int k = 0; k < ColumnCount; k++)
                {
                    matrix[i, k] = random.Next(-20, 20);
                }
            }
        }

        public int RowCount
        {
            get
            {
                return this.rowCount;
            }
            set
            {
                if (value > 0)
                {
                    this.rowCount = value;
                    matrix = new int[this.rowCount, this.columnCount];
                }

                else
                    throw new ArgumentException("Value <1");
            }
        }

        public int ColumnCount
        {
            get
            {
                return this.columnCount;
            }
            set
            {
                if (value > 0)
                {
                    this.columnCount = value;
                    matrix = new int[this.rowCount, this.columnCount];
                }

                else
                    throw new ArgumentException("Value <1");
            }
        }
        public override string ToString()
        {
            string res = "";
            for (int i = 0; i < RowCount; i++)
            {
                for (int k = 0; k < ColumnCount; k++)
                {
                    res += string.Format("{0}\t", matrix[i, k]);
                }
                res += "\n";
            }
            return res;
        }

        //енумератор для матриці
        public IEnumerator<double> GetEnumerator()
        {
            for (int i = matrix.GetLength(0) - 1; i > -1; i--)
            {
                for (int j = matrix.GetLength(1) - 1; j > -1; j--)
                {
                    yield return matrix[i, j];
                }
            }
        }
    }
}
