using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Oracle.ManagedDataAccess.Client;
using Oracle.ManagedDataAccess.Types;


namespace lab5.Code
{
    public class SimplexMethod
    {
        private const int colNum = 4;
        private const int rowNum = 4;
        private double[][] matrix = new double[rowNum][];
        private Dictionary<int, int> positionDictionary = new Dictionary<int, int>(); 
        
        public SimplexMethod()
        {
            matrix[0] = new double[] { 1.6, 1.2, 1.4, 0.8 };
            matrix[1] = new double[] { 200,  80, 280, 240 };
            matrix[2] = new double[] {  10,   5,   5, 100 };
            matrix[3] = new double[] {   0,   3,   4,   5 };

            positionDictionary.Add(1, -1);
            positionDictionary.Add(2, -1);
            positionDictionary.Add(3, -1);
        }

        public void SetMatrix(double[][] m)
        {
            for(int i = 0; i < rowNum; i++)
                for(int j = 0; j < colNum; j++)
                {
                    matrix[i][j] = m[i][j];
                }
        }

        public double[] DoTheStuff()
        {
            int generalRow = -1;
            int generalCol = -1;
            do
            {
                generalCol = FindGeneralCol();
                if (generalCol == -1)
                    break;

                generalRow = FindGeneralRow(generalCol);

                positionDictionary[generalCol] = generalRow;
                RecalculateElements(generalRow, generalCol);
            } while (true);
            double x1 = matrix[positionDictionary[1]][0];
            double x2 = matrix[positionDictionary[2]][0];
            double x3 = matrix[positionDictionary[3]][0];
            

            return new double[] {x1,x2,x3};
        }

        private void RecalculateElements(int generalRow,int generalCol)
        {
            for (int i = 0; i < rowNum; i++)
            {
                if (i == generalRow)
                    continue;
                for (int j = 0; j < colNum; j++)
                {                  

                    if (j != generalCol)
                        matrix[i][j] = matrix[i][j]
                            + (-matrix[i][generalCol] / matrix[generalRow][generalCol]) * matrix[generalRow][j];
                }
            }

            for (int i = 0; i < rowNum; i++)
            {
                if (i != generalRow)
                    matrix[i][generalCol] = (-1 * matrix[i][generalCol]) / matrix[generalRow][generalCol];
            }

            for (int j = 0; j < colNum; j++)
            {
                if (j != generalCol)
                    matrix[generalRow][j] = matrix[generalRow][j] / matrix[generalRow][generalCol];
            }
            matrix[generalRow][generalCol] = 1 / matrix[generalRow][generalCol];
        }

        private bool CheckForLegalSolution()
        {
            bool res = true;
            return res;
        }

        private int FindGeneralRow(int genCol)
        {

            double min = 0;
            int curRow = 0;
            for(int i = 0; i < rowNum - 1;i++)
            {
                if(matrix[i][0] > 0  && matrix[i][genCol] > 0)
                {
                    curRow = i;
                    min = matrix[i][0] / matrix[i][genCol];
                    break;
                }
            }

            if (min == 0)
                return -1;

            for(int i =0; i < rowNum - 1; i++)
            {
                if (matrix[i][0] > 0 && matrix[i][genCol] > 0)
                {
                    if (matrix[i][0] / matrix[i][genCol] < min)
                    {
                        min = matrix[i][0] / matrix[i][genCol];
                        curRow = i;
                    }
                }
            }

            return curRow;
        }
        
        private int FindGeneralCol()
        {
            double max = 0;
            int curCol = -1;
            for(int i = 1; i < colNum; i++)
            {
                if (matrix[3][i] > max)
                {
                    max = matrix[3][i];
                    curCol = i;
                }
            }

            return curCol;
        }   
       
    }

    public class ImprovementOfMyDBSkills
    {
        public static bool DoConnection()
        {
            string conn = "DATA SOURCE=localhost:1521/orcl2;USER ID=SLAVKOUSER";
            OracleConnection _connection = new OracleConnection(conn);
            OracleCommand command = _connection.CreateCommand();
            string commandText = "insert into users (@id,@username,@email,@passwordhash)"
                + "values ";
            command.CommandText = commandText;


            return true;
        }
    }
}
