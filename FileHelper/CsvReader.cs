namespace InvitationGenerator.FileHelper
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Reflection;

    using Contracts;
    using static Contracts.Constants;

    public class CsvReader : ICsvReader
    {
        public List<T> Parse<T>(Stream stream) where T : new()
        {
            var results = new List<T>();
            var csvDescriptors = InitializeCsvDescriptors<T>();

            using (var streamReader = new StreamReader(stream))
            {
                var headers = streamReader.ReadLine();
                if (string.IsNullOrEmpty(headers))
                {
                    throw new ArgumentNullException(NoDataFound);
                }
                var boundIndexCsvDescriptors = BindIndexToCsvDescriptors(headers, csvDescriptors);
                var areDescriptorsValid = boundIndexCsvDescriptors.All(s => s.Index == -1);
                if (areDescriptorsValid)
                {
                    throw new InvalidDataException(InValidData);
                }
                var sortedCsvDescriptors = new SortedDictionary<int, CsvDescriptor>(boundIndexCsvDescriptors.ToDictionary(x => x.Index, x => x));
                var line = string.Empty;

                while (!string.IsNullOrEmpty(line = streamReader.ReadLine()))
                {
                    results.Add(BindData<T>(line, sortedCsvDescriptors));
                }
            }

            return results;
        }

        private T BindData<T>(string line, IDictionary<int, CsvDescriptor> csvDescriptors) where T : new()
        {
            var data = line.Split(',');
            var row = new T();

            for (var i = 0; i < data.Length; i++)
            {
                if (csvDescriptors.ContainsKey(i))
                {
                    var csvDescriptor = csvDescriptors[i];
                    var value = data[i];
                    if (csvDescriptor.PropertyInfo.PropertyType == typeof(int) ||
                        csvDescriptor.PropertyInfo.PropertyType == typeof(decimal))
                    {
                        var isNumeric = decimal.TryParse(value, out decimal numericValue);
                        if (!isNumeric)
                        {
                            value = "0";
                        }
                    }

                    csvDescriptor.PropertyInfo.SetValue(
                        obj: row,
                        value: Convert.ChangeType(value, csvDescriptor.PropertyInfo.PropertyType),
                        index: null
                        );
                }
            }
            return row;
        }

        private IEnumerable<CsvDescriptor> InitializeCsvDescriptors<T>() where T : new()
        {
            var targetType = typeof(T);
            var properties = targetType.GetProperties(BindingFlags.Public | BindingFlags.Instance);

            var csvDescriptors = from prop in properties
                                 let customAttributes = prop.GetCustomAttributes(typeof(CsvEntryAttribute), false).Cast<CsvEntryAttribute>()
                                 from attribute in customAttributes
                                 where !string.IsNullOrEmpty(attribute.HeaderName)
                                 select new CsvDescriptor(prop, attribute.HeaderName);
            return csvDescriptors;
        }

        private IEnumerable<CsvDescriptor> BindIndexToCsvDescriptors(string headerLine, IEnumerable<CsvDescriptor> csvDescriptors)
        {
            var headers = headerLine.Split(',');
            var descriptors = new List<CsvDescriptor>(csvDescriptors);

            for (var i = 0; i < headers.Length; i++)
            {
                var header = headers[i];
                var csvDescriptor = descriptors.FirstOrDefault(x => x.HeaderName == header);

                if (csvDescriptor == null)
                {
                    continue;
                }

                csvDescriptor.Index = i;
            }

            return descriptors;
        }
    }
}
