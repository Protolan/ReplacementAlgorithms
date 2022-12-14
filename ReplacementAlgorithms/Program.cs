using ReplacementAlgorithms;

var framesCount = 8;
var pages = new [] { 9, 1, 8, 1, 17, 15, 16, 18, 8, 8, 17, 5, 12, 20, 13, 8, 11, 13, 19, 8, 13, 17, 2, 10, 12, 18, 19, 17, 12, 5, 4 };
//var pages = new [] { 2, 3, 2, 1, 5, 2, 4, 5, 3, 2, 5, 2};
var logger = new PageLogger();
var pageExecutor = new PageExecutor(framesCount, pages);
pageExecutor.Execute(PageExecutor.Algorithm.Optimal, logger);
var optimalCount = logger.Print();
logger = new PageLogger();
pageExecutor = new PageExecutor(framesCount, pages);
pageExecutor.Execute(PageExecutor.Algorithm.MostUnused, logger);
logger.Print(optimalCount);
logger = new PageLogger();
pageExecutor = new PageExecutor(framesCount, pages);
pageExecutor.Execute(PageExecutor.Algorithm.FirstInFirstOut, logger);
logger.Print(optimalCount);