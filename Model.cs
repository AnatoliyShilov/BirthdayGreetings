using System;
using System.Collections.Generic;
using System.Text.Json;
using System.IO;
//using System.Text.Json.Serialization;

namespace BirthdayGreetings
{
    class Model: IModel
    {
        private List<Person> data;
        private string filename;
        public List<Person> Data { get => data; }
        public Model(string filename)
        {
            this.filename = filename;
        }
        public void Load()
        {
            if (!File.Exists(filename))
                using (StreamWriter sw = File.CreateText(filename))
                {
                    sw.WriteLine("[]");
                }
            string json = File.ReadAllText(filename);
            data = JsonSerializer.Deserialize<List<Person>>(json);
        }
        public void Save()
        {
            string json = JsonSerializer.Serialize(data);
            File.WriteAllText(filename, json);
        }
        public void Add(Person person)
        {
            data.Add(person);
        }
        public void Delete(int index)
        {
            data.RemoveAt(index);
        }
        public void Edit(int index, Person person)
        {
            data[index] = person;
        }
        public List<Person> All()
        {
            return data;
        }
        public List<Person> Nearest(DateTime end)
        {
            DateTime now = new(1, DateTime.Now.Month, DateTime.Now.Day);
            end = new(1, end.Month, end.Day);
            return data.FindAll((Person person) =>
            {
                DateTime birthday = new(1, person.birthday.Month, person.birthday.Day);
                return birthday >= now && birthday <= end;
            });
        }
        public Person Person(int index)
        {
            return data[index];
        }
    }
}
