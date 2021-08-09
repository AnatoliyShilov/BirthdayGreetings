namespace BirthdayGreetings
{
    class Program
    {
        static void Main()
        {
            Model model = new("1.txt");
            model.Load();
            Menu menu = new(model);
            menu.Show();
            model.Save();
        }
    }
}
