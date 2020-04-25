namespace ConsoleApp1
{
    internal class ProgramSB
    {
        private void Main(string[] args)
        {
            string one = GetTest();
            var two = GetTest();
        }

        private string GetTest()
        {
            return "abc";
        }
    }
}
