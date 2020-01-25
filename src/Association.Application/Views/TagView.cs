namespace Association.Application.Views
{
    public class TagView
    {
        public TagView(string username, int number)
        {
            Username = username;
            Number = number;
        }

        public string Username { get; }
        public int Number { get; }
    }
}