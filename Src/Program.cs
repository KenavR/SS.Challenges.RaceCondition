using SS.Challenges.RaceCondition;

var appArgs = ProgramArgs.CreateFromArgsArray(args);

var runner = new TestRunner();
await runner.Run(appArgs);
