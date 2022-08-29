namespace CoreEngine.Core;

public interface IMetricView
{
    void Subscribe(IMetricSource source);
}