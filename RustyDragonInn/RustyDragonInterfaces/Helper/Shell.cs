using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RustyDragonBasesAndInterfaces.Helper
{
    /// <summary>
    /// Shell draws a table on the screen showing the list of items which is passed to it.
    /// </summary>
    public sealed class Shell
    {
        private List<int> _columnLengths;
        private string _format;
        private int _maxRowLength;
        private string _columnHeaders;
        private int _longestLine;
        private string[] _headerColumns;
        public IList<object> Columns { get; set; }
        public IList<object[]> Rows { get; set; }

        public Shell(params string[] columns)
        {
            Columns = new List<object>(columns);
            Rows = new List<object[]>();
        }

        public Shell AddColumn(IEnumerable<string> names)
        {
            foreach (var name in names)
                Columns.Add(name);

            return this;
        }

        public Shell AddRow(params object[] values)
        {
            if (values == null)
                throw new ArgumentNullException(nameof(values));

            if (!Columns.Any())
                throw new Exception("Please set the columns first");

            if (Columns.Count != values.Length)
                throw new Exception(
                    $"The number columns in the row ({Columns.Count}) does not match the values ({values.Length}");
            Rows.Add(values);
            return this;
        }

        public static Shell From<T>(IEnumerable<T> values)
        {
            var shell = new Shell();

            var columns = typeof(T).GetProperties().Select(x => x.Name).ToArray();
            shell.AddColumn(columns);

            foreach (var propertyValues in values.Select(value => columns.Select(column => typeof(T).GetProperty(column).GetValue(value, null))))
                shell.AddRow(propertyValues.ToArray());

            return shell;
        }

        private static object[] ToShort(object[] values)
        {
            var shortenValues = new object[values.Length];
            foreach (var value in values)
            {
                DateTime result;
                var isDateTime = DateTime.TryParse(value?.ToString().Replace(".", ""), out result);
                var newValue = isDateTime ? result.ToShortDateString() : value?.ToString().Trim();
                var index = Array.IndexOf(values, value);
                shortenValues[index] = newValue;
            }
            return shortenValues;
        }

        public Shell AddHeader(string[] headerColumns)
        {
            _headerColumns = (string[])headerColumns.Clone();
            return this;
        }

        private string DrawHeader()
        {
            if (_headerColumns == null)
            {
                return string.Empty;
            }
            var builder = new StringBuilder();
            // create the divider
            var spliter = " " + string.Join("", Enumerable.Repeat("-", _longestLine - 1)) + " " + Environment.NewLine;
            builder.Append(spliter);
            builder.Append("|");
            for (var i = 0; i < _headerColumns.Length; i += 2)
            {
                var spaceString = string.Join("",
                    Enumerable.Repeat(" ", i * Convert.ToInt32((_longestLine - 1) / _headerColumns.Length))) + " ";

                builder.Append(spaceString);
                var tempString = $"{_headerColumns[i]}:{_headerColumns[i + 1]}";
                builder.Append(tempString);
            }
            builder.Append("|" + Environment.NewLine);
            return builder.ToString();
        }

        private void Calculations()
        {
            // find the longest column by searching each row
            _columnLengths = Columns
                .Select((t, i) => Rows.Select(x => x[i])
                    .Union(Columns)
                    .Where(x => x != null)
                    .Select(x => x.ToString().Length).Max())
                    .ToList();

            // create the string format with padding
            _format = Enumerable.Range(0, Columns.Count)
               .Select(i => " | {" + i + ", -" + _columnLengths[i] + " }")
               .Aggregate((s, a) => s + a) + " |";

            // find the longest formatted line
            _maxRowLength = Math.Max(0, Rows.Any() ? Rows.Max(row => string.Format(_format, row).Length) : 0);
            _columnHeaders = string.Format(_format, Columns.ToArray());

            // longest line is greater of formatted columnHeader and longest row
            _longestLine = Math.Max(_maxRowLength, _columnHeaders.Length);
        }

        private new string ToString()
        {
            var builder = new StringBuilder();

            var results = new List<string>();

            // add each row
            Array.ForEach(Rows.Select(row => string.Format(_format, ToShort(row))).ToArray(), results.Add);

            // create the divider
            var spliter = " " + string.Join("", Enumerable.Repeat("-", _longestLine - 1)) + " ";

            builder.AppendLine(spliter);
            builder.AppendLine(_columnHeaders);

            foreach (var row in results)
            {
                builder.AppendLine(spliter);
                builder.AppendLine(row);
            }

            builder.AppendLine(spliter);
            builder.AppendLine("");
            // builder.AppendFormat(" Count: {0}", Rows.Count);

            return builder.ToString();
        }

        public void Write()
        {
            Calculations();
            var header = DrawHeader();
            var body = ToString();
            Console.WriteLine(header + body);
        }
    }
}