using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Spearing.Utilities.Data.FramesCore
{
    public class Column<T> : List<T>, IColumn
    {
        public string Name { get; set; }

        public Type GetSubType() { return typeof(T); }

        public Column(IEnumerable<T> list) : base(list) { }

        /// <summary>
        /// Returns the value at the index as an object
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
        public object Value(int index)
        {
            return this[index];
        }


        Column IColumn.Duplicate(List<int> indexes)
        {
            IEnumerable<T> list = indexes.Select(i => i == -1 ? default(T) : this[i]);
            var c = new Column<T>(list)
            {
                Name = this.Name
            };
            return new Column(c);
        }

        Column IColumn.Duplicate()
        {
            IEnumerable<T> list = this.Select(i => i);
            var c = new Column<T>(list)
            {
                Name = this.Name
            };
            return new Column(c);
        }

        Column IColumn.Duplicate(int count)
        {
            IEnumerable<T> list =
                Enumerable.Range(0, count)
                .SelectMany(x => this.Select(i => i));

            var c = new Column<T>(list)
            {
                Name = this.Name
            };
            return new Column(c);
        }

        Column IColumn.Union(Column column)
        {
            var toMerge = column.Col as Column<T>;
            var merged = this.Union(toMerge);

            var c = new Column<T>(merged)
            {
                Name = this.Name
            };
            return new Column(c);
        }



        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            //sb.AppendLine(this.Name);

            var max = this.Max(value => value == null ? 0 : value.ToString().Length + 3);
            max = Math.Max(max, this.Name.Length + 3);

            sb.AppendLine(this.Name.PadLeft(max));

            this.ForEach(value =>
            {
                var str = value == null ? "".PadLeft(max) : value.ToString().PadLeft(max);
                sb.AppendLine(str);
            });

            return sb.ToString();
        }


        #region Addition Column Operators

        // Addition

        public static Column<T> operator +(Column<T> first, Column<T> second)
        {
            var values = first.Select((value, i) =>
            {
                dynamic val1 = value;
                dynamic val2 = second[i];
                return (T)(val1 + val2);
            }
            ).ToList();
            return new Column<T>(values) { };
        }



        public static Column<T> operator +(Column<T> first, IEnumerable<T> second)
        {
            var values = first.Select((value, i) =>
            {
                dynamic val1 = value;
                dynamic val2 = second.ElementAt(i);
                return (T)(val1 + val2);
            }
            ).ToList();

            return new Column<T>(values);
        }


        public static Column<T> operator +(Column<T> first, T second)
        {
            var values =
                first.Select(value =>
                {
                    dynamic val1 = value;
                    dynamic val2 = second;
                    return (T)(val1 + val2);
                });
            return new Column<T>(values);
        }

        #endregion



        #region Multiplication Column Operators

        public static Column<T> operator *(Column<T> first, Column<T> second)
        {
            var values = first.Select((value, i) =>
            {
                dynamic val1 = value;
                dynamic val2 = second[i];
                return (T)(val1 * val2);
            }
            ).ToList();
            return new Column<T>(values) { };
        }



        public static Column<T> operator *(Column<T> first, IEnumerable<T> second)
        {
            var values = first.Select((value, i) =>
            {
                dynamic val1 = value;
                dynamic val2 = second.ElementAt(i);
                return (T)(val1 * val2);
            }
            ).ToList();

            return new Column<T>(values);
        }

        public static Column<T> operator *(Column<T> first, T second)
        {
            var values =
                first.Select(value =>
                {
                    dynamic val1 = value;
                    dynamic val2 = second;
                    return (T)(val1 * val2);
                });
            return new Column<T>(values);
        }

        #endregion



        #region Division Column Operators

        public static Column<T> operator /(Column<T> first, Column<T> second)
        {
            var values = first.Select((value, i) =>
            {
                dynamic val1 = value;
                dynamic val2 = second[i];
                return (T)(val1 / val2);
            }
            ).ToList();
            return new Column<T>(values) { };
        }

        public static Column<T> operator /(Column<T> first, IEnumerable<T> second)
        {
            var values = first.Select((value, i) =>
            {
                dynamic val1 = value;
                dynamic val2 = second.ElementAt(i);
                return (T)(val1 / val2);
            }
            ).ToList();

            return new Column<T>(values);
        }

        public static Column<T> operator /(Column<T> first, T second)
        {
            var values =
                first.Select(value =>
                {
                    dynamic val1 = value;
                    dynamic val2 = second;
                    return (T)(val1 / val2);
                });
            return new Column<T>(values);
        }

        #endregion



        #region Subtraction Column Operators



        public static Column<T> operator -(Column<T> first, Column<T> second)
        {
            var values = first.Select((value, i) =>
            {
                dynamic val1 = value;
                dynamic val2 = second[i];
                return (T)(val1 - val2);
            }
            ).ToList();
            return new Column<T>(values) { };
        }

        public static Column<T> operator -(Column<T> first, IEnumerable<T> second)
        {
            var values = first.Select((value, i) =>
            {
                dynamic val1 = value;
                dynamic val2 = second.ElementAt(i);
                return (T)(val1 - val2);
            }
            ).ToList();

            return new Column<T>(values);
        }

        public static Column<T> operator -(Column<T> first, T second)
        {
            var values =
                first.Select(value =>
                {
                    dynamic val1 = value;
                    dynamic val2 = second;
                    return (T)(val1 - val2);
                });
            return new Column<T>(values);
        }

        #endregion


    }

}
