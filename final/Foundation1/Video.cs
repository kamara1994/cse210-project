using System.Collections.Generic;
using System.Text;

namespace Foundation1
{
    public class Video
    {
        public string Title { get; }
        public string Author { get; }
        public int LengthSeconds { get; }

        private readonly List<Comment> _comments = new();

        public Video(string title, string author, int lengthSeconds)
        {
            Title = title;
            Author = author;
            LengthSeconds = lengthSeconds;
        }

        public void AddComment(Comment c) => _comments.Add(c);

        public string ToDisplayString()
        {
            var sb = new StringBuilder();
            sb.AppendLine($"Title: {Title}  | Author: {Author}  | Length: {LengthSeconds}s");
            sb.AppendLine($"Comments ({_comments.Count}):");
            foreach (var c in _comments)
                sb.AppendLine($"  - {c}");
            return sb.ToString();
        }
    }
}
