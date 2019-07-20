using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace Ample.Core
{
    public abstract class BaseViewModel : NotifyUnit
    {
        private bool isFirstAppearing;

        #region props
        /// <summary>
        /// Указывает - видит ли пользователь данную страницу на главном экране или нет
        /// </summary>
        public bool IsAppearing { get; private set; }

        /// <summary>
        /// Анимация Pull refresh
        /// </summary>
        public bool IsBusy { get; set; }

        /// <summary>
        /// Анимация обращения HTTP запроса к серверу
        /// </summary>
        public bool IsLoading { get; set; }

        /// <summary>
        /// Нет соединения с сервером
        /// </summary>
        public bool IsNotConnect { get; set; }

        /// <summary>
        /// Какая на данный ориентация экрана
        /// </summary>
        public StackOrientation Orientation { get; private set; }

        public List<BaseTabModel> ChildTabs { get; set; } = new List<BaseTabModel>();

        public abstract Page View { get; protected set; }
        #endregion

        public BaseViewModel()
        {
            View.BindingContext = this;

            View.Appearing += ViewAppearing;
            View.Disappearing += ViewDisappearing;
            View.SizeChanged += SizeChanged;
        }

        #region private methods
        private void ViewAppearing(object sender, EventArgs e)
        {
            IsAppearing = true;
            if (!isFirstAppearing)
            {
                isFirstAppearing = true;
                OnFirstAppearing();
            }
            OnAppearing();
        }

        private void ViewDisappearing(object sender, EventArgs e)
        {
            IsAppearing = false;
            OnDisappearing();
        }

        private void SizeChanged(object sender, EventArgs e)
        {
            var w = View.Width;
            var h = View.Height;
            var lastOrientation = Orientation;

            if (h > w)
                Orientation = StackOrientation.Vertical;
            else
                Orientation = StackOrientation.Horizontal;

            if (Orientation != lastOrientation)
            {
                OnPropertychanged(nameof(Orientation));
                OnOrientationChanged(Orientation);
            }
        }
        #endregion

        #region virtuals
        /// <summary>
        /// Страница первый раз появилась на экране
        /// </summary>
        protected virtual void OnFirstAppearing() { }

        /// <summary>
        /// Страница стала активной
        /// </summary>
        protected virtual void OnAppearing() { }

        /// <summary>
        /// Страница получила результат работы
        /// </summary>
        /// <param name="sender">ViewModel, которая передала результат работы</param>
        /// <param name="result"></param>
        protected virtual void OnTakeResult(BaseViewModel sender, object result) { }

        /// <summary>
        /// Страница перестала быть активной
        /// </summary>
        protected virtual void OnDisappearing() { }

        /// <summary>
        /// Срабатывает каждый раз, когда поверх этой страницы открывается еще одна страница
        /// </summary>
        protected virtual void OnNext() { }

        /// <summary>
        /// Срабатывает каждый раз, когда текущую страницу убирают с помощью Pop или кнопки Back
        /// </summary>
        protected virtual void OnBack() { }

        /// <summary>
        /// Страница изменила свою ориентацию
        /// </summary>
        protected virtual void OnOrientationChanged(StackOrientation orientation) { }
        #endregion
    }
}
