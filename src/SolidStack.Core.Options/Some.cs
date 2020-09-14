namespace SolidStack.Core.Options
{
    public class Some<T> : Option<T>
    {
        public Some(T content) => 
            Content = content;

        private T Content { get; }

        public static implicit operator T(Some<T> value) =>
            value.Content;
    }
}