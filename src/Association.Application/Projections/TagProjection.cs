using Common.Application.Projections;

namespace Association.Application.Projections
{
    public class TagProjection : IProjection
    {
        public string Username { get; set; }
        public int Number { get; set; }
    }
}