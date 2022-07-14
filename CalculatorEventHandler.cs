using Disruptor;
using System.Diagnostics;

namespace DisruptorCalculator;

public class CalculatorWorkHandler : IWorkHandler<CalculatorEvent>
{
    public void OnEvent(CalculatorEvent evt)
    {
        var ss = new Stopwatch();
        ss.Start();
        string code, result;
        switch (evt.Operation)
        {
            case CalculatorOperation.Add:
                code = "Success";
                var addition = evt.Val1 + evt.Val2;
                result = addition.ToString();
                break;
            case CalculatorOperation.Sub:
                code = "Success";
                var subsctraction = evt.Val1 - evt.Val2;
                result = subsctraction.ToString();
                break;
            default:
                code = "OperationNotFound";
                result = string.Empty;
                break;
        }

        ss.Stop();
        Console.WriteLine($"Operation Id: {evt.OperationId}, Code:{code}, Result: {result} ");
        Console.WriteLine($"Operation Id: {evt.OperationId}, Ticks: {ss.ElapsedTicks} Ms: {ss.ElapsedMilliseconds}");
    }
}