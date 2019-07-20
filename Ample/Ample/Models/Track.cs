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

        #region Data
        public string AbsolutePath { get; set; }
        public string FileName { get; set; }
        public string AuthorName { get; set; }
        public string TrackName { get; set; }
        public decimal Length { get; set; }
        #endregion
    }
}
