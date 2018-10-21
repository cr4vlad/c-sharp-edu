using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyOwnMatrix
{
    class Program
    {
        static void Main(string[] args)
        {
            Matrix matrix = new Matrix();
            matrix.WriteMatrix("matrix:");
            matrix[0, 0] = 11;
            matrix[1, 0] = 12;
            matrix[2, 0] = 13;
            matrix.WriteMatrix();
            matrix = matrix + 5;
            matrix.WriteMatrix();
            matrix = matrix - 10;
            matrix.WriteMatrix();
            Matrix matrix2 = new Matrix();
            matrix2.WriteMatrix("matrix2:");
            matrix2 -= matrix;
            matrix2.WriteMatrix();
            int[,] mat = (int[,])matrix;
            matrix2 = mat;
            matrix2.WriteMatrix();
            matrix.Transpose();
            matrix.WriteMatrix("Transpose matrix(first one):");
            Matrix.Transpose(matrix);
            matrix.WriteMatrix();
            Console.WriteLine("Matrices equal?: " + matrix.Equals(matrix2));
            //Console.WriteLine(matrix.ToString());

            Console.ReadKey();
        }
    }

    class Matrix
    {
        private int[,] numbers = new int[,] { { 1, 2, 4 }, { 2, 3, 6 }, { 3, 4, 8 } };
        public int this[int i, int j]
        {
            get
            {
                return numbers[i, j];
            }
            set
            {
                numbers[i, j] = value;
            }
        }

        public static implicit operator Matrix(int[,] nums) // int[,] to matrix
        {
            return new Matrix { numbers = nums };
        }
        public static explicit operator int[,](Matrix matrix) // matrix to int[,] , using (int[,])
        {
            return matrix.numbers;
        }

        public void WriteMatrix() // or ToString
        {
            Console.WriteLine("Matrix:");
            
            int rows = numbers.GetUpperBound(0) + 1; // GetUpperBound(dimension) returns index of the last element in exact dimension
            int columns = numbers.Length / rows;

            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < columns; j++)
                {
                    Console.Write(numbers[i, j] + "\t");
                }
                Console.WriteLine();
            }
        }

        public void WriteMatrix(string title)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine(title);
            Console.ResetColor();

            int rows = numbers.GetUpperBound(0) + 1;
            int columns = numbers.Length / rows;

            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < columns; j++)
                {
                    Console.Write(numbers[i, j] + "\t");
                }
                Console.WriteLine();
            }
        }

        public static Matrix Transpose(Matrix mat)
        {
            Matrix result = new Matrix();
            int matRows = mat.numbers.GetUpperBound(0) + 1;
            int matColumns = mat.numbers.Length / matRows;
            result.numbers = new int[matColumns, matRows];

            for (int i = 0; i < matRows; i++)
            {
                for (int j = 0; j < matColumns; j++)
                {
                    result[j, i] = mat[i, j];
                }
            }

            return result;
        }

        public Matrix Transpose()
        {
            Matrix result = new Matrix();
            int matRows = this.numbers.GetUpperBound(0) + 1;
            int matColumns = this.numbers.Length / matRows;
            result.numbers = new int[matColumns, matRows];

            for (int i = 0; i < matRows; i++)
            {
                for (int j = 0; j < matColumns; j++)
                {
                    result[j, i] = this[i, j];
                }
            }

            return result;
        }

        public static Matrix operator +(Matrix mat, int num)
        {
            Matrix result = new Matrix();
            result.numbers = mat.numbers;
            int rows = result.numbers.GetUpperBound(0) + 1;
            int columns = result.numbers.Length / rows;

            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < columns; j++)
                {
                    result[i, j] = mat[i, j] + num;
                }
            }

            return result;
        }

        public static Matrix operator +(Matrix mat1, Matrix mat2)
        {
            
            Matrix result = new Matrix();
            result.numbers = mat1.numbers;
            int rows1 = mat1.numbers.GetUpperBound(0) + 1;
            int columns1 = mat1.numbers.Length / rows1;
            int rows2 = mat2.numbers.GetUpperBound(0) + 1;
            int columns2 = mat2.numbers.Length / rows2;

            if ((rows1 == rows2) && (columns1 == columns2))
            {
                for (int i = 0; i < rows1; i++)
                {
                    for (int j = 0; j < columns1; j++)
                    {
                        result[i, j] = mat1[i, j] + mat2[i, j];
                    }
                }
            }
            else
            {
                Console.WriteLine("Error #001: Impossible to sum up matrices with different size. First matrix returned");
            }

            return result;
        }

        public static Matrix operator -(Matrix mat, int num)
        {
            Matrix result = new Matrix();
            result.numbers = mat.numbers;
            int rows = result.numbers.GetUpperBound(0) + 1;
            int columns = result.numbers.Length / rows;

            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < columns; j++)
                {
                    result[i, j] = mat[i, j] - num;
                }
            }

            return result;
        }

        public static Matrix operator -(Matrix mat1, Matrix mat2)
        {

            Matrix result = new Matrix();
            result.numbers = mat1.numbers;
            int rows1 = mat1.numbers.GetUpperBound(0) + 1;
            int columns1 = mat1.numbers.Length / rows1;
            int rows2 = mat2.numbers.GetUpperBound(0) + 1;
            int columns2 = mat2.numbers.Length / rows2;

            if ((rows1 == rows2) && (columns1 == columns2))
            {
                for (int i = 0; i < rows1; i++)
                {
                    for (int j = 0; j < columns1; j++)
                    {
                        result[i, j] = mat1[i, j] - mat2[i, j];
                    }
                }
            }
            else
            {
                Console.WriteLine("Error #001: Impossible to sum up matrices with different size. First matrix returned");
            }

            return result;
        }

        public override bool Equals(object obj)
        {
            if (obj.GetType() != this.GetType()) return false;
 
            Matrix matrix2 = (Matrix)obj;
            return (this.numbers == matrix2.numbers);
        }

        /*
        public override string ToString()
        {
            int rows = numbers.GetUpperBound(0) + 1; // GetUpperBound(dimension) returns index of the last element in exact dimension
            int columns = numbers.Length / rows;

            string result = "";

            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < columns; j++)
                {
                    result += numbers[i, j] + "\t";
                }
                result += "\n";
            }
            return result;
        }
        */
    }
}
