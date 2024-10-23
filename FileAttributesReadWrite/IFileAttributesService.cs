namespace FileAttributesReadWrite
{
    public interface IFileAttributesService
    {
        /// <summary>
        /// Запись в альтернативный поток данных
        /// </summary>
        /// <param name="filePath">Путь к файлу</param>
        /// <param name="key">Имя потока</param>
        /// <param name="data">Данные для записи в альтернативный поток данных</param>
        void Write(string filePath, string key, string data);

        /// <summary>
        /// Чтение данных из альтернативного потока данных
        /// </summary>
        /// <param name="filePath">Путь к файлу</param>
        /// <param name="key">Имя потока</param>
        /// <returns>Данные из альтернативного потока</returns>
        string? Read(string filePath, string key);
    }
}