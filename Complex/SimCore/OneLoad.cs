namespace SimCore
{
    public class OneLoad
    {
        public OneLoad(string file, string model, string sdk)
        {
            File = file;
            Model = model;
            Sdk = sdk;
        }

        public string File { get; init; }
        public string Model { get; init; }
        public string Sdk { get; init; }
    }
}