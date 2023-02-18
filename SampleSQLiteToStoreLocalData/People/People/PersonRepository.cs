using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SQLite;
using People.Models;

namespace People
{
    public class PersonRepository
    {
        string _dbPath;

        public string StatusMessage { get; set; }

        // TODO: Add variable for the SQLite connection
        private static SQLiteConnection conn;

        private void Init()
        {
            // TODO: Add code to initialize the repository         
            if (conn is not null)
                return;

            conn = new SQLiteConnection(_dbPath);
            conn.CreateTable<Person>();
        }

        public PersonRepository(string dbPath)
        {
            _dbPath = dbPath;                        
        }

        public void AddNewPerson(string name)
        {
            ArgumentNullException.ThrowIfNull(name, nameof(name));

            int result = 0;

            try
            {
                // TODO: Call Init()
                Init();

                // TODO: Insert the new person into the database
                result = conn.Insert(new Person { Name = name });

                StatusMessage = string.Format("{0} record(s) added (Name: {1})", result, name);
            }
            catch (Exception ex)
            {
                StatusMessage = string.Format("Failed to add {0}. Error: {1}", name, ex.Message);
            }

        }

        public List<Person> GetAllPeople()
        {
            // TODO: Init then retrieve a list of Person objects from the database into a list
            try
            {
                Init();
                return conn.Table<Person>().ToList();
            }
            catch (Exception ex)
            {
                StatusMessage = string.Format("Failed to retrieve data. {0}", ex.Message);
            }

            return new List<Person>();
        }
    }
}
