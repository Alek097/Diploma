namespace Diploma.Core.ConfigureModels
{
    public class App
    {
        private string domain;

        public string Domain
        {
            get
            {
                if (domain != null && this.domain[this.domain.Length - 1] != '/')
                {
                    return $"{this.domain}/";
                }
                else
                {
                    return this.domain;
                }
            }

            set => domain = value;
        }

        public string ConnectionString { get; set; }
    }
}
