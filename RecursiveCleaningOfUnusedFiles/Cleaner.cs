namespace RecursiveCleaningOfUnusedFiles
{
  internal class Cleaner
  {
    public static (long Volume, long DeletedFiles) Clean(string _url, long _volume, long _deletedFiles)
    {
      try
      {
        var dirInfo = new DirectoryInfo(_url);

        var files = dirInfo.GetFiles();

        if (files != null)
        {
          foreach (var file in files)
          {
            if ((DateTime.Now - file.LastWriteTime) > (DateTime.Now - DateTime.Now.AddMinutes(-30)))
            {
              Console.WriteLine($"Файл {file.Name} использовался более 30 минут назад --> удаляем");
              _deletedFiles++;
              file.Delete();
            }
            else
            {
              Console.WriteLine($"Файл {file.Name} использовался менее 30 минут назад --> пусть живет");
              _volume += file.Length;
            }
          }
          var dirs = dirInfo.GetDirectories();

          if (dirs != null)
          {
            foreach (var dir in dirs)
            {
              (_volume, _deletedFiles) = Clean(dir.FullName, _volume, _deletedFiles);
            }
          }
        }
      }
      catch (Exception ex)
      {
        Console.WriteLine(ex.ToString());
      }
      return (_volume, _deletedFiles);
    }
  }
}
