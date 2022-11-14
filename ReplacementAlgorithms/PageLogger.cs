namespace ReplacementAlgorithms;

public class PageLogger
{
    private static readonly string DividingLine = new('-', 50);
    private struct LogData
    {
        public int[] pages;
        public bool interrupt;

        public LogData(int[] pages, bool interrupt)
        {
            this.pages = pages;
            this.interrupt = interrupt;
        }
    }
    private int interruptsCount = 0;
    private readonly List<LogData> _logs = new();
    
    public void AddPage(int[] pages, bool interrupt)
    {
        if (interrupt) interruptsCount++;
        _logs.Add(new LogData(pages.ToArray(), interrupt));
    }
    

    public int Print(int optimalInterruptCount = -1)
    {
      Console.WriteLine(DividingLine);
      foreach (var log in _logs)
      {
          foreach (var page in log.pages)
          {
              var print = page == 0 ? "" : page.ToString();
              Console.Write($"{print}\t");
          }
          if(log.interrupt) Console.WriteLine("F");
          else Console.WriteLine();
      }
      Console.WriteLine(DividingLine);
      Console.WriteLine($"Кол-во прерываний: {interruptsCount}");
      if (optimalInterruptCount != -1)
          Console.WriteLine($"Отношение к оптимальному: {(float)interruptsCount / optimalInterruptCount}");
      Console.WriteLine(DividingLine);
      return interruptsCount;
    }
}