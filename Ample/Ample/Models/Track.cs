using Ample.Core;
using System;
using System.Collections.Generic;
using System.Text;

namespace Ample.Models
{
    public class Track : NotifyUnit
    {
        /// <summary>
        /// XAML
        /// </summary>
        public int PositionId { get; set; }

        /// <summary>
        /// XAML
        /// </summary>
        public bool IsSelected { get; set; }

        /// <summary>
        /// XAML
        /// </summary>
        public string TitleView {
            get
            {
                string au = AuthorName;
                if (AuthorName == null && AuthorParse != null)
                    au = AuthorParse;

                string tr = TrackName;
                if (TrackName == null && TrackParse != null)
                    tr = TrackParse;

                if (au == null || tr == null)
                    return FileName;
                else
                    return $"{au} - {tr}.{FileExtension}";
            }
        }

        #region Data

        /// <summary>
        /// Альтернативное представление пути до файла
        /// </summary>
        public object AlternativePathObject { get; set; }

        /// <summary>
        /// Абсолютный путь файла
        /// </summary>
        public string AbsolutePath { get; set; }

        /// <summary>
        /// Расширение файла (например, .mp3)
        /// </summary>
        public string FileExtension { get; set; }

        /// <summary>
        /// Название файла (с именем расширения)
        /// </summary>
        public string FileName { get; set; }

        /// <summary>
        /// Автор
        /// </summary>
        public string AuthorName { get; set; }

        /// <summary>
        /// Название трэка
        /// </summary>
        public string TrackName { get; set; }

        /// <summary>
        /// Продолжительность воспроизведения
        /// </summary>
        public decimal Length { get; set; }

        /// <summary>
        /// Имя автора, полученное через парсинг имени файла
        /// </summary>
        public string AuthorParse { get; set; }

        /// <summary>
        /// Название трэка, полученное через парсинг имени файла
        /// </summary>
        public string TrackParse { get; set; }
        #endregion
    }
}
