# Basic Releases for Win und Linux x64
## Build yourself
To build the project for a different system run 

```bash
dotnet build SS.Challenges.RaceCondition.csproj -c Release --runtime <RID> --self-contained true
```

The availabled RIDs can be found [here](https://learn.microsoft.com/en-us/dotnet/core/rid-catalog#known-rids).
`--self-contained true` adds the .NET Core runtime to the build output.

## Run on Windows
```bash
racecondition.exe balance=1000 transactions=15 logging=runs solution=none
```

**See other arguments in main README**

## Run on Windows
```bash
./racecondition balance=1000 transactions=15 logging=runs solution=none
```

**See other arguments in main README**