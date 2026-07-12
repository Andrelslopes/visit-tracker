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
        // Delegates to avoid reflection overhead in measurements
        private Func<string, string> formatPhoneDelegate;
        private Func<string, string> formatCellDelegate;
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
            // Create delegates bound to the instance to avoid measuring reflection invoke overhead.
            // This assumes the method signatures are: string FormatPhone(string) and string FormatCell(string).
            try
            {
                formatPhoneDelegate = (Func<string, string>)formatPhoneMethod.CreateDelegate(typeof(Func<string, string>), clientInstance);
                formatCellDelegate = (Func<string, string>)formatCellMethod.CreateDelegate(typeof(Func<string, string>), clientInstance);
            }
            catch
            {
                // If delegate creation fails, we'll still be able to run the Invoke variants; delegates will remain null.
                formatPhoneDelegate = null;
                formatCellDelegate = null;
            }
        }

        // Variant A: measure including reflection overhead (MethodInfo.Invoke)
        [Benchmark]
        public string FormatPhone_Invoke_10() => (string)formatPhoneMethod.Invoke(clientInstance, new object[] { numbers10 });
        [Benchmark]
        public string FormatCell_Invoke_11() => (string)formatCellMethod.Invoke(clientInstance, new object[] { numbers11 });
        [Benchmark]
        public string FormatPhone_Invoke_7() => (string)formatPhoneMethod.Invoke(clientInstance, new object[] { numbers7 });
        // Variant B: measure only method logic via delegates (no reflection overhead)
        [Benchmark]
        public string FormatPhone_Delegate_10()
        {
            if (formatPhoneDelegate == null)
                throw new InvalidOperationException("Delegate not available. Falling back to Invoke variant.");
            return formatPhoneDelegate(numbers10);
        }

        [Benchmark]
        public string FormatCell_Delegate_11()
        {
            if (formatCellDelegate == null)
                throw new InvalidOperationException("Delegate not available. Falling back to Invoke variant.");
            return formatCellDelegate(numbers11);
        }

        [Benchmark]
        public string FormatPhone_Delegate_7()
        {
            if (formatPhoneDelegate == null)
                throw new InvalidOperationException("Delegate not available. Falling back to Invoke variant.");
            return formatPhoneDelegate(numbers7);
        }
    }
}