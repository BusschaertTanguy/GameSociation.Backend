using Common.Application.Views;

namespace Association.Application.Views
{
    public class TagView : IProjection
    {
        public string Username { get; set; }
        public int Number { get; set; }
    }
}
