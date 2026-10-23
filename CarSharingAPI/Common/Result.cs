namespace CarSharingAPI.Common
{
    public class Result
    {
        private Result(bool isSuccess, Error error)
        {
            IsSuccess = isSuccess;
            Error = error ?? throw new ArgumentNullException(nameof(error));
        }

        public bool IsSuccess { get; }
        public bool IsFailure => !IsSuccess;
        public Error Error { get; }

        public static Result Success() => new(true, Error.None);

        public static Result Failure(Error error)
        {
            if (error == null || error == Error.None)
            {
                throw new ArgumentException("You must provide a valid error.", nameof(error));
            }

            return new(false, error);
        }
    }

    public sealed class Result<T>
    {
        private Result(T value, bool isSuccess, Error error)
        {
            IsSuccess = isSuccess;
            Error = error ?? throw new ArgumentNullException(nameof(error));
            Value = value;
        }

        public bool IsSuccess { get; }
        public bool IsFailure => !IsSuccess;
        public Error Error { get; }
        public T Value { get; }
        public static Result<T> Succes(T value)
        {
            if (value == null)
            {
                throw new ArgumentNullException(nameof(value), "Succes value cannot be null.");
            }

            return new(value, true, Error.None);
        }
        public static Result<T> Failure(Error error)
        {
            if (error == null || error == Error.None)
            {
                throw new ArgumentNullException("You must provide a valid error.", nameof (error));
            }

            return new(default!, false, error);
        }
    }
}
