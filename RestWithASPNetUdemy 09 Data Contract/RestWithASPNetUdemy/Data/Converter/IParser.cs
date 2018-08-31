using System.Collections.Generic;

namespace RestWithASPNetUdemy.Data.Converter
{
    public interface IParser<Origin, Destine>
    {
        Destine Parse(Origin origin);

        List<Destine> Parse(List<Origin> origin);
    }
}