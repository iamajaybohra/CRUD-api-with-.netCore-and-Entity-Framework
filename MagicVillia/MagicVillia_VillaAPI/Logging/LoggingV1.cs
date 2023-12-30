namespace MagicVillia_VillaAPI.Logging
{
    public class LoggingV1:ILogging
    {
        public void Log(string message, string type) {
            if(type == "error")
            {
                Console.WriteLine("ERROR - "+ message);
            }
            else
            {
                Console.WriteLine(message);
            }
        }
    }
}
