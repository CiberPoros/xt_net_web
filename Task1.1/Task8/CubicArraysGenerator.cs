using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task8
{
    internal class CubicArraysGenerator
    {
        public static T[,,] Generate<T>(int degree0, int degree1, int degree2, Func<T> generator)
        {
            T[,,] res = new T[degree0, degree1, degree2];

            for (int i = 0; i < degree0; i++)
                for (int j = 0; j < degree1; j++)
                    for (int k = 0; k < degree2; k++)
                        res[i, j, k] = generator();

            return res;
        }
    }
}
