namespace TestTaskRosa.Models
{
    public class Request
    {
        public Guid Id { get; set; }

        public string EmployeeName { get; set; } = string.Empty;

        public string CertificateType { get; set; } = string.Empty;

        public int Copies { get; set; }

        public string Reason { get; set; } = string.Empty;

        public RequestStatus Status { get; set; }

        public DateTime CreatedAt { get; set; }
    }
}
