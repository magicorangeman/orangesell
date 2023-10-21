using System;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;

namespace GaussAlgorithm;

public class Solver
{
    public double[] Solve(double[][] matrix, double[] freeMembers)
    {
        var result = new double[matrix[0].Length].Select(x => 0d).ToArray();
        var indexes = new int[matrix[0].Length].Select(i => -1).ToArray();

        if (matrix.Length != freeMembers.Length)
            throw new FormatException();
        for (int i = 0; i < matrix.Length - 1; i++)
            if (matrix[i].Length != matrix[i + 1].Length)
                throw new FormatException();

        var array = MatrixExtend(matrix, freeMembers);
        for (int i = 0; i < array.Length && i < indexes.Length; i++)
            for (int j = 0; j < array[i].Length - 1; j++)
            {
                if (Math.Round(array[i][j], 3) != 0)
                {
                    indexes[i] = j;
                    for (int k = 0; k < array.Length; k++)
                        if (k != i)
                            array[k] = DoMatrixSubtract(array[k], MatrixLineMultiply(array[i], array[k][j] / array[i][j]));
                    break;
                }
            }

        for (int i = 0; i < array.Length || i < indexes.Length; i++)
        {
            if (i < indexes.Length && indexes[i] >= 0) 
                result[indexes[i]] = Math.Round(array[i][array[i].Length - 1] / array[i][indexes[i]], 8);
            else if (i < array.Length && array[i][array[i].Length - 1] != 0)
                throw new NoSolutionException("Нет решений");
        }
        return result;
	}

    private static double[][] MatrixExtend(double[][] matrix, double[] members)
    {
        var array = new double[matrix.Length][];
        for (int i = 0; i < matrix.Length; i++)
        {
            array[i] = new double[matrix[i].Length + 1];
            for (int j = 0; j < matrix[i].Length; j++)
                array[i][j] = matrix[i][j];
            array[i][matrix[i].Length] = members[i];
        }
        return array;
    }

    private static double[] MatrixLineMultiply(double[] row, double multiplier) => 
        row.Select(x => x * multiplier).ToArray();

    public static double[] DoMatrixSubtract(double[] a, double[] b)
    {
        var array = new double[a.Length];
        for (int i = 0; i < a.Length; i++)
            array[i] = a[i] - b[i];
        return array;
    }
}