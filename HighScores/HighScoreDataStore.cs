using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using SQLite;

namespace HighScores
{
    class HighScoreDataStore
    {
        readonly SQLiteAsyncConnection _database;
        public HighScoreDataStore()
        {
            string dbPath = System.Environment.GetFolderPath(System.Environment.SpecialFolder.ApplicationData);
            dbPath = Path.Combine(dbPath, "highscoredata.sqlite");
            _database = new SQLiteAsyncConnection(dbPath);
            _database.CreateTableAsync<HighScore>().Wait();
        }

        public async Task<bool> AddAsync(HighScore HighScore)
        {
            await _database.InsertAsync(HighScore);

            return await Task.FromResult(true);
        }

        public async Task<bool> DeleteAsync(string id)
        {
            await _database.DeleteAsync(id);

            return await Task.FromResult(true);
        }

        public async Task<HighScore> GetAsync(string id)
        {
            return await _database.GetAsync<HighScore>(id);
        }

        public async Task<List<HighScore>> GetAsync(bool forceRefresh = false)
        {
            return await _database.Table<HighScore>().ToListAsync();
        }

        public async Task<int> UpdateAsync(HighScore HighScore)
        {
            return await _database.UpdateAsync(HighScore);
        }
    }
}