﻿using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Append.Blazor.Sidepanel
{
    internal class SidepanelService : ISidepanelService
    {
        /// <inheritdoc />
        public event Func<ValueTask> OnSidepanelChanged;

        /// <inheritdoc />
        public string Title { get; internal set; }

        /// <inheritdoc />
        public string Subtitle { get; internal set; }

        /// <inheritdoc />
        public Type Component { get; internal set; }

        /// <inheritdoc />
        public Dictionary<string, object> Parameters { get; internal set; }
        
        /// <inheritdoc />
        public bool IsOpen { get; internal set; }

        /// <inheritdoc />
        public void Open(string title, Type component, string subtitle = null, Dictionary<string, object> parameters = null)
        {
            if (component is null)
                throw new NullReferenceException($"{nameof(component)} cannot be null.");

            if (!typeof(ComponentBase).IsAssignableFrom(component))
                throw new ArgumentException($"{component.FullName} must be a Blazor Component");

            IsOpen = true;
            Title = title;
            Subtitle = subtitle;
            Component = component;
            Parameters = parameters;
            OnSidepanelChanged?.Invoke();
        }
        /// <inheritdoc />
        public void Open<TComponent>(string title, string subtitle = "") where TComponent : IComponent
        {
            Open(title, typeof(TComponent), subtitle);
        }
        /// <inheritdoc />
        public void Open<TComponent>(string title, string subtitle, Dictionary<string,object> parameters) where TComponent : IComponent
        {
            Open(title, typeof(TComponent), subtitle,parameters);
        }
        /// <inheritdoc />
        public void Open<TComponent>(string title, string subtitle, (string Key, object Value) parameter) where TComponent : IComponent
        {
            var dict = new Dictionary<string, object>
            {
                { parameter.Key, parameter.Value }
            };
            Open(title, typeof(TComponent), subtitle, dict);
        }
        /// <inheritdoc />
        public void Open<TComponent>(string title, (string Key, object Value) parameter) where TComponent : IComponent
        {
            Open<TComponent>(title, null, parameter);
        }
        /// <inheritdoc />
        public void Close()
        {
            IsOpen = false;
            Title = string.Empty;
            Subtitle = null;
            Component = null;
            OnSidepanelChanged?.Invoke();
        }
        /// <inheritdoc />
        public void SoftClose()
        {
            IsOpen = false;
            OnSidepanelChanged?.Invoke();
        }
    }

}