using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Ample.Core
{
    [Xamarin.Forms.Internals.Preserve(AllMembers = true)]
    public class LockCommand : ICommand
    {
        private readonly BaseViewModel viewModel;
        private readonly bool isCommandCanBlocked;
        private readonly FuncArg func;
        private bool isEnable = true;

        public delegate void Func();
        public delegate void FuncArg(object obj);
        public delegate Task FuncAsync();
        public delegate Task FuncArgAsync(object obj);

        #pragma warning disable CS0067
        public event EventHandler CanExecuteChanged;

        /// <summary>
        /// Комманда которая аттачится к ViewModel.IsBusy - если модель занята, то 
        /// любая другая команда, кроме SimpleCommand не сработает.
        /// После того как команда выполнится, то IsBusy автоматически становится false,
        /// давая возможность другим командам выполняться
        /// </summary>
        /// <param name="vm">ViewModel которая будет блокироваться после нажатия</param>
        /// <param name="function">Действие которое будет выполнено</param>
        public LockCommand(BaseViewModel vm, FuncAsync function, bool isBusy = false)
        {
            viewModel = vm ?? throw new Exception("ViewModel не должна быть null");
            isCommandCanBlocked = isBusy;

            if (isCommandCanBlocked)
                func = async (arg) =>
                {
                    await function();
                    vm.IsBusy = false;
                };
            else
                func = async (arg) =>
                {
                    await function();
                    isEnable = true;
                    if (CanExecuteChanged != null)
                        CanExecuteChanged.Invoke(this, new EventArgs());
                };
        }

        /// <summary>
        /// Комманда которая аттачится к ViewModel.IsBusy - если модель занята, то 
        /// любая другая команда, кроме SimpleCommand не сработает.
        /// После того как команда выполнится, то IsBusy автоматически становится false,
        /// давая возможность другим командам выполняться
        /// </summary>
        /// <param name="vm">ViewModel которая будет блокироваться после нажатия</param>
        /// <param name="funcArg">Действие которое будет выполнено</param>
        public LockCommand(BaseViewModel vm, FuncArgAsync funcArg, bool isBusy = false)
        {
            viewModel = vm ?? throw new Exception("ViewModel не должна быть null");
            isCommandCanBlocked = isBusy;

            if (isCommandCanBlocked)
                func = async (arg) =>
                {
                    await funcArg(arg);
                    vm.IsBusy = false;
                };
            else
                func = async (arg) =>
                {
                    await funcArg(arg);
                    isEnable = true;
                    if (CanExecuteChanged != null)
                        CanExecuteChanged.Invoke(this, new EventArgs());
                };
        }

        /// <summary>
        /// Комманда которая аттачится к ViewModel.IsBusy - если модель занята, то 
        /// любая другая команда, кроме SimpleCommand не сработает.
        /// После того как команда выполнится, то IsBusy автоматически становится false,
        /// давая возможность другим командам выполняться
        /// </summary>
        /// <param name="vm">ViewModel которая будет блокироваться после нажатия</param>
        /// <param name="function">Действие которое будет выполнено</param>
        public LockCommand(BaseViewModel vm, Func function, bool isBusy = false)
        {
            viewModel = vm ?? throw new Exception("ViewModel не должна быть null");
            isCommandCanBlocked = isBusy;

            if (isCommandCanBlocked)
                func = (arg) =>
                {
                    function();
                    vm.IsBusy = false;
                };
            else
                func = (arg) =>
                {
                    function();
                    isEnable = true;
                    if (CanExecuteChanged != null)
                        CanExecuteChanged.Invoke(this, new EventArgs());
                };
        }

        /// <summary>
        /// Комманда которая аттачится к ViewModel.IsBusy - если модель занята, то 
        /// любая другая команда, кроме SimpleCommand не сработает.
        /// После того как команда выполнится, то IsBusy автоматически становится false,
        /// давая возможность другим командам выполняться
        /// </summary>
        /// <param name="vm">ViewModel которая будет блокироваться после нажатия</param>
        /// <param name="funcArg">Действие которое будет выполнено</param>
        public LockCommand(BaseViewModel vm, FuncArg funcArg, bool isBusy = false)
        {
            viewModel = vm ?? throw new Exception("ViewModel не должна быть null");
            isCommandCanBlocked = isBusy;

            if (isCommandCanBlocked)
                func = (arg) =>
                {
                    funcArg(arg);
                    vm.IsBusy = false;
                };
            else
                func = (arg) =>
                {
                    funcArg(arg);
                    isEnable = true;
                    if (CanExecuteChanged != null)
                        CanExecuteChanged.Invoke(this, new EventArgs());
                };
        }

        public bool CanExecute(object parameter)
        {
            if (isCommandCanBlocked)
                return !viewModel.IsBusy;
            else
                return isEnable;
        }

        public void Execute(object parameter)
        {
            if (viewModel != null && viewModel.IsBusy)
                return;

            if (isCommandCanBlocked)
            {
                viewModel.IsBusy = true;
            }
            else
            {
                isEnable = false;
            }

            if (CanExecuteChanged != null)
                CanExecuteChanged.Invoke(this, new EventArgs());

            func(parameter);
        }
    }
}
