using System;
using System.Reflection;
using System.Runtime.Serialization;
using BenchmarkDotNet.Attributes;
using Microsoft.VSDiagnostics;

namespace visit_tracker_form
{
    [CPUUsageDiagnoser]
    public class FormattersBenchmark
    {
        private object clientInstance;
        private MethodInfo formatPhoneMethod;
        private MethodInfo formatCellMethod;
        private string numbers10 = "1234567890";
        private string numbers11 = "12345678901";
        private string numbers7 = "1234567";
        [GlobalSetup]
        public void Setup()
        {
            var type = typeof(frm_Client);
            // Create instance without running constructor to avoid DB/UI side effects
            clientInstance = FormatterServices.GetUninitializedObject(type);
            formatPhoneMethod = type.GetMethod("FormatPhone", BindingFlags.NonPublic | BindingFlags.Instance);
            formatCellMethod = type.GetMethod("FormatCell", BindingFlags.NonPublic | BindingFlags.Instance);
            if (formatPhoneMethod == null || formatCellMethod == null)
                throw new InvalidOperationException("Could not find target methods. Ensure they exist and are named 'FormatPhone' and 'FormatCell'.");
        }

        [Benchmark]
        public string FormatPhone_10() => (string)formatPhoneMethod.Invoke(clientInstance, new object[] { numbers10 });
        [Benchmark]
        public string FormatCell_11() => (string)formatCellMethod.Invoke(clientInstance, new object[] { numbers11 });
        [Benchmark]
        public string FormatPhone_7() => (string)formatPhoneMethod.Invoke(clientInstance, new object[] { numbers7 });
    }
}