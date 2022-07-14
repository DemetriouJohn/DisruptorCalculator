namespace DisruptorCalculator;

public class CalculatorEvent
{
    public uint OperationId { get; set; }
    public int Val1 { get; set; }
    public int Val2 { get; set; }
    public CalculatorOperation Operation { get; set; }
}