using Serilog;

namespace CreditCardApi.Logger{

    public static class Logger{

        public static Serilog.Core.Logger createLogger(){
            return new LoggerConfiguration()
                .WriteTo.Console()
                .CreateLogger();
        }

        public static Serilog.Core.Logger createFileLogger(){
            return new LoggerConfiguration()
                .WriteTo.File("log.txt", rollingInterval: RollingInterval.Day)
                .CreateLogger();    
        } 

        public static Serilog.Core.Logger createSQLite(){
            var sqlPath = Environment.CurrentDirectory + @"/WebApp.db";
            return new LoggerConfiguration()
                .WriteTo.SQLite(sqliteDbPath:sqlPath,tableName:"Log",batchSize:1)
                .CreateLogger();
        }

    }
}