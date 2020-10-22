using System;
using System.Collections.Generic;

namespace Spearing.Utilities.Data.FramesCore
{
    public class Column : IColumn
    {
        internal IColumn Col { get; }

        public int Count { get { return this.Col.Count; } }
        public string Name
        {
            get { return this.Col.Name; }
            set { this.Col.Name = value; }
        }

        public Type GetSubType() { return this.Col.GetSubType(); }

        public Column DuplicateThis(List<int> indexes) { return this.Col.Duplicate(indexes); }
        internal Column DuplicateThis(int count) { return this.Col.Duplicate(count); }
        internal Column UnionThis(Column column) { return this.Col.Union(column); }

        Column IColumn.Duplicate(List<int> indexes) { return this.Col.Duplicate(indexes); }
        Column IColumn.Duplicate() { return this.Col.Duplicate(); }
        Column IColumn.Duplicate(int count) { return this.Col.Duplicate(count); }
        Column IColumn.Union(Column column) { return this.Col.Union(column); }

        public object Value(int index) { return this.Col.Value(index); }


        public static implicit operator Column(List<bool> col) { return new Column(new Column<bool>(col)); }
        public static implicit operator Column(List<DateTime> col) { return new Column(new Column<DateTime>(col)); }
        public static implicit operator Column(List<byte> col) { return new Column(new Column<byte>(col)); }
        public static implicit operator Column(List<sbyte> col) { return new Column(new Column<sbyte>(col)); }
        public static implicit operator Column(List<char> col) { return new Column(new Column<char>(col)); }
        public static implicit operator Column(List<decimal> col) { return new Column(new Column<decimal>(col)); }
        public static implicit operator Column(List<double> col) { return new Column(new Column<double>(col)); }
        public static implicit operator Column(List<float> col) { return new Column(new Column<float>(col)); }
        public static implicit operator Column(List<int> col) { return new Column(new Column<int>(col)); }
        public static implicit operator Column(List<uint> col) { return new Column(new Column<uint>(col)); }
        public static implicit operator Column(List<long> col) { return new Column(new Column<long>(col)); }
        public static implicit operator Column(List<ulong> col) { return new Column(new Column<ulong>(col)); }
        public static implicit operator Column(List<object> col) { return new Column(new Column<object>(col)); }
        public static implicit operator Column(List<short> col) { return new Column(new Column<short>(col)); }
        public static implicit operator Column(List<ushort> col) { return new Column(new Column<ushort>(col)); }
        public static implicit operator Column(List<string> col) { return new Column(new Column<string>(col)); }
        public static implicit operator Column(List<bool?> col) { return new Column(new Column<bool?>(col)); }
        public static implicit operator Column(List<DateTime?> col) { return new Column(new Column<DateTime?>(col)); }
        public static implicit operator Column(List<byte?> col) { return new Column(new Column<byte?>(col)); }
        public static implicit operator Column(List<sbyte?> col) { return new Column(new Column<sbyte?>(col)); }
        public static implicit operator Column(List<char?> col) { return new Column(new Column<char?>(col)); }
        public static implicit operator Column(List<decimal?> col) { return new Column(new Column<decimal?>(col)); }
        public static implicit operator Column(List<double?> col) { return new Column(new Column<double?>(col)); }
        public static implicit operator Column(List<float?> col) { return new Column(new Column<float?>(col)); }
        public static implicit operator Column(List<int?> col) { return new Column(new Column<int?>(col)); }
        public static implicit operator Column(List<uint?> col) { return new Column(new Column<uint?>(col)); }
        public static implicit operator Column(List<long?> col) { return new Column(new Column<long?>(col)); }
        public static implicit operator Column(List<ulong?> col) { return new Column(new Column<ulong?>(col)); }
        public static implicit operator Column(List<short?> col) { return new Column(new Column<short?>(col)); }
        public static implicit operator Column(List<ushort?> col) { return new Column(new Column<ushort?>(col)); }


        public static implicit operator Column(bool[] col) { return new Column(new Column<bool>(col)); }
        public static implicit operator Column(DateTime[] col) { return new Column(new Column<DateTime>(col)); }
        public static implicit operator Column(byte[] col) { return new Column(new Column<byte>(col)); }
        public static implicit operator Column(sbyte[] col) { return new Column(new Column<sbyte>(col)); }
        public static implicit operator Column(char[] col) { return new Column(new Column<char>(col)); }
        public static implicit operator Column(decimal[] col) { return new Column(new Column<decimal>(col)); }
        public static implicit operator Column(double[] col) { return new Column(new Column<double>(col)); }
        public static implicit operator Column(float[] col) { return new Column(new Column<float>(col)); }
        public static implicit operator Column(int[] col) { return new Column(new Column<int>(col)); }
        public static implicit operator Column(uint[] col) { return new Column(new Column<uint>(col)); }
        public static implicit operator Column(long[] col) { return new Column(new Column<long>(col)); }
        public static implicit operator Column(ulong[] col) { return new Column(new Column<ulong>(col)); }
        public static implicit operator Column(object[] col) { return new Column(new Column<object>(col)); }
        public static implicit operator Column(short[] col) { return new Column(new Column<short>(col)); }
        public static implicit operator Column(ushort[] col) { return new Column(new Column<ushort>(col)); }
        public static implicit operator Column(string[] col) { return new Column(new Column<string>(col)); }
        public static implicit operator Column(bool?[] col) { return new Column(new Column<bool?>(col)); }
        public static implicit operator Column(DateTime?[] col) { return new Column(new Column<DateTime?>(col)); }
        public static implicit operator Column(byte?[] col) { return new Column(new Column<byte?>(col)); }
        public static implicit operator Column(sbyte?[] col) { return new Column(new Column<sbyte?>(col)); }
        public static implicit operator Column(char?[] col) { return new Column(new Column<char?>(col)); }
        public static implicit operator Column(decimal?[] col) { return new Column(new Column<decimal?>(col)); }
        public static implicit operator Column(double?[] col) { return new Column(new Column<double?>(col)); }
        public static implicit operator Column(float?[] col) { return new Column(new Column<float?>(col)); }
        public static implicit operator Column(int?[] col) { return new Column(new Column<int?>(col)); }
        public static implicit operator Column(uint?[] col) { return new Column(new Column<uint?>(col)); }
        public static implicit operator Column(long?[] col) { return new Column(new Column<long?>(col)); }
        public static implicit operator Column(ulong?[] col) { return new Column(new Column<ulong?>(col)); }
        public static implicit operator Column(short?[] col) { return new Column(new Column<short?>(col)); }
        public static implicit operator Column(ushort?[] col) { return new Column(new Column<ushort?>(col)); }


        //public static implicit operator Column(Column<T> col)
        //{

        //}


        internal Column(IColumn col)
        {
            this.Col = col;
        }

        public override string ToString()
        {
            return this.Col.ToString();
        }
    }

}
