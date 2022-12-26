using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DenseIndex
{
    public class SharSearch
    {

        private static int UniformBinarySearch(long[] arr, long key, int i, int omega, ref int count)
        {
            count = 0;

            while (omega > 0)
            {
                count++;

                if (i < arr.Length && i >= 0)
                {
                    if (arr[i] == key)
                    {
                        return i;
                    }
                    else
                    {
                        if (arr[i] > key)
                        {
                            i = i - (omega / 2 + 1);
                            if (omega > 1)
                            {
                                omega /= 2;
                            }

                        }
                        else
                        {
                            i = i + (omega / 2 + 1);
                            if (omega > 1)
                            {
                                omega /= 2;
                            }
                        }
                    }
                }
                else
                {
                    if (i < 0)
                    {
                        i = i + (omega / 2 + 1);
                        if (omega > 1)
                        {
                            omega /= 2;
                        }
                    }
                    else
                    {
                        i = i - (omega / 2 + 1);
                        if (omega > 1)
                        {
                            omega /= 2;
                        }
                    }


                }



            }

            if (arr[i] == key)
            {
                return i;
            }

            return -1;
        }

        public static int Search(long[] arr, long key, ref int count)
        {
            int k = Convert.ToInt32(Math.Truncate(Math.Log2(arr.Length)));
            long keyI = arr[Convert.ToInt32(Math.Pow(2, k)) - 1];

            if (key == keyI)
            {
                return Convert.ToInt32(Math.Pow(2, k)) - 1;
            }
            else
            {
                if (key < keyI)
                {
                    return UniformBinarySearch(arr, key, Convert.ToInt32(Math.Pow(2, k) - 0), 2 * Convert.ToInt32(Math.Pow(2, (k - 1))), ref count); // i: -1
                }
                else
                {
                    int l = Convert.ToInt32(Math.Log2(arr.Length - Math.Pow(2, k) + 1));
                    return UniformBinarySearch(arr, key, Convert.ToInt32(arr.Length + 1 - Math.Pow(2, l) - 0), 2 * Convert.ToInt32(Math.Pow(2, l - 1)), ref count); // i:-1
                }
            }

        }

    }
}
