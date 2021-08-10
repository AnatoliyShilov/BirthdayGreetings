using System;
using System.Collections.Generic;

namespace BirthdayGreetings
{
    class Menu
    {
        private IModel model;
        public Menu(IModel model)
        {
            this.model = model;
        }
        public void Show()
        {
            int index = 1;
            while (index != 0)
            {
                Console.WriteLine("Поздравлятор");
                Nearest();
                Console.WriteLine("1 - Показать все");
                Console.WriteLine("2 - Показать ближайшие");
                Console.WriteLine("3 - Добавить день рождения");
                Console.WriteLine("4 - Удалить день рождения");
                Console.WriteLine("5 - Редактировать день рождения");
                Console.WriteLine("0 - Выйти");
                Console.Write("Введите номер команды: ");
                index = Convert.ToInt32(Console.ReadLine());
                Console.Clear();
                ExecCommand(index);
                Console.Write("Нажмите любую клавишу...");
                Console.ReadKey();
                Console.Clear();
            }
        }
        private void ExecCommand(int index)
        {
            switch (index)
            {
                case 1:
                    All();
                    break;
                case 2:
                    Nearest();
                    break;
                case 3:
                    Add();
                    break;
                case 4:
                    Delete();
                    break;
                case 5:
                    Edit();
                    break;
                case 0: break;
                default:
                    Console.WriteLine("Неизвестная опция");
                    break;
            }
        }
        private void All()
        {
            Table("Все дни рождения", model.All());
        }
        private void Nearest()
        {
            Table("Ближайшие дни рождения", model.Nearest(DateTime.Now.AddMonths(1)));
        }
        private static void Table(string title, List<Person> people)
        {
            if (people.Count == 0)
            {
                Console.WriteLine(title + ": Пусто");
                return;
            }
            Console.WriteLine(title);
            Console.WriteLine("================");
            int index = 1;
            foreach (Person person in people)
            {
                Console.WriteLine(index.ToString() + ". " + person);
                Console.WriteLine("================");
                index++;
            }
        }
        private void Add()
        {
            Console.WriteLine("Добавление нового дня рождения");
            model.Add(InputPerson());
            Console.WriteLine("Добавление завершено");
        }
        private Person InputPerson(Person? defVal = null)
        {
            Person person = new();
            person.surname = Input("Фамилия: ", defVal?.surname);
            person.firstName = Input("Имя: ", defVal?.firstName);
            person.patronymic = Input("Отчество: ", defVal?.patronymic);
            person.birthday = Convert.ToDateTime(Input("День рождения: ", defVal?.birthday.ToString("d")));
            return person;
        }
        private static string Input(string title, string? defVal)
        {
            string temp;
            while (true)
            {
                Console.Write(title);
                if (!String.IsNullOrEmpty(defVal))
                    Console.Write($"{defVal} -> ");
                temp = Console.ReadLine();
                if (String.IsNullOrEmpty(temp))
                    if (!String.IsNullOrEmpty(defVal))
                    {
                        temp = defVal;
                        break;
                    }
                    else
                    {
                        Console.Clear();
                        Console.WriteLine("Пустое значение недопустимо!");
                    }
                else break;
            }
            return temp;
        }
        private void Delete()
        {
            All();
            Console.Write("Введите номер удаляемой записи: ");
            int index = Convert.ToInt32(Console.ReadLine()) - 1;
            model.Delete(index);
            Console.WriteLine("Удаление завершено");
        }
        private void Edit()
        {
            All();
            Console.Write("Ведите номер редактируемой записи: ");
            int index = Convert.ToInt32(Console.ReadLine()) - 1;
            Console.Clear();
            Console.WriteLine("Введите новые данные (Enter - без изменений)");
            Person person = model.Person(index);
            Person editedPerson = InputPerson(person);
            if (person.Equals(editedPerson)) return;
            model.Edit(index, editedPerson);
            Console.WriteLine("Запись обновлена");
        }
    }
}
