namespace Diploma.Core
{
    public class ControllerResult
    {
        public bool IsSuccess { get; set; }

        public string Message { get; set; }

        public int Status { get; set; }
    }

    public class ControllerResult<TValue> : ControllerResult
    {
        public TValue Value { get; set; }
    }
}
