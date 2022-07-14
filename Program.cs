using Disruptor.Dsl;
using DisruptorCalculator;

var disruptor = new Disruptor<CalculatorEvent>(() => new CalculatorEvent(), ringBufferSize: 128);
disruptor
    .HandleEventsWith(new SleepEventHandler())
    .ThenHandleEventsWithWorkerPool(new CalculatorWorkHandler());
disruptor.Start();

while (true)
{
    Console.WriteLine("Expected input format:");
    Console.WriteLine("OperationId,Operation,Value 1,Value 2 (no spaces)");
    Console.WriteLine("Write 'Exit' (case sensitive) in order to finish");
    var input = Console.ReadLine();
    if (string.IsNullOrWhiteSpace(input))
    {
        continue;
    }

    if (input == "Exit")
    {
        break;
    }

    var values = input.Split(',');

    if (values.Length != 4)
    {
        Console.WriteLine("Invalid Input");
        continue;
    }

    if (!uint.TryParse(values[0], out var operationId))
    {
        Console.WriteLine("Invalid Input");
        continue;
    }

    if (!int.TryParse(values[2], out var val1))
    {
        Console.WriteLine("Invalid Input");
        continue;
    }

    if (!int.TryParse(values[3], out var val2))
    {
        Console.WriteLine("Invalid Input");
        continue;
    }

    if (!Enum.TryParse(typeof(CalculatorOperation), values[1], out var calculatorOperation))
    {
        Console.WriteLine("Invalid Input");
        continue;
    }

    var operation = (CalculatorOperation)calculatorOperation!;

    using (var scope = disruptor.PublishEvent())
    {
        var data = scope.Event();
        data.OperationId = operationId;
        data.Operation = operation;
        data.Val1 = val1;
        data.Val2 = val2;
    }
}