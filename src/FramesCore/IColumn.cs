using System;
using System.Collections.Generic;

namespace Spearing.Utilities.Data.FramesCore
{
    public interface IColumn
    {
        string Name { get; set; }
        int Count { get; }
        object Value(int index);
        Column Duplicate(List<int> indexes);
        Column Duplicate();
        Column Duplicate(int count);
        Column Union(Column column);


        Type GetSubType();
    }

}
