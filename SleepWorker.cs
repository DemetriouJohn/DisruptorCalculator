namespace DisruptorCalculator;

using Disruptor;

public class SleepEventHandler : IEventHandler<CalculatorEvent>
{
    public void OnEvent(CalculatorEvent data, long sequence, bool endOfBatch) => Thread.Sleep(2000);
}