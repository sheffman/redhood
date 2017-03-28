using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Automation;

namespace CalcTest
{
   
   [TestFixture]
    public class CalcTest
    {
        [Test, Order(1)]
        public void StartCalc()
        {
            //arrange
            string expected = "calc";
            //act
            Process.Start("calc.exe");
            //assert
            Assert.NotZero(Process.GetProcessesByName(expected).Length);
      
        }
        [Test, Order(2)]
        public void PressFive()
        {
            //arrange
            Thread.Sleep(200);
            string expected = "5";
            AutomationElement mainCalcWindow = AutomationElement.RootElement.FindFirst(TreeScope.Descendants,
                new AndCondition(
                    new PropertyCondition(AutomationElement.ControlTypeProperty, ControlType.Window),
                    new PropertyCondition(AutomationElement.ClassNameProperty, "CalcFrame")));
            var buttonFive = mainCalcWindow.FindFirst(TreeScope.Descendants, new PropertyCondition(AutomationElement.AutomationIdProperty, "135"));
            InvokePattern buttonFivePattern = buttonFive.GetCurrentPattern(InvokePattern.Pattern) as InvokePattern;
            //act
            buttonFivePattern.Invoke();
            //assert
            var resultText= mainCalcWindow.FindFirst(TreeScope.Descendants, new PropertyCondition(AutomationElement.AutomationIdProperty, "150"));
            var actual = resultText.GetCurrentPropertyValue(AutomationElement.NameProperty).ToString();

            Assert.AreEqual(expected, actual);
        }
        [Test, Order(3)]
        public void CloseCalc()
        {
            //arrange
            string processName = "calc";
            var processes = Process.GetProcessesByName(processName);
            if (processes.Length != 0) 
            {
                //act
                processes.Where(p => p.ProcessName.Contains(processName)).ToList().ForEach(pr=>pr.Kill());
                Thread.Sleep(100);
                //assert
                Assert.Zero(Process.GetProcessesByName(processName).Length);
            }
        }
    }
}
