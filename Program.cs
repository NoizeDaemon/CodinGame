using BetterConsoles.Tables;
using BetterConsoles.Tables.Builders;
using BetterConsoles.Tables.Configuration;
using BetterConsoles.Tables.Models;
using System;
using System.Drawing;
using System.IO;
using System.Linq;

//var filePath = "D:\\Coding\\CodinGame\\TestCases.txt";
var filePath = "C:\\Users\\dario.frei\\Documents\\GitHub\\CodinGame\\TestCases.txt";

var inputOutputSeparator = "OUTPUT";

var testCases = File.ReadAllText(filePath)
                    .Split(Environment.NewLine + Environment.NewLine)
                    .Select((s, i) => new TestCase(i + 1, s, inputOutputSeparator))
                    .ToArray();

//Console.WriteLine("6,4 -> " + Solution.GCD(6, 4));
//Console.WriteLine("45,15 -> " + Solution.GCD(45, 15));
//Console.WriteLine("8,0 -> " + Solution.GCD(8, 0));
//Console.WriteLine("0,8 -> " + Solution.GCD(0, 8));



while (true)
{
    Console.WriteLine("Enter testcase number or hit ENTER to play all cases...");

    if (int.TryParse(Console.ReadLine(), out int testCase))
    {
        RunTestCase(testCases[testCase - 1]);
    }
    else
    {
        foreach (var t in testCases) RunTestCase(t);
        break;
    }
}

Console.ReadKey();
Environment.Exit(0);


static void RunTestCase(TestCase testCase)
{
    var standardInputStream = new StreamReader(Console.OpenStandardInput());
    var standardOutputStream = new StreamWriter(Console.OpenStandardOutput());
    standardOutputStream.AutoFlush = true;

    //var header = $"**** TESTCASE {testCase.ID} ****";

    //Console.WriteLine(new string('*', header.Length));
    //Console.WriteLine($"**** TESTCASE {testCase.ID} ****");
    //Console.WriteLine(new string('*', header.Length));

    Console.WriteLine($"  TESTCASE {testCase.ID}");

    var testCaseInputStream = new StringReader(string.Join(Environment.NewLine, testCase.Input));
    var testCaseOutputStream = new StringWriter();

    Console.SetIn(testCaseInputStream);
    Console.SetOut(testCaseOutputStream);

    Solution.Main();
    var output = testCaseOutputStream.ToString().Trim();

    Console.SetIn(standardInputStream);
    Console.SetOut(standardOutputStream);

    PrintTable(testCase, output);

    Console.WriteLine();
}

static void PrintTable(TestCase testCase, string output)
{
    bool showExpectedOutput = testCase.ExpectedOutput is not null && testCase.ExpectedOutput.Equals(output);
    bool isOutputCorrect = output.Equals(testCase.ExpectedOutput);


    Table table = new TableBuilder()
        .AddColumn("Input")
            .HeaderFormat()
                .ForegroundColor(Color.AliceBlue)
        .AddColumn("Output")
            .HeaderFormat()
                .ForegroundColor(isOutputCorrect ? Color.Green : Color.Red)
        .AddColumn("Expected")
            .HeaderFormat()
                .ForegroundColor(Color.AliceBlue)   
        .Build();

    table.Config = TableConfig.Unicode();

    var inputLines = testCase.Input.Split(Environment.NewLine);
    var outputLines = output.Split(Environment.NewLine);
    var expectedLines = testCase.ExpectedOutput.Split(Environment.NewLine);

    var maxLines = Math.Max(Math.Max(inputLines.Length, expectedLines.Length), outputLines.Length);

    for (int i = 0;  i < maxLines; i++)
    {
        var rowValues = new string[]
        {
            i < inputLines.Length ? inputLines[i] : string.Empty,
            i < outputLines.Length ? outputLines[i] : string.Empty,
            i < expectedLines.Length ? expectedLines[i] : string.Empty
        };

        table.AddRow(rowValues);
    }

    Console.WriteLine(table.ToString());
}

class TestCase
{
    public TestCase(int id, string input, string separator)
    {
        var parts = input.Split(separator);
        ID = id;
        Input = parts[0].Trim();
        ExpectedOutput = parts.Length > 1 ? parts[1].Trim() : null;
    }

    public int ID { get; }
    public string Input { get; }
    public string ExpectedOutput { get; }
}