﻿using ERP.Windows.WF.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERP.Windows.WF.Binding.Controls
{
    public interface IBindable
    {
        public event EventHandler ControlValueChanged;

        public event EventHandler FormatRequest;

        public bool Access { get; set; }

        public string BindingDestination { get; set; }

        public Type OriginType { get; }

        public bool ReadOnly { get; set; }

        public InputStatus Status { get; set; }

        public string UserFriendlyName { get; set; }

        public Object GetControlValue();

        public Control GetMainControl();

        public void OnBound(DataContext DataContext);

        public void SetControlValue(Object Value);
    }
}