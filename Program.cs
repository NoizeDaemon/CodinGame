var standardInputStream = new StreamReader(Console.OpenStandardInput());

var filePath = "D:\\Coding\\CodinGame\\TestCases.txt";
var testCases = File.ReadAllText(filePath).Split(Environment.NewLine + Environment.NewLine);

//Console.WriteLine("6,4 -> " + Solution.GCD(6, 4));
//Console.WriteLine("45,15 -> " + Solution.GCD(45, 15));
//Console.WriteLine("8,0 -> " + Solution.GCD(8, 0));
//Console.WriteLine("0,8 -> " + Solution.GCD(0, 8));



while (true)
{
    Console.WriteLine("Enter testcase number...");
    int testCase = Convert.ToInt32(Console.ReadLine());


    Console.WriteLine($"\n**** Start: TestCase {testCase} ****\n\n");

    var testCaseInputStream = new StringReader(testCases[testCase - 1]);

    Console.SetIn(testCaseInputStream);

    Solution.Main(new string[0]);

    Console.WriteLine($"\n\n**** End: TestCase {testCase} ****\n");

    Console.SetIn(standardInputStream);
}
