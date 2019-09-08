using Calculator.Mvvm;
using ExpressionLibrary;
using System;
using System.ComponentModel;
using System.Windows.Input;

namespace Calculator
{
    internal class ViewModel : INotifyPropertyChanged, IDataErrorInfo
    {
        private string error;
        private string expression;
        private int expressionResult;
        private bool hasError;
        private bool showResult;

        public event PropertyChangedEventHandler PropertyChanged;

        public string Error
        {
            get => error;

            set
            {
                error = value;
                NotifyPropertyChanged(nameof(Error));
            }
        }

        public ICommand ExecuteCommand => new DelegateCommand(Execute);

        public int ExpressionResult
        {
            get => expressionResult;

            set
            {
                expressionResult = value;
                NotifyPropertyChanged(nameof(ExpressionResult));
            }
        }

        public bool HasError
        {
            get => hasError;

            set
            {
                hasError = value;
                NotifyPropertyChanged(nameof(HasError));
            }
        }

        public bool ShowResult
        {
            get => showResult;

            set
            {
                showResult = value;
                NotifyPropertyChanged(nameof(ShowResult));
            }
        }

        public string UserExpression
        {
            get => expression;

            set
            {
                expression = value;
                NotifyPropertyChanged(nameof(UserExpression));
                OnUserExpressionChanged();
            }
        }

        public string this[string columnName]
        {
            get
            {
                string result = null;

                if (columnName.Equals(nameof(UserExpression)) && !Expression.ValidateExpression(UserExpression, out string error))
                {
                    result = error;
                }

                Error = result;
                HasError = result != null;
                return result;
            }
        }

        private void Execute()
        {
            if (!HasError)
            {
                try
                {
                    ExpressionResult = Expression.Evaluate(UserExpression);
                    ShowResult = true;
                }
                catch (ArithmeticException ex)
                {
                    HasError = true;
                    Error = ex.Message;
                }
                catch (Exception ex)
                {
                    HasError = true;
                    Error = $"An unexpected error occurred.\r\n{ex.Message}";
                }
            }
        }

        private void NotifyPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private void OnUserExpressionChanged()
        {
            ShowResult = false;
        }
    }
}