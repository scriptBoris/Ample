using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;

namespace Ample.Core
{
    [Xamarin.Forms.Internals.Preserve(AllMembers = true)]
    public class SimpleCommand : ICommand
    {
        private readonly Func func;
        private bool isEnable = true;

        public delegate void FuncAction();
        public delegate void Func(object obj);
#pragma warning disable CS0067
        public event EventHandler CanExecuteChanged;

        /// <summary>
        /// Комманда которая аттачится к ViewModel
        /// </summary>
        /// <param name="vm">ViewModel которая будет блокироваться после нажатия</param>
        /// <param name="act">Действие которое будет выполнено</param>
        /// <param name="canBlocked">Будет ли комманда перед выполнением блокировать ViewModel?</param>
        public SimpleCommand(FuncAction act)
        {
            func = (arg) => act();
        }

        public SimpleCommand(Func f)
        {
            func = (arg) => f(arg);
        }

        /// <summary>
        /// Сделать кнопку не активной
        /// </summary>
        public void TurnDisable()
        {
            isEnable = false;

            if (CanExecuteChanged != null)
                CanExecuteChanged.Invoke(this, new EventArgs());
        }

        /// <summary>
        /// Сделать кнопку активной
        /// </summary>
        public void TurnEnable()
        {
            isEnable = true;

            if (CanExecuteChanged != null)
                CanExecuteChanged.Invoke(this, new EventArgs());
        }

        public bool CanExecute(object parameter)
        {
            return isEnable;
        }

        public void Execute(object parameter)
        {
            if (isEnable)
                func(parameter);
        }
    }
}
