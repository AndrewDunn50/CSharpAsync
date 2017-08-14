using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Async
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private int _taskCounter = 0;
        private object lockObject = new object();

        public MainWindow()
        {
            InitializeComponent();
        }

        private void LongRunningOperationButton_Click(object sender, RoutedEventArgs e)
        {
            var task = Task.Factory.StartNew(() =>
            {
                lock(lockObject)
                {
                    _taskCounter++;
                }

                int taskNumber = _taskCounter;

                for (int i = 0; i < int.MaxValue; i++) {}

                return taskNumber;
            });

            //Continuation - fire task after 'task' is finished
            var continueTask = task.ContinueWith((antecedent) => //antecendent - task that fired this task (parent)
            {
                ResultsTextBox.Text += $"{Environment.NewLine}Task {antecedent.Result} Complete";
            },
            TaskScheduler.FromCurrentSynchronizationContext()); //run continuation on the thread ui

        }
    }
}
