using System;
using System.ComponentModel;
using Xamarin.Forms;


namespace Shiny.XamForms
{
    public class EntryFocusBindBehavior : Behavior<Entry>
    {
        Entry? boundEntry;


        public static readonly BindableProperty IsFocusedProperty = BindableProperty.Create(
            nameof(IsFocused),
            typeof(bool),
            typeof(EntryFocusBindBehavior),
            false,
            defaultBindingMode: BindingMode.TwoWay,
            propertyChanged: OnFocusChanged
        );
        public bool IsFocused
        {
            get => (bool)this.GetValue(IsFocusedProperty);
            set => this.SetValue(IsFocusedProperty, value);
        }


        static void OnFocusChanged(BindableObject bindable, object oldValue, object newValue)
        {
            var behave = (EntryFocusBindBehavior)bindable;
            behave.SetFocus(behave.boundEntry, (bool)newValue);
        }


        protected override void OnAttachedTo(Entry bindable)
        {
            // TODO: not triggering unfocus back to bind
            base.OnAttachedTo(bindable);
            this.boundEntry = bindable;
            bindable.Focused += this.OnFocused;
            bindable.Unfocused += this.OnFocused;
        }


        protected override void OnDetachingFrom(Entry bindable)
        {
            base.OnDetachingFrom(bindable);
            this.boundEntry = null;
            bindable.Focused -= this.OnFocused;
            bindable.Unfocused -= this.OnFocused;
        }


        void OnFocused(object sender, FocusEventArgs e)
            => this.IsFocused = e.IsFocused;


        void SetFocus(Entry? entry, bool flag)
        {
            if (entry == null)
                return;

            if (flag)
                entry.Focus();
            else
                entry.Unfocus();
        }
    }
}
