﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace CodeCheckerAddin.Commands
{
    public class RelayCommand : ICommand
    {
      #region Variables

      private readonly Predicate<object> m_canExecute;
      private readonly Action<object> m_execute;
      private readonly Action _act;

      #endregion Variables

      #region Constructor

      public RelayCommand(Action act)
      {
         _act = act;
      }

      public RelayCommand(Action<object> execute, Predicate<object> canExecute = null)
      {
         m_execute = execute ?? throw new ArgumentNullException("Execute");
         m_canExecute = canExecute;
      }

      #endregion Constructor

      #region Implementation

      // Evaluate the command if it is valid to execute
      public bool CanExecute(object parameter = null)
      {
         if (parameter == null || m_canExecute == null) return true;
         else return m_canExecute(parameter);
      }

      // Main execute method
      public void Execute(object parameter = null)
      {
         if (_act != null) _act();
         else m_execute(parameter);
      }

      // In WPF CommandManager is a pre-defined class that take charge of observing the user interface
      // and calls the CanExecute method when it deems it necessary
      public event EventHandler CanExecuteChanged
      {
         add => CommandManager.RequerySuggested += value;
         remove => CommandManager.RequerySuggested -= value;
      }

      #endregion Implementation






   }
}
