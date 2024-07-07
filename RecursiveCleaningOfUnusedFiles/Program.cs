using RecursiveCleaningOfUnusedFiles;

internal class Program
{
  private static bool p_isCorrect;
  private static async Task Main(string[] args)
  {
    while (!p_isCorrect)
    {
      Console.Write($"Введите URL папки: ");

      string? path = Console.ReadLine();

      if (string.IsNullOrWhiteSpace(path))
      {
        Console.WriteLine($"URL некорректный");
      }
      else if (!Directory.Exists(path))
      {
        Console.WriteLine($"Указанного каталога не существует!");
      }
      else
      {
        var volumeBefore = RecursiveReader.GetDirVolume(path, 0);
        Console.WriteLine($"Папка весит:{volumeBefore} байт до очистки");
        Console.WriteLine("Очищаем...");
        await Task.Delay(1000);
        var volumeAfter = Cleaner.Clean(path, 0, 0);
        Console.WriteLine($"Папка весит:{volumeAfter.Volume} байт после очистки");
        Console.WriteLine($"Удалено {volumeAfter.DeletedFiles} файлов");
        Console.WriteLine($"Освобождено: {volumeBefore - volumeAfter.Volume} места");
        p_isCorrect = true;
      }
    }
  }
}