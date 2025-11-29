using System.Runtime.CompilerServices;


namespace ConsoleGames.Core.Render.Utils;

public static class BufferExtensions
{
    extension(char[] buffer)
    {
        public int LengthUntilTerminator()
        {
            int length = 0;
            while (length < buffer.Length && buffer[length] != '\0')
            {
                length++;
            }
            return length;
        }
    }

    extension(Array)
    {
        public static char[] NewClearingBuffer(int size)
        {
            var clearingBuffer = new char[size];
            for (int i = 0; i < size; i++)
            {
                clearingBuffer[i] = (char)32; // ASCII space
            }
            return clearingBuffer;
        }
    }

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


    extension<T>(T enumValue)
        where T : struct, Enum
    {
        public int ToInt() => Unsafe_As(enumValue);

    }

    private static int Unsafe_As<TEnum>(TEnum enumValue)
        where TEnum : struct, Enum
    {
        return Unsafe.As<TEnum, int>(ref enumValue);
    }

}
