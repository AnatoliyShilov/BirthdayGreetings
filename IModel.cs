using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace BirthdayGreetings
{
    struct Person
    {
        [JsonInclude]
        public string firstName;
        [JsonInclude]
        public string surname;
        [JsonInclude]
        public string patronymic;
        [JsonInclude]
        public DateTime birthday;
        public int id;
        public override string ToString()
        {
            // TODO удалить пробелы в конце строки
            return $"ФИО: {surname} {firstName} {patronymic}\nДень рождения: " + birthday.ToString("d");
        }
        public bool Equals(Person person)
        {
            return person.firstName == firstName &&
                person.surname == surname &&
                person.patronymic == patronymic &&
                person.birthday == birthday;
        }
    }
    interface IModel
    {
        public void Add(Person person);
        public void Delete(int index);
        public void Edit(int index, Person person);
        public List<Person> All();
        public List<Person> Nearest(DateTime end);
        public Person Person(int index);
    }
}
