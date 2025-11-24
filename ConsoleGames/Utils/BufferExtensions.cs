using System;
using System.Collections.Generic;
using System.Text;

namespace SnakeGame.Utils;

internal static class BufferExtensions
{
    extension(int num)
    {
        // Implementation of citoa()
        public char[] Itoa(char[] str, int @base)
        {
            int i = 0;
            bool isNegative = false;

            /* Handle 0 explicitly, otherwise empty string is
            * printed for 0 */
            if (num == 0)
            {
                str[i++] = '0';
                str[i] = '\0';
                return str;
            }

            // In standard itoa(), negative numbers are handled
            // only with base 10. Otherwise numbers are
            // considered unsigned.
            if (num < 0 && @base == 10)
            {
                isNegative = true;
                num = -num;
            }

            // Process individual digits
            while (num != 0)
            {
                int rem = num % @base;
                str[i++] = (char)((rem > 9) ? rem - 10 + 'a' : rem + '0');
                num = num / @base;
            }

            // If number is negative, append '-'
            if (isNegative)
                str[i++] = '-';

            str[i] = '\0'; // Append string terminator

            // Reverse the string
            Reverse(str, i);

            return str;
        }

    }

    extension(char[] buffer)
    {
        public void FillClearingBuffer(char[] clearingBuffer)
        {
            int idx = 0;
            while (idx < buffer.Length)
            {
                if (buffer[idx] is default(char)) // is null terminator
                    break;
                clearingBuffer[idx] = (char)32;
                idx++;
            }
        }
    }

    private static void Reverse(char[] str, int length)
    {
        int start = 0;
        int end = length - 1;
        while (start < end)
        {
            char temp = str[start];
            str[start] = str[end];
            str[end] = temp;
            end--;
            start++;
        }
    }
}
