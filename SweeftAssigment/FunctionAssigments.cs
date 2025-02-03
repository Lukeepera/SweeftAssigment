using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SweeftAssigment
{
    internal class Function_assigments
    {
        // 1st
        bool sPalindrome(string text)
        {

            int left = 0;
            int right = text.Length - 1;
            while (left < right)
            {
                if (!char.IsLetterOrDigit(text[left]))
                {
                    left++;
                    continue;
                }
                if (!char.IsLetterOrDigit(text[right]))
                {
                    right--; 
                    continue;
                }

                if (char.ToLower(text[left]) != char.ToLower(text[right]))
                {
                    return false;
                }

                left++;
                right--;
            }
            return true;
        }

        //2nd
        int Minsplit(int amount)
        {
            int[] coins = { 50, 20, 10, 5, 1 }; //მონეტები დალაგებულია უდიდესიდან უმცირესისკენ კლებადობით.
            int coinCount = 0;

            foreach (int coin in coins)
            {
                coinCount += amount / coin;
                amount %= coin;
            }

            return coinCount;


        }

        //3rd
        int NotContains(int[] array)
        {
            Array.Sort(array);

            for (int i = 1; i <= array.Length; i++)
            {
                if (Array.IndexOf(array, i) == -1)
                {
                    return i;
                }
            }

            return array.Length + 1;
        }

        //4th
        static bool IsProperly(string sequence)
        {
            int openMatch = 0;
            int closeMatch = 0;

            foreach (char symb in sequence)
            {
                if (symb == '(')
                {

                    if (closeMatch > 0)
                    {
                        closeMatch--;
                    }
                    else
                    {
                        openMatch++;
                    }
                }
                else if (symb == ')')
                {
                    if (openMatch > 0)
                    {
                        openMatch--;
                    }
                    else
                    {
                        closeMatch++;
                    }
                }
            }

            return openMatch == 0 && closeMatch == 0;
        }

        //5th
        int CountVariants(int stairCount)
        {
            if (stairCount == 0 || stairCount == 1)
            {

                return 1;

            }

            return CountVariants(stairCount - 1) + CountVariants(stairCount - 2);

        }
    }
}
