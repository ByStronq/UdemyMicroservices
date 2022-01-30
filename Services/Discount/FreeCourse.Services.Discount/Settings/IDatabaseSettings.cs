namespace FreeCourse.Services.Discount.Settings
{
    public interface IDatabaseSettings
    {
        public string Host { get; set; }
        public int PortNumber { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string DatabaseName { get; set; }
        public bool IntegratedSecurity { get; set; }
        public bool Pooling { get; set; }
    }
}
