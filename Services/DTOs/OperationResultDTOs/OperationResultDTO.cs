namespace Services.DTOs.OperationResultDTOs
{
    public class OperationResultDTO<T>
    {
        public bool Success { get; set; }
        public string Message { get; set; }
        public T Data { get; set; }
    }
}
