using System.Reflection;

namespace ReqestDispatcher.Mapper;

public class Mapper: IMapper
{
    public TDestination Map<TSource, TDestination>(TSource source)
        where TDestination : new()
    {
        if (source == null) throw new ArgumentNullException(nameof(source));
        var destination = new TDestination();

        var sourceProps = typeof(TSource).GetProperties(BindingFlags.Public | BindingFlags.Instance);
        var destProps = typeof(TDestination).GetProperties(BindingFlags.Public | BindingFlags.Instance);

        foreach (var destProp in destProps)
        {
            var sourceProp = sourceProps.FirstOrDefault(x => x.Name == destProp.Name &&
                                                             x.PropertyType == destProp.PropertyType &&
                                                             x.CanRead && destProp.CanWrite);
            if (sourceProp != null)
            {
                var value = sourceProp.GetValue(source);
                destProp.SetValue(destination, value);
            }
        }

        return destination;
    }
    
}