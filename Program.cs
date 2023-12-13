var filePath = "D:\\Coding\\CodinGame\\TestCases.txt";
var testCases = File.ReadAllText(filePath).Split(Environment.NewLine + Environment.NewLine);

//Console.WriteLine("6,4 -> " + Solution.GCD(6, 4));
//Console.WriteLine("45,15 -> " + Solution.GCD(45, 15));
//Console.WriteLine("8,0 -> " + Solution.GCD(8, 0));
//Console.WriteLine("0,8 -> " + Solution.GCD(0, 8));



while (true)
{
    int testCaseIndex = Convert.ToInt32(Console.ReadLine()) - 1;

    Console.WriteLine("Enter testcase number...");

    Console.WriteLine($"\n**** Start: TestCase {testCaseIndex} ****\n\n");

    Console.SetIn(new StringReader(testCases[testCaseIndex]));

    Solution.Main(new string[0]);

    Console.WriteLine($"\n\n**** End: TestCase {testCaseIndex} ****\n");
}
