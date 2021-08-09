using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace BirthdayGreetings
{
    class MSSQLModel: IModel
    {
        private List<Person> people;
        private const string connectionString = "Data Source=localhost;Initial Catalog=People;Integrated Security=True;Pooling=False";
        public MSSQLModel()
        {
        }
        private Person CreatePerson(IDataRecord record)
        {
            Person person = new();
            person.id = record.GetInt32(0);
            person.firstName = record.GetString(1);
            person.surname = record.GetString(2);
            person.patronymic = record.GetString(3);
            person.birthday = record.GetDateTime(4);
            return person;
        }
        public void Add(Person person)
        {
            string sql = "INSERT dbo.People (FirstName, Surname, Patronymic, Birthday) " +
                $"VALUES ('{person.firstName}', '{person.surname}', '{person.patronymic}', '{person.birthday:d}');";
            ExecuteNonQuery(sql);
        }
        public void Delete(int index)
        {
            int id = people[index].id;
            string sql = $"DELETE FROM dbo.People WHERE Id = {id};";
            ExecuteNonQuery(sql);
        }
        public void Edit(int index, Person person)
        {
            int id = people[index].id;
            string sql = $"UPDATE dbo.People " +
                $"SET FirstName = '{person.firstName}', Surname = '{person.surname}', Patronymic = '{person.patronymic}', Birthday = '{person.birthday:d}' " +
                $"WHERE Id = {id};";
            ExecuteNonQuery(sql);
        }
        private static void ExecuteNonQuery(string sql)
        {
            using (SqlConnection connection = new(connectionString))
            using (SqlCommand sqlCommand = new(sql, connection))
                try
                {
                    connection.Open();
                    sqlCommand.ExecuteNonQuery();
                }
                catch (SqlException e)
                {
                    Console.WriteLine("Ошибка выполнения запроса: " + sql);
                    DisplaySqlErrors(e);
                }
                catch
                {
                    Console.WriteLine("Ошибка выполнения запроса: " + sql);
                }
                finally
                {
                    connection.Close();
                }
        }
        public List<Person> All()
        {
            return SelectFromPeople();
        }
        public List<Person> Nearest(DateTime end)
        {
            int nowDayOfYear = DateTime.Now.DayOfYear;
            int endDayOfYear = end.DayOfYear;
            string query = $"WHERE DATEPART(dayofyear, Birthday) BETWEEN {nowDayOfYear} AND {endDayOfYear}";
            return SelectFromPeople(query);
        }
        private List<Person> SelectFromPeople(string query = "")
        {
            people = new();
            string sql = "SELECT * FROM dbo.People";
            if (!String.IsNullOrEmpty(query))
                sql += " " + query;
            sql += ";";
            using (SqlConnection connection = new(connectionString))
            using (SqlCommand sqlCommand = new(sql, connection))
                try
                {
                    connection.Open();
                    SqlDataReader dataReader = sqlCommand.ExecuteReader();
                    while (dataReader.Read())
                        people.Add(CreatePerson((IDataRecord)dataReader));
                }
                catch (SqlException e)
                {
                    Console.WriteLine("Ошибка выполнения запроса: " + sql);
                    DisplaySqlErrors(e);
                }
                catch
                {
                    Console.WriteLine("Ошибка выполнения запроса: " + sql);
                }
                finally
                {
                    connection.Close();
                }
            return people;
        }
        private static void DisplaySqlErrors(SqlException exception)
        {
            for (int i = 0; i < exception.Errors.Count; i++)
            {
                Console.WriteLine("Index #" + i + "\n" +
                    "Error: " + exception.Errors[i].ToString() + "\n");
            }
        }
        public Person Person(int index)
        {
            return people[index];
        }
    }
}
