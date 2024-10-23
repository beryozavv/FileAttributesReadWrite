using System;
using System.Runtime.InteropServices;
using System.Text;

namespace FileAttributesReadWrite
{
    internal class LinuxFileAttributesService : IFileAttributesService
    {
        // P/Invoke для вызова системных функций Linux
        [DllImport("libc", SetLastError = true)]
        private static extern int setxattr(string path, string name, byte[] value, ulong size, int flags);

        [DllImport("libc", SetLastError = true)]
        private static extern int getxattr(string path, string name, byte[] value, ulong size);

        [DllImport("libc", SetLastError = true)]
        private static extern int removexattr(string path, string name);


        /// <summary>
        /// Запись в расширенные атрибуты
        /// </summary>
        /// <param name="filePath">Путь к файлу</param>
        /// <param name="key">Имя атрибута</param>
        /// <param name="data">Данные для записи в xattr</param>
        public void Write(string filePath, string key, string data)
        {
            byte[] valueBytes = Encoding.UTF8.GetBytes(data);
            var xAttrKey = $"user.{key}"; // обязательная приставка user.
            int result = setxattr(filePath, xAttrKey, valueBytes, (ulong)valueBytes.Length, 0);

            if (result != 0)
            {
                Console.WriteLine($"Ошибка записи атрибута: {Marshal.GetLastWin32Error()}");
            }
            else
            {
                Console.WriteLine("Атрибут успешно записан.");
            }

            Console.WriteLine("Data has successfully written to ADS {0} of file {1}", xAttrKey, filePath);
        }

        /// <summary>
        /// Чтение данных из расширенных атрибутов
        /// </summary>
        /// <param name="filePath">Путь к файлу</param>
        /// <param name="key">Имя атрибута</param>
        /// <returns>Данные из xattr</returns>
        public string? Read(string filePath, string key)
        {
            byte[] buffer = new byte[1024]; // Буфер для чтения значения атрибута
            var xAttrKey = $"user.{key}"; // обязательная приставка user.
            int result = getxattr(filePath, xAttrKey, buffer, (ulong)buffer.Length);

            if (result < 0)
            {
                Console.WriteLine($"Ошибка чтения атрибута: {Marshal.GetLastWin32Error()}");
                return null;
            }

            return Encoding.UTF8.GetString(buffer, 0, result);
        }
    }
}