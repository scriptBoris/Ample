using System;
using System.Collections.Generic;
using System.Text;

namespace Ample.Core
{
    public static class ArrayExtended
    {
        /// <summary>
        /// Получает массив путем копирования указзаного диапазона
        /// </summary>
        /// <param name="self"></param>
        /// <param name="start">Начало, где 0 - начало массива</param>
        /// <param name="length">Количество байтов которые нужно скопировать</param>
        /// <returns></returns>
        public static byte[] GetBytesByRange(this byte[] self, int start, int length)
        {
            if (length == 0) return null;

            byte[] res = new byte[length];

            for (int i=0; i<length; i++)
            {
                res[i] = self[start + i];
            }

            return res;
        }
    }
}
