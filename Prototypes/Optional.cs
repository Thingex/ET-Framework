using System;
namespace ET_Framework.Prototypes
{
    public class Optional<T> where T : class
    {
        private T? _instance;

        private Optional(T? value)
        {
            _instance = value;
        }

        public static Optional<T> OfNullable(T? value)
        {
            return new Optional<T>(value);
        }

        public static Optional<T> Of(T value)
        {
            if (value is not null)
                return new Optional<T>(value);
            else
                throw new NullReferenceException();
        }

        public bool IsPresent() => _instance == null;

        public void IfPresent(Action<T> callback)
        {
            if (_instance is not null)
                callback.Invoke(_instance);
        }

        public void IfPresentOrElse(Action<T> callback, Action emptyCallback)
        {
            if (_instance is not null)
                callback.Invoke(_instance);
            else
                emptyCallback.Invoke();
        }

        public T Get()
        {
            if (_instance is not null) return _instance;
            else
                throw new Exception("No such element was found in current optional.");
        }

        public T OrElse(T other) => _instance is null ? other : _instance;

        public T OrElseGet(Func<T> others) => _instance is null ? others.Invoke() : _instance;

        public T OrElseThrows<E>(Func<E> exce) where E : Exception, new()
        {
            if (_instance is not null)
                return _instance;
            else
                throw new E();
        }

        public Optional<T> FilteByPredicate(Predicate<T> condition) => _instance is not null && condition(_instance) ? this : new(null);
    }
}

