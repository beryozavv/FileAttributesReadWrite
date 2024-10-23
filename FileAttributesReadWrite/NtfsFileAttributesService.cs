using System;
using System.IO;

namespace FileAttributesReadWrite
{
    internal class NtfsFileAttributesService : IFileAttributesService
    {
        /// <summary>
        /// Запись в альтернативный поток данных
        /// </summary>
        /// <param name="filePath">Путь к файлу</param>
        /// <param name="key">Имя потока</param>
        /// <param name="data">Данные для записи в альтернативный поток данных</param>
        public void Write(string filePath, string key, string data)
        {
            // Имя альтернативного потока
            var adsName = $"{filePath}:{key}";

            // Запись данных в ADS
            using (var fs = new FileStream(adsName, FileMode.Create, FileAccess.Write))
            using (var writer = new StreamWriter(fs))
            {
                writer.Write(data);
            }

            Console.WriteLine("Data has successfully written to ADS {0} of file {1}", key, filePath);
        }

        /// <summary>
        /// Чтение данных из альтернативного потока данных
        /// </summary>
        /// <param name="filePath">Путь к файлу</param>
        /// <param name="key">Имя потока</param>
        /// <returns>Данные из альтернативного потока</returns>
        public string? Read(string filePath, string key)
        {
            // Имя альтернативного потока
            var adsName = $"{filePath}:{key}";

            // Чтение данных из ADS
            if (File.Exists(adsName))
            {
                using (var fs = new FileStream(adsName, FileMode.Open, FileAccess.Read))
                using (var reader = new StreamReader(fs))
                {
                    var data = reader.ReadToEnd();
                    //Console.WriteLine("From ADS {0} of file {1} was read data {2} : ", key, filePath, data);

                    return data;
                }
            }
            else
            {
                Console.WriteLine("ADS doesn't exists");
                return null;
            }
        }
    }
}