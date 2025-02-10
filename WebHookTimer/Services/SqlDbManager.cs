using Dapper;
using System.Data.SqlClient;
using WebHookTimer.Interfaces;
using WebHookTimer.Models;

namespace WebHookTimer.Services
{
    public class SqlDbManager : IDbManager
    {
        private readonly string _dbConnectionString;
        public SqlDbManager(IConfiguration config)
        {
            _dbConnectionString = config.GetConnectionString("myDbConn");
            InitTable();
        }

        public void InitTable()
        {
            using (var connection = new SqlConnection(_dbConnectionString))
            {
                connection.Open();

                string query = @"
                IF NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME = 'TimerInfo')
                CREATE TABLE TimerInfo (
                    Id NVARCHAR(50) PRIMARY KEY,
                    TimeCreated DATETIME NOT NULL,
                    Hours BIGINT NOT NULL,
                    Minutes BIGINT NOT NULL,
                    Seconds BIGINT NOT NULL,
                    ExpiredTime DATETIME NOT NULL,
                    WebhookUrl NVARCHAR(500) NOT NULL,
                    Status NVARCHAR(50) NOT NULL
                )";

                connection.Execute(query);

                Console.WriteLine("Table created successfully (if it didn't exist).");
            }
        }

        public async Task<TimerInfo> GetTimerInfoById(string id)
        {
            string query = $"SELECT Id, ExpiredTime FROM TimerInfo WHERE LTRIM(RTRIM(Id)) ='{id}';";
            var timerInfoRecords = await GetDataByQuery(query);

            return timerInfoRecords[0];
        }

        public async Task<List<TimerInfo>> GetExpiredTimers()
        {
            string transactionQuery = $"BEGIN TRANSACTION;" +
                $" UPDATE TimerInfo SET Status = 'Finished' " +
                $"OUTPUT inserted. * " +
                $"WHERE ExpiredTime <= '{DateTime.Now}' AND Status ='Started'; " +
                $"COMMIT";
            var timerInfoRecords = await GetDataByQuery(transactionQuery);

            return timerInfoRecords;
        }

        public async Task<string> SetNewTimerRecord(TimerInfo newTimerRecord)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(_dbConnectionString))
                {
                    await connection.OpenAsync();

                    string query = $"INSERT INTO TimerInfo VALUES ('{newTimerRecord.id}', '{newTimerRecord.timeCreated.ToString("yyyy-MM-dd HH:mm:ss")}'," +
                        $" {newTimerRecord.hours}, {newTimerRecord.minutes}, {newTimerRecord.seconds}, '{newTimerRecord.expiredTime.ToString("yyyy-MM-dd HH:mm:ss")}'" +
                        $", '{newTimerRecord.webhookUrl}', '{newTimerRecord.status}');";

                    var newId = await connection.ExecuteScalarAsync<string>(query, newTimerRecord);
                    return newId;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return "";
            }
        }

        public async Task<List<TimerInfo>> GetDataByQuery(string query)
        {
            try
            {
                var timerInfoRecords = new List<TimerInfo>();
                using (SqlConnection connection = new SqlConnection(_dbConnectionString))
                {
                    await connection.OpenAsync();
                    timerInfoRecords = (await connection.QueryAsync<TimerInfo>(query)).AsList();
                }

                return timerInfoRecords;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return new List<TimerInfo>();
            }
        }
    }
}
