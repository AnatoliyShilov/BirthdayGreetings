namespace BirthdayGreetings
{
    class Program
    {
        static void Main()
        {
            //UseModel();
            UseMSSQLModel();
        }
        static void UseModel()
        {
            Model model = new("1.txt");
            model.Load();
            Menu menu = new(model);
            menu.Show();
            model.Save();
        }
        static void UseMSSQLModel()
        {
            MSSQLModel model = new();
            Menu menu = new(model);
            menu.Show();
        }
    }
}
